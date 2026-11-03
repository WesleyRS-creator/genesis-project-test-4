namespace TestMVCSharpC.Specification;

using System.Linq.Expressions;
using TestMVCSharpC.Models;
using TestMVCSharpC.Models.Dto;
using TestMVCSharpC.Utils;

public sealed class ProjetFilterBuilder : FilterBuilder<Projet, ProjetDto>
{
    public override ProjetFilterBuilder Build(ProjetDto dto)
    {
    	AddRange(e => e.BudgetEstime,  dto.BudgetEstimeMin,  dto.BudgetEstimeMax);
    	AddEquals(e => e.BudgetEstime, dto.BudgetEstime);
    

    	AddRange(e => e.DateDebut,  dto.DateDebutMin,  dto.DateDebutMax);
    	AddEquals(e => e.DateDebut, dto.DateDebut);
    

    	AddEquals(e => e.Id, dto.Id);

    	AddLike(e => e.NomProjet, dto.NomProjet);
    	
    	
        return this;
    }
}
