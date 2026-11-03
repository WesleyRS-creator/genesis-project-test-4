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


public class PasseportController  : Controller 
{
	private readonly IPasseportService _service;
	private readonly IPersonneService _personneService;
	private readonly EntityUtils _detector;


	public PasseportController(IPasseportService service, IPersonneService personneService, EntityUtils detector)
	{
	   _service = service;
	   _personneService = personneService;
	   _detector = detector;

	}

	// POST: /Passeport/ExportToCsv
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ExportToCsvAsync(PasseportDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);

	    // 2. On détecte la colonne d'affichage pour la FK
	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();

	    // 3. On aplatit les données dans une liste de dictionnaires
	    var exportData = (results.Content ?? new List<Passeport>()).Select(item => new Dictionary<string, object>
	    {
{ "DateExpiration", item.DateExpiration },
{ "Id", item.Id },
{ "Personne", item.IdpersonnePersonne?.GetType().GetProperty(personneDisplayColumn)?.GetValue(item.IdpersonnePersonne) },
{ "Numero", item.Numero },
	    }).ToList();

	    var csvContent = await _service.ExportToCsvAsync(exportData);
	    var bytes = Encoding.UTF8.GetBytes(csvContent);

	    Response.Headers["Content-Disposition"] = "attachment; filename=Passeport.csv";

	    return File(bytes, "text/csv; charset=utf-8");
	}

	// GET: /Passeport

	// GET: /Passeport
	public async Task<IActionResult> Index(int page = 0, int pageSize = 10, string? sortOrder = null, string? sortBy = null)
	{
	    string finalSortOrder = !string.IsNullOrEmpty(sortBy)
	        ? WebUtils.AddSort(sortOrder, sortBy)
	        : sortOrder;

	    var results = await _service.GetAllAsync(page, pageSize, finalSortOrder, e => e.IdpersonnePersonne);

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);

	    var vm = new PasseportViewModel
	    {
	        Passeports = results.Content ?? new List<Passeport>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	        CurrentSort = finalSortOrder
	    };

	    return View(vm);
	}

	// POST: /Passeport/Search
	[HttpPost]
	public async Task<IActionResult> Search(PasseportDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);

	    var vm = new PasseportViewModel
	    {
	        Passeports = results.Content ?? new List<Passeport>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	        SearchPasseportCriteria = dto,
	        CurrentSort = sortOrder ?? string.Empty
	    };

	    return View("Index", vm);
	}

	// GET: /Passeport/Details/id
	public async Task<IActionResult> Details(int id)
	{
	    var passeport = await _service.GetByIdAsync(id, e => e.IdpersonnePersonne);
	    if (passeport == null) return NotFound();

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();

	    var vm = new PasseportViewModel
	    {
	        Passeport = passeport,
	        PersonneDisplayColumn = personneDisplayColumn,
	    };

	    return View(vm);
	}

	// GET: /Passeport/Create
	public async Task<IActionResult> Create()
	{
	    var vm = await BuildFormViewModel(new Passeport());
	    return View(vm);
	}

	// POST: /Passeport/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(PasseportViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.AddAsync(vm.Passeport);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Passeport);
	    return View(reloadedVm);
	}

	// GET: /Passeport/Edit/id
	public async Task<IActionResult> Edit(int id)
	{
	    var passeport = await _service.GetByIdAsync(id);
	    if (passeport == null) return NotFound();

	    var vm = await BuildFormViewModel(passeport);
	    return View(vm);
	}

	// POST: /Passeport/Edit/id
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(PasseportViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.UpdateAsync(vm.Passeport);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Passeport);
	    return View(reloadedVm);
	}

	// POST: /Passeport/Delete/id
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
	    await _service.DeleteAsync(id);
	    return RedirectToAction(nameof(Index));
	}

	private async Task<PasseportViewModel> BuildFormViewModel(Passeport passeport)
	{
	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);
	    return new PasseportViewModel
	    {
	        Passeport = passeport,
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	    };
	}

}
