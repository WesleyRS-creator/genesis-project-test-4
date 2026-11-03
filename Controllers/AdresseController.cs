namespace TestMVCSharpC.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Dto;
using Services.Interface;
using TestMVCSharpC.Utils;
using TestMVCSharpC.Filters;
using TestMVCSharpC.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.IO;


public class AdresseController  : Controller 
{
	private readonly IAdresseService _service;
	private readonly IPersonneService _personneService;
	private readonly EntityUtils _detector;


	public AdresseController(IAdresseService service, IPersonneService personneService, EntityUtils detector)
	{
	   _service = service;
	   _personneService = personneService;
	   _detector = detector;

	}

	// POST: /Adresse/ExportToCsv
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ExportToCsvAsync(AdresseDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);

	    // 2. On détecte la colonne d'affichage pour la FK
	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();

	    // 3. On aplatit les données dans une liste de dictionnaires
	    var exportData = (results.Content ?? new List<Adresse>()).Select(item => new Dictionary<string, object>
	    {
{ "Id", item.Id },
{ "Personne", item.IdpersonnePersonne?.GetType().GetProperty(personneDisplayColumn)?.GetValue(item.IdpersonnePersonne) },
{ "Latitude", item.Latitude },
{ "CodePostal", item.CodePostal },
{ "Ville", item.Ville },
{ "Rue", item.Rue },
	    }).ToList();

	    var csvContent = await _service.ExportToCsvAsync(exportData);
	    var bytes = Encoding.UTF8.GetBytes(csvContent);

	    Response.Headers["Content-Disposition"] = "attachment; filename=Adresse.csv";

	    return File(bytes, "text/csv; charset=utf-8");
	}

	// GET: /Adresse

	// GET: /Adresse
	public async Task<IActionResult> Index(int page = 0, int pageSize = 10, string? sortOrder = null, string? sortBy = null)
	{
	    string finalSortOrder = !string.IsNullOrEmpty(sortBy)
	        ? WebUtils.AddSort(sortOrder, sortBy)
	        : sortOrder;

	    var results = await _service.GetAllAsync(page, pageSize, finalSortOrder, e => e.IdpersonnePersonne);

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);

	    var vm = new AdresseViewModel
	    {
	        Adresses = results.Content ?? new List<Adresse>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	        CurrentSort = finalSortOrder
	    };

	    return View(vm);
	}

	// POST: /Adresse/Search
	[HttpPost]
	public async Task<IActionResult> Search(AdresseDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);

	    var vm = new AdresseViewModel
	    {
	        Adresses = results.Content ?? new List<Adresse>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	        SearchAdresseCriteria = dto,
	        CurrentSort = sortOrder ?? string.Empty
	    };

	    return View("Index", vm);
	}

	// GET: /Adresse/Details/id
	public async Task<IActionResult> Details(int id)
	{
	    var adresse = await _service.GetByIdAsync(id, e => e.IdpersonnePersonne);
	    if (adresse == null) return NotFound();

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();

	    var vm = new AdresseViewModel
	    {
	        Adresse = adresse,
	        PersonneDisplayColumn = personneDisplayColumn,
	    };

	    return View(vm);
	}

	// GET: /Adresse/Create
	public async Task<IActionResult> Create()
	{
	    var vm = await BuildFormViewModel(new Adresse());
	    return View(vm);
	}

	// POST: /Adresse/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(AdresseViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.AddAsync(vm.Adresse);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Adresse);
	    return View(reloadedVm);
	}

	// GET: /Adresse/Edit/id
	public async Task<IActionResult> Edit(int id)
	{
	    var adresse = await _service.GetByIdAsync(id);
	    if (adresse == null) return NotFound();

	    var vm = await BuildFormViewModel(adresse);
	    return View(vm);
	}

	// POST: /Adresse/Edit/id
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(AdresseViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.UpdateAsync(vm.Adresse);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Adresse);
	    return View(reloadedVm);
	}

	// POST: /Adresse/Delete/id
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
	    await _service.DeleteAsync(id);
	    return RedirectToAction(nameof(Index));
	}

	private async Task<AdresseViewModel> BuildFormViewModel(Adresse adresse)
	{
	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);
	    return new AdresseViewModel
	    {
	        Adresse = adresse,
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	    };
	}

}
