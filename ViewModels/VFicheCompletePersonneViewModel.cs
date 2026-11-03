namespace TestMVCSharpC.ViewModels;

using System.ComponentModel.DataAnnotations;
using Models;
using Models.Dto;
using TestMVCSharpC.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

public class VFicheCompletePersonneViewModel
{
    public VFicheCompletePersonne? VFicheCompletePersonne { get; set; }
  
    public VFicheCompletePersonneDto? SearchVFicheCompletePersonneCriteria { get; set; }

    


    public List<VFicheCompletePersonne> VFicheCompletePersonnes { get; set; } = new List<VFicheCompletePersonne>();
    public PageInfo Page { get; set; } = new PageInfo();

    

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
