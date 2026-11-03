namespace TestMVCSharpC.Models.Dto;

using System.Text.Json.Serialization;

public class ParticiperDto
{
	public int? Id { get; set; }

	public int? IdPersonne { get; set; }
	public PersonneDto? IdpersonnePersonne { get; set; }


	public int? IdProjet { get; set; }
	public ProjetDto? IdprojetProjet { get; set; }


	public DateTime? DateInscription { get; set; }
	public DateTime? DateInscriptionMin { get; set; }
	public DateTime? DateInscriptionMax { get; set; }
	

	public string? RoleDansProjet { get; set; }
	

  public Participer ToEntity() => new()
  {
    	IdPersonne = IdpersonnePersonne?.Id
    	,
    	IdProjet = IdprojetProjet?.Id
    	,
    	DateInscription = DateInscription,
    	RoleDansProjet = RoleDansProjet
    	
  };

  public Participer ToEntity(int id) => new()
  {
    	Id = id
    ,
    	IdPersonne = IdpersonnePersonne?.Id
    	,
    	IdProjet = IdprojetProjet?.Id
    	,
    	DateInscription = DateInscription,
    	RoleDansProjet = RoleDansProjet
    	
  };
}
