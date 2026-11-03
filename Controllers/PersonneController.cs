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


public class PersonneController  : Controller 
{
	private readonly IPersonneService _service;
	

	public PersonneController(IPersonneService service)
	{
	   _service = service;
	   
	}

	// POST: /Personne/ExportToCsv
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ExportToCsvAsync(PersonneDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    // 3. On aplatit les données dans une liste de dictionnaires
	    var exportData = (results.Content ?? new List<Personne>()).Select(item => new Dictionary<string, object>
	    {
{ "DateNaissance", item.DateNaissance },
{ "Id", item.Id },
{ "NumeroSs", item.NumeroSs },
{ "Nom", item.Nom },
{ "Prenom", item.Prenom },
{ "Biographie", item.Biographie },
	    }).ToList();

	    var csvContent = await _service.ExportToCsvAsync(exportData);
	    var bytes = Encoding.UTF8.GetBytes(csvContent);

	    Response.Headers["Content-Disposition"] = "attachment; filename=Personne.csv";

	    return File(bytes, "text/csv; charset=utf-8");
	}

	// GET: /Personne

	// GET: /Personne
	public async Task<IActionResult> Index(int page = 0, int pageSize = 10, string? sortOrder = null, string? sortBy = null)
	{
	    string finalSortOrder = !string.IsNullOrEmpty(sortBy)
	        ? WebUtils.AddSort(sortOrder, sortBy)
	        : sortOrder;

	    var results = await _service.GetAllAsync(page, pageSize, finalSortOrder);


	    var vm = new PersonneViewModel
	    {
	        Personnes = results.Content ?? new List<Personne>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        CurrentSort = finalSortOrder
	    };

	    return View(vm);
	}

	// POST: /Personne/Search
	[HttpPost]
	public async Task<IActionResult> Search(PersonneDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    var vm = new PersonneViewModel
	    {
	        Personnes = results.Content ?? new List<Personne>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        SearchPersonneCriteria = dto,
	        CurrentSort = sortOrder ?? string.Empty
	    };

	    return View("Index", vm);
	}

	// GET: /Personne/Details/id
	public async Task<IActionResult> Details(int id)
	{
	    var personne = await _service.GetByIdAsync(id);
	    if (personne == null) return NotFound();


	    var vm = new PersonneViewModel
	    {
	        Personne = personne,
	    };

	    return View(vm);
	}

	// GET: /Personne/Create
	public async Task<IActionResult> Create()
	{
	    var vm = await BuildFormViewModel(new Personne());
	    return View(vm);
	}

	// POST: /Personne/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(PersonneViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.AddAsync(vm.Personne);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Personne);
	    return View(reloadedVm);
	}

	// GET: /Personne/Edit/id
	public async Task<IActionResult> Edit(int id)
	{
	    var personne = await _service.GetByIdAsync(id);
	    if (personne == null) return NotFound();

	    var vm = await BuildFormViewModel(personne);
	    return View(vm);
	}

	// POST: /Personne/Edit/id
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(PersonneViewModel vm)
	{
	    if (ModelState.IsValid)
	    {
	        try
	        {
	            await _service.UpdateAsync(vm.Personne);
	            return RedirectToAction(nameof(Index));
	        }
	        catch (Exception ex)
	        {
	            string detailedMessage = GlobalExceptionFilter.GetInnermostMessage(ex);
	            ModelState.AddModelError("", detailedMessage);
	        }
	    }

	    var reloadedVm = await BuildFormViewModel(vm.Personne);
	    return View(reloadedVm);
	}

	// POST: /Personne/Delete/id
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Delete(int id)
	{
	    await _service.DeleteAsync(id);
	    return RedirectToAction(nameof(Index));
	}

	private async Task<PersonneViewModel> BuildFormViewModel(Personne personne)
	{
	    return new PersonneViewModel
	    {
	        Personne = personne,
	    };
	}

}
