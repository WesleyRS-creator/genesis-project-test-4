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


public class ParticiperController  : Controller 
{
	private readonly IParticiperService _service;
	private readonly IPersonneService _personneService;
	private readonly IProjetService _projetService;
	private readonly EntityUtils _detector;


	public ParticiperController(IParticiperService service, IPersonneService personneService, IProjetService projetService, EntityUtils detector)
	{
	   _service = service;
	   _personneService = personneService;
	   _projetService = projetService;
	   _detector = detector;

	}

	// POST: /Participer/ExportToCsv
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ExportToCsvAsync(ParticiperDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);

	    // 2. On détecte la colonne d'affichage pour la FK
	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    // 2. On détecte la colonne d'affichage pour la FK
	    string projetDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Projet>();

	    // 3. On aplatit les données dans une liste de dictionnaires
	    var exportData = (results.Content ?? new List<Participer>()).Select(item => new Dictionary<string, object>
	    {
{ "Id", item.Id },
{ "Personne", item.IdpersonnePersonne?.GetType().GetProperty(personneDisplayColumn)?.GetValue(item.IdpersonnePersonne) },
{ "Projet", item.IdprojetProjet?.GetType().GetProperty(projetDisplayColumn)?.GetValue(item.IdprojetProjet) },
{ "DateInscription", item.DateInscription },
{ "RoleDansProjet", item.RoleDansProjet },
	    }).ToList();

	    var csvContent = await _service.ExportToCsvAsync(exportData);
	    var bytes = Encoding.UTF8.GetBytes(csvContent);

	    Response.Headers["Content-Disposition"] = "attachment; filename=Participer.csv";

	    return File(bytes, "text/csv; charset=utf-8");
	}

	// GET: /Participer

	// GET: /Participer
	public async Task<IActionResult> Index(int page = 0, int pageSize = 10, string? sortOrder = null, string? sortBy = null)
	{
	    string finalSortOrder = !string.IsNullOrEmpty(sortBy)
	        ? WebUtils.AddSort(sortOrder, sortBy)
	        : sortOrder;

	    var results = await _service.GetAllAsync(page, pageSize, finalSortOrder, e => e.IdpersonnePersonne, e => e.IdprojetProjet);

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);
	    string projetDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Projet>();
	    string projetPkColumn = _detector.GetPrimaryKeyName<Projet>();
	    var projets = await _projetService.GetAllAsync(0, int.MaxValue, null);

	    var vm = new ParticiperViewModel
	    {
	        Participers = results.Content ?? new List<Participer>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	        ProjetDisplayColumn = projetDisplayColumn,
	        ProjetPkColumn = projetPkColumn,
	        ProjetsSelectList = new SelectList(projets?.Content ?? new List<Projet>(), projetPkColumn, projetDisplayColumn),
	        CurrentSort = finalSortOrder
	    };

	    return View(vm);
	}

	// POST: /Participer/Search
	[HttpPost]
	public async Task<IActionResult> Search(ParticiperDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);
	    string projetDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Projet>();
	    string projetPkColumn = _detector.GetPrimaryKeyName<Projet>();
	    var projets = await _projetService.GetAllAsync(0, int.MaxValue, null);

	    var vm = new ParticiperViewModel
	    {
	        Participers = results.Content ?? new List<Participer>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	        ProjetDisplayColumn = projetDisplayColumn,
	        ProjetPkColumn = projetPkColumn,
	        ProjetsSelectList = new SelectList(projets?.Content ?? new List<Projet>(), projetPkColumn, projetDisplayColumn),
	        SearchParticiperCriteria = dto,
	        CurrentSort = sortOrder ?? string.Empty
	    };

	    return View("Index", vm);
	}

	// GET: /Participer/Details/id
	public async Task<IActionResult> Details(int id)
	{
	    var participer = await _service.GetByIdAsync(id, e => e.IdpersonnePersonne, e => e.IdprojetProjet);
	    if (participer == null) return NotFound();

	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string projetDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Projet>();

	    var vm = new ParticiperViewModel
	    {
	        Participer = participer,
	        PersonneDisplayColumn = personneDisplayColumn,
	        ProjetDisplayColumn = projetDisplayColumn,
	    };

	    return View(vm);
	}

	// GET: /Participer/Create
	public async Task<IActionResult> Create()
	{
	    var vm = await BuildFormViewModel(new Participer());
	    return View(vm);
	}

	// POST: /Participer/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(ParticiperViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.AddAsync(vm.Participer);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Participer);
	    return View(reloadedVm);
	}

	// GET: /Participer/Edit/id
	public async Task<IActionResult> Edit(int id)
	{
	    var participer = await _service.GetByIdAsync(id);
	    if (participer == null) return NotFound();

	    var vm = await BuildFormViewModel(participer);
	    return View(vm);
	}

	// POST: /Participer/Edit/id
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(ParticiperViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.UpdateAsync(vm.Participer);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Participer);
	    return View(reloadedVm);
	}

	// POST: /Participer/Delete/id
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
	    await _service.DeleteAsync(id);
	    return RedirectToAction(nameof(Index));
	}

	private async Task<ParticiperViewModel> BuildFormViewModel(Participer participer)
	{
	    string personneDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Personne>();
	    string personnePkColumn = _detector.GetPrimaryKeyName<Personne>();
	    var personnes = await _personneService.GetAllAsync(0, int.MaxValue, null);
	    string projetDisplayColumn = await _detector.DetectBestDisplayColumnAsync<Projet>();
	    string projetPkColumn = _detector.GetPrimaryKeyName<Projet>();
	    var projets = await _projetService.GetAllAsync(0, int.MaxValue, null);
	    return new ParticiperViewModel
	    {
	        Participer = participer,
	        PersonneDisplayColumn = personneDisplayColumn,
	        PersonnePkColumn = personnePkColumn,
	        PersonnesSelectList = new SelectList(personnes?.Content ?? new List<Personne>(), personnePkColumn, personneDisplayColumn),
	        ProjetDisplayColumn = projetDisplayColumn,
	        ProjetPkColumn = projetPkColumn,
	        ProjetsSelectList = new SelectList(projets?.Content ?? new List<Projet>(), projetPkColumn, projetDisplayColumn),
	    };
	}

}
