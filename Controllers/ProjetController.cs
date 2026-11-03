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


public class ProjetController  : Controller 
{
	private readonly IProjetService _service;
	

	public ProjetController(IProjetService service)
	{
	   _service = service;
	   
	}

	// POST: /Projet/ExportToCsv
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ExportToCsvAsync(ProjetDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    // 3. On aplatit les données dans une liste de dictionnaires
	    var exportData = (results.Content ?? new List<Projet>()).Select(item => new Dictionary<string, object>
	    {
{ "BudgetEstime", item.BudgetEstime },
{ "DateDebut", item.DateDebut },
{ "Id", item.Id },
{ "NomProjet", item.NomProjet },
	    }).ToList();

	    var csvContent = await _service.ExportToCsvAsync(exportData);
	    var bytes = Encoding.UTF8.GetBytes(csvContent);

	    Response.Headers["Content-Disposition"] = "attachment; filename=Projet.csv";

	    return File(bytes, "text/csv; charset=utf-8");
	}

	// GET: /Projet

	// GET: /Projet
	public async Task<IActionResult> Index(int page = 0, int pageSize = 10, string? sortOrder = null, string? sortBy = null)
	{
	    string finalSortOrder = !string.IsNullOrEmpty(sortBy)
	        ? WebUtils.AddSort(sortOrder, sortBy)
	        : sortOrder;

	    var results = await _service.GetAllAsync(page, pageSize, finalSortOrder);


	    var vm = new ProjetViewModel
	    {
	        Projets = results.Content ?? new List<Projet>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        CurrentSort = finalSortOrder
	    };

	    return View(vm);
	}

	// POST: /Projet/Search
	[HttpPost]
	public async Task<IActionResult> Search(ProjetDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    var vm = new ProjetViewModel
	    {
	        Projets = results.Content ?? new List<Projet>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        SearchProjetCriteria = dto,
	        CurrentSort = sortOrder ?? string.Empty
	    };

	    return View("Index", vm);
	}

	// GET: /Projet/Details/id
	public async Task<IActionResult> Details(int id)
	{
	    var projet = await _service.GetByIdAsync(id);
	    if (projet == null) return NotFound();


	    var vm = new ProjetViewModel
	    {
	        Projet = projet,
	    };

	    return View(vm);
	}

	// GET: /Projet/Create
	public async Task<IActionResult> Create()
	{
	    var vm = await BuildFormViewModel(new Projet());
	    return View(vm);
	}

	// POST: /Projet/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(ProjetViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.AddAsync(vm.Projet);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Projet);
	    return View(reloadedVm);
	}

	// GET: /Projet/Edit/id
	public async Task<IActionResult> Edit(int id)
	{
	    var projet = await _service.GetByIdAsync(id);
	    if (projet == null) return NotFound();

	    var vm = await BuildFormViewModel(projet);
	    return View(vm);
	}

	// POST: /Projet/Edit/id
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(ProjetViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.UpdateAsync(vm.Projet);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Projet);
	    return View(reloadedVm);
	}

	// POST: /Projet/Delete/id
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
	    await _service.DeleteAsync(id);
	    return RedirectToAction(nameof(Index));
	}

	private async Task<ProjetViewModel> BuildFormViewModel(Projet projet)
	{
	    return new ProjetViewModel
	    {
	        Projet = projet,
	    };
	}

}
