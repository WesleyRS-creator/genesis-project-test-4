namespace TestMVCSharpC.Specification;

using System.Linq.Expressions;
using TestMVCSharpC.Models;
using TestMVCSharpC.Models.Dto;
using TestMVCSharpC.Utils;

public sealed class VSynthetiseGlobalFilterBuilder : FilterBuilder<VSynthetiseGlobal, VSynthetiseGlobalDto>
{
    public override VSynthetiseGlobalFilterBuilder Build(VSynthetiseGlobalDto dto)
    {
    	AddEquals(e => e.IdPersonne, dto.IdPersonne);

    	AddLike(e => e.NomComplet, dto.NomComplet);
    	

    	AddLike(e => e.TrancheAge, dto.TrancheAge);
    	

    	AddRange(e => e.TotalParticipations,  dto.TotalParticipationsMin,  dto.TotalParticipationsMax);
    	AddEquals(e => e.TotalParticipations, dto.TotalParticipations);
    

    	AddLike(e => e.RolePrincipal, dto.RolePrincipal);
    	

    	AddRange(e => e.BudgetMoyenProjets,  dto.BudgetMoyenProjetsMin,  dto.BudgetMoyenProjetsMax);
    	AddEquals(e => e.BudgetMoyenProjets, dto.BudgetMoyenProjets);
    

    	AddRange(e => e.BudgetGlobalMoyen,  dto.BudgetGlobalMoyenMin,  dto.BudgetGlobalMoyenMax);
    	AddEquals(e => e.BudgetGlobalMoyen, dto.BudgetGlobalMoyen);
    

    	AddLike(e => e.PositionBudget, dto.PositionBudget);
    	

    	AddRange(e => e.NombreVilles,  dto.NombreVillesMin,  dto.NombreVillesMax);
    	AddEquals(e => e.NombreVilles, dto.NombreVilles);
    

    	AddRange(e => e.LatitudeMoyenne,  dto.LatitudeMoyenneMin,  dto.LatitudeMoyenneMax);
    	AddEquals(e => e.LatitudeMoyenne, dto.LatitudeMoyenne);
    
    	
        return this;
    }
}
