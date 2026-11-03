namespace TestMVCSharpC.Models.Dto;

using System.Text.Json.Serialization;

public class VProjetsEtEquipeDto
{
	public int? IdProjet { get; set; }

	public string? NomProjet { get; set; }

	public DateOnly? DateDebut { get; set; }
	public DateOnly? DateDebutMin { get; set; }
	public DateOnly? DateDebutMax { get; set; }
	

	public double? BudgetEstime { get; set; }
	public double? BudgetEstimeMin { get; set; }
	public double? BudgetEstimeMax { get; set; }
	

	public int? DureeJours { get; set; }
	public int? DureeJoursMin { get; set; }
	public int? DureeJoursMax { get; set; }
	

	public long? NombreParticipants { get; set; }
	public long? NombreParticipantsMin { get; set; }
	public long? NombreParticipantsMax { get; set; }
	

	public double? BudgetParParticipant { get; set; }
	public double? BudgetParParticipantMin { get; set; }
	public double? BudgetParParticipantMax { get; set; }
	

	public string? ListeEquipe { get; set; }

	public string? RolesDistribues { get; set; }

	public DateTime? DerniereInscription { get; set; }
	public DateTime? DerniereInscriptionMin { get; set; }
	public DateTime? DerniereInscriptionMax { get; set; }
	
	

  public VProjetsEtEquipe ToEntity() => new()
  {
    	NomProjet = NomProjet,
    	DateDebut = DateDebut,
    	BudgetEstime = BudgetEstime,
    	DureeJours = DureeJours,
    	NombreParticipants = NombreParticipants,
    	BudgetParParticipant = BudgetParParticipant,
    	ListeEquipe = ListeEquipe,
    	RolesDistribues = RolesDistribues,
    	DerniereInscription = DerniereInscription
    	
  };

  public VProjetsEtEquipe ToEntity(int id) => new()
  {
    	IdProjet = id
    ,
    	NomProjet = NomProjet,
    	DateDebut = DateDebut,
    	BudgetEstime = BudgetEstime,
    	DureeJours = DureeJours,
    	NombreParticipants = NombreParticipants,
    	BudgetParParticipant = BudgetParParticipant,
    	ListeEquipe = ListeEquipe,
    	RolesDistribues = RolesDistribues,
    	DerniereInscription = DerniereInscription
    	
  };
}
