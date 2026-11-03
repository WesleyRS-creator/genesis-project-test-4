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


public class VFicheCompletePersonneController  : Controller 
{
	private readonly IVFicheCompletePersonneService _service;
	

	public VFicheCompletePersonneController(IVFicheCompletePersonneService service)
	{
	   _service = service;
	   
	}

	// POST: /VFicheCompletePersonne/ExportToCsv
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ExportToCsvAsync(VFicheCompletePersonneDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    // 3. On aplatit les données dans une liste de dictionnaires
	    var exportData = (results.Content ?? new List<VFicheCompletePersonne>()).Select(item => new Dictionary<string, object>
	    {
{ "IdPersonne", item.IdPersonne },
{ "Nom", item.Nom },
{ "Prenom", item.Prenom },
{ "DateNaissance", item.DateNaissance },
{ "Age", item.Age },
{ "Biographie", item.Biographie },
{ "NumeroSs", item.NumeroSs },
{ "NumeroPasseport", item.NumeroPasseport },
{ "DateExpiration", item.DateExpiration },
{ "StatutPasseport", item.StatutPasseport },
{ "NombreAdresses", item.NombreAdresses },
{ "VillePrincipale", item.VillePrincipale },
{ "NombreProjets", item.NombreProjets },
	    }).ToList();

	    var csvContent = await _service.ExportToCsvAsync(exportData);
	    var bytes = Encoding.UTF8.GetBytes(csvContent);

	    Response.Headers["Content-Disposition"] = "attachment; filename=VFicheCompletePersonne.csv";

	    return File(bytes, "text/csv; charset=utf-8");
	}

	// GET: /VFicheCompletePersonne

	// GET: /VFicheCompletePersonne
	public async Task<IActionResult> Index(int page = 0, int pageSize = 10, string? sortOrder = null, string? sortBy = null)
	{
	    string finalSortOrder = !string.IsNullOrEmpty(sortBy)
	        ? WebUtils.AddSort(sortOrder, sortBy)
	        : sortOrder;

	    var results = await _service.GetAllAsync(page, pageSize, finalSortOrder);


	    var vm = new VFicheCompletePersonneViewModel
	    {
	        VFicheCompletePersonnes = results.Content ?? new List<VFicheCompletePersonne>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        CurrentSort = finalSortOrder
	    };

	    return View(vm);
	}

	// POST: /VFicheCompletePersonne/Search
	[HttpPost]
	public async Task<IActionResult> Search(VFicheCompletePersonneDto dto, int page = 0, int pageSize = 10, string? sortOrder = null)
	{
	    var results = await _service.SearchAsync(dto, page, pageSize, sortOrder);


	    var vm = new VFicheCompletePersonneViewModel
	    {
	        VFicheCompletePersonnes = results.Content ?? new List<VFicheCompletePersonne>(),
	        Page = results.Page ?? new PageInfo { Number = page, Size = pageSize },
	        SearchVFicheCompletePersonneCriteria = dto,
	        CurrentSort = sortOrder ?? string.Empty
	    };

	    return View("Index", vm);
	}

	// GET: /VFicheCompletePersonne/Details/idPersonne
	public async Task<IActionResult> Details(int idPersonne)
	{
	    var vFicheCompletePersonne = await _service.GetByIdAsync(idPersonne);
	    if (vFicheCompletePersonne == null) return NotFound();


	    var vm = new VFicheCompletePersonneViewModel
	    {
	        VFicheCompletePersonne = vFicheCompletePersonne,
	    };

	    return View(vm);
	}







}
