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


public class VProjetsEtEquipeController  : Controller 
{
	private readonly IVProjetsEtEquipeService _service;
	

	public VProjetsEtEquipeController(IVProjetsEtEquipeService service)
	{
	   _service = service;
	   
	}

	// POST: /VProjetsEtEquipe/ExportToCsv
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ExportToCsvAsync(VProjetsEtEquipeDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    // 3. On aplatit les données dans une liste de dictionnaires
	    var exportData = (results.Content ?? new List<VProjetsEtEquipe>()).Select(item => new Dictionary<string, object>
	    {
{ "IdProjet", item.IdProjet },
{ "NomProjet", item.NomProjet },
{ "DateDebut", item.DateDebut },
{ "BudgetEstime", item.BudgetEstime },
{ "DureeJours", item.DureeJours },
{ "NombreParticipants", item.NombreParticipants },
{ "BudgetParParticipant", item.BudgetParParticipant },
{ "ListeEquipe", item.ListeEquipe },
{ "RolesDistribues", item.RolesDistribues },
{ "DerniereInscription", item.DerniereInscription },
	    }).ToList();

	    var csvContent = await _service.ExportToCsvAsync(exportData);
	    var bytes = Encoding.UTF8.GetBytes(csvContent);

	    Response.Headers["Content-Disposition"] = "attachment; filename=VProjetsEtEquipe.csv";

	    return File(bytes, "text/csv; charset=utf-8");
	}

	// GET: /VProjetsEtEquipe

	// GET: /VProjetsEtEquipe
	public async Task<IActionResult> Index(int page = 0, int pageSize = 10, string? sortOrder = null, string? sortBy = null)
	{
	    string finalSortOrder = !string.IsNullOrEmpty(sortBy)
	        ? WebUtils.AddSort(sortOrder, sortBy)
	        : sortOrder;

	    var results = await _service.GetAllAsync(page, pageSize, finalSortOrder);


	    var vm = new VProjetsEtEquipeViewModel
	    {
	        VProjetsEtEquipes = results.Content ?? new List<VProjetsEtEquipe>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        CurrentSort = finalSortOrder
	    };

	    return View(vm);
	}

	// POST: /VProjetsEtEquipe/Search
	[HttpPost]
	public async Task<IActionResult> Search(VProjetsEtEquipeDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    var vm = new VProjetsEtEquipeViewModel
	    {
	        VProjetsEtEquipes = results.Content ?? new List<VProjetsEtEquipe>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        SearchVProjetsEtEquipeCriteria = dto,
	        CurrentSort = sortOrder ?? string.Empty
	    };

	    return View("Index", vm);
	}

	// GET: /VProjetsEtEquipe/Details/idProjet
	public async Task<IActionResult> Details(int idProjet)
	{
	    var vProjetsEtEquipe = await _service.GetByIdAsync(idProjet);
	    if (vProjetsEtEquipe == null) return NotFound();


	    var vm = new VProjetsEtEquipeViewModel
	    {
	        VProjetsEtEquipe = vProjetsEtEquipe,
	    };

	    return View(vm);
	}







}
