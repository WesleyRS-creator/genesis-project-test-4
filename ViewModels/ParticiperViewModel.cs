namespace TestMVCSharpC.ViewModels;

using System.ComponentModel.DataAnnotations;
using Models;
using Models.Dto;
using TestMVCSharpC.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

public class ParticiperViewModel
{
    public Participer? Participer { get; set; }
  
    public ParticiperDto? SearchParticiperCriteria { get; set; }

    public string PersonneDisplayColumn { get; set; } = string.Empty;
    public string PersonnePkColumn { get; set; } = string.Empty;
public string ProjetDisplayColumn { get; set; } = string.Empty;
    public string ProjetPkColumn { get; set; } = string.Empty;


    public List<Participer> Participers { get; set; } = new List<Participer>();
    public PageInfo Page { get; set; } = new PageInfo();

    public IEnumerable<SelectListItem> PersonnesSelectList { get; set; } = new List<SelectListItem>();
public IEnumerable<SelectListItem> ProjetsSelectList { get; set; } = new List<SelectListItem>();

    public string CurrentSort { get; set; } = string.Empty;
    public string CurrentSortDisplay
    {
        get
        {
            if (string.IsNullOrWhiteSpace(CurrentSort))
            return "default";
            
            var criteria = CurrentSort
                .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(s => s.Trim())
                .Where(s => s.Contains(","))
                .Select(s =>
                {
                    var parts = s.Split(',', 2);
                    if (parts.Length != 2) return s;

                    string field = parts[0].Trim();
                    string dir   = parts[1].Trim().ToLower();

                    string dirText  = dir == "asc" ? "asc" : (dir == "desc" ? "desc" : dir);
                                                                                                                          
                    return $"{field} {dirText}";
                });
    
            return string.Join(" → ", criteria);
        }
    }
}
