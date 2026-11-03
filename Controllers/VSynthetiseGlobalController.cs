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


public class VSynthetiseGlobalController  : Controller 
{
	private readonly IVSynthetiseGlobalService _service;
	

	public VSynthetiseGlobalController(IVSynthetiseGlobalService service)
	{
	   _service = service;
	   
	}

	// POST: /VSynthetiseGlobal/ExportToCsv
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ExportToCsvAsync(VSynthetiseGlobalDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    // 3. On aplatit les données dans une liste de dictionnaires
	    var exportData = (results.Content ?? new List<VSynthetiseGlobal>()).Select(item => new Dictionary<string, object>
	    {
{ "IdPersonne", item.IdPersonne },
{ "NomComplet", item.NomComplet },
{ "TrancheAge", item.TrancheAge },
{ "TotalParticipations", item.TotalParticipations },
{ "RolePrincipal", item.RolePrincipal },
{ "BudgetMoyenProjets", item.BudgetMoyenProjets },
{ "BudgetGlobalMoyen", item.BudgetGlobalMoyen },
{ "PositionBudget", item.PositionBudget },
{ "NombreVilles", item.NombreVilles },
{ "LatitudeMoyenne", item.LatitudeMoyenne },
	    }).ToList();

	    var csvContent = await _service.ExportToCsvAsync(exportData);
	    var bytes = Encoding.UTF8.GetBytes(csvContent);

	    Response.Headers["Content-Disposition"] = "attachment; filename=VSynthetiseGlobal.csv";

	    return File(bytes, "text/csv; charset=utf-8");
	}

	// GET: /VSynthetiseGlobal

	// GET: /VSynthetiseGlobal
	public async Task<IActionResult> Index(int page = 0, int pageSize = 10, string? sortOrder = null, string? sortBy = null)
	{
	    string finalSortOrder = !string.IsNullOrEmpty(sortBy)
	        ? WebUtils.AddSort(sortOrder, sortBy)
	        : sortOrder;

	    var results = await _service.GetAllAsync(page, pageSize, finalSortOrder);


	    var vm = new VSynthetiseGlobalViewModel
	    {
	        VSynthetiseGlobals = results.Content ?? new List<VSynthetiseGlobal>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        CurrentSort = finalSortOrder
	    };

	    return View(vm);
	}

	// POST: /VSynthetiseGlobal/Search
	[HttpPost]
	public async Task<IActionResult> Search(VSynthetiseGlobalDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    var vm = new VSynthetiseGlobalViewModel
	    {
	        VSynthetiseGlobals = results.Content ?? new List<VSynthetiseGlobal>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        SearchVSynthetiseGlobalCriteria = dto,
	        CurrentSort = sortOrder ?? string.Empty
	    };

	    return View("Index", vm);
	}

	// GET: /VSynthetiseGlobal/Details/idPersonne
	public async Task<IActionResult> Details(int idPersonne)
	{
	    var vSynthetiseGlobal = await _service.GetByIdAsync(idPersonne);
	    if (vSynthetiseGlobal == null) return NotFound();


	    var vm = new VSynthetiseGlobalViewModel
	    {
	        VSynthetiseGlobal = vSynthetiseGlobal,
	    };

	    return View(vm);
	}







}
