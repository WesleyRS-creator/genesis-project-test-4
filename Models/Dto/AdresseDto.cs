namespace TestMVCSharpC.Models.Dto;

using System.Text.Json.Serialization;

public class AdresseDto
{
	public int? Id { get; set; }

	public int? IdPersonne { get; set; }
	public PersonneDto? IdpersonnePersonne { get; set; }


	public double? Latitude { get; set; }
	public double? LatitudeMin { get; set; }
	public double? LatitudeMax { get; set; }
	

	public string? CodePostal { get; set; }

	public string? Ville { get; set; }

	public string? Rue { get; set; }
	

  public Adresse ToEntity() => new()
  {
    	IdPersonne = IdpersonnePersonne?.Id
    	,
    	Latitude = Latitude,
    	CodePostal = CodePostal,
    	Ville = Ville,
    	Rue = Rue
    	
  };

  public Adresse ToEntity(int id) => new()
  {
    	Id = id
    ,
    	IdPersonne = IdpersonnePersonne?.Id
    	,
    	Latitude = Latitude,
    	CodePostal = CodePostal,
    	Ville = Ville,
    	Rue = Rue
    	
  };
}
