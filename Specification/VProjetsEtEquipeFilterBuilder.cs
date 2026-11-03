namespace TestMVCSharpC.Specification;

using System.Linq.Expressions;
using TestMVCSharpC.Models;
using TestMVCSharpC.Models.Dto;
using TestMVCSharpC.Utils;

public sealed class VProjetsEtEquipeFilterBuilder : FilterBuilder<VProjetsEtEquipe, VProjetsEtEquipeDto>
{
    public override VProjetsEtEquipeFilterBuilder Build(VProjetsEtEquipeDto dto)
    {
    	AddEquals(e => e.IdProjet, dto.IdProjet);

    	AddLike(e => e.NomProjet, dto.NomProjet);
    	

    	AddRange(e => e.DateDebut,  dto.DateDebutMin,  dto.DateDebutMax);
    	AddEquals(e => e.DateDebut, dto.DateDebut);
    

    	AddRange(e => e.BudgetEstime,  dto.BudgetEstimeMin,  dto.BudgetEstimeMax);
    	AddEquals(e => e.BudgetEstime, dto.BudgetEstime);
    

    	AddRange(e => e.DureeJours,  dto.DureeJoursMin,  dto.DureeJoursMax);
    	AddEquals(e => e.DureeJours, dto.DureeJours);
    

    	AddRange(e => e.NombreParticipants,  dto.NombreParticipantsMin,  dto.NombreParticipantsMax);
    	AddEquals(e => e.NombreParticipants, dto.NombreParticipants);
    

    	AddRange(e => e.BudgetParParticipant,  dto.BudgetParParticipantMin,  dto.BudgetParParticipantMax);
    	AddEquals(e => e.BudgetParParticipant, dto.BudgetParParticipant);
    

    	AddLike(e => e.ListeEquipe, dto.ListeEquipe);
    	

    	AddLike(e => e.RolesDistribues, dto.RolesDistribues);
    	

    	AddRange(e => e.DerniereInscription,  dto.DerniereInscriptionMin,  dto.DerniereInscriptionMax);
    	AddEquals(e => e.DerniereInscription, dto.DerniereInscription);
    
    	
        return this;
    }
}
