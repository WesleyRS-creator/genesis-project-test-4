namespace TestMVCSharpC.Models.Dto;

using System.Text.Json.Serialization;

public class PersonneDto
{
	public DateOnly? DateNaissance { get; set; }
	public DateOnly? DateNaissanceMin { get; set; }
	public DateOnly? DateNaissanceMax { get; set; }
	

	public int? Id { get; set; }

	public long? NumeroSs { get; set; }
	public long? NumeroSsMin { get; set; }
	public long? NumeroSsMax { get; set; }
	

	public string? Nom { get; set; }

	public string? Prenom { get; set; }

	public string? Biographie { get; set; }
	

  public Personne ToEntity() => new()
  {
    	DateNaissance = DateNaissance,
    	NumeroSs = NumeroSs,
    	Nom = Nom,
    	Prenom = Prenom,
    	Biographie = Biographie
    	
  };

  public Personne ToEntity(int id) => new()
  {
    	DateNaissance = DateNaissance,
    	Id = id
    ,
    	NumeroSs = NumeroSs,
    	Nom = Nom,
    	Prenom = Prenom,
    	Biographie = Biographie
    	
  };
}
