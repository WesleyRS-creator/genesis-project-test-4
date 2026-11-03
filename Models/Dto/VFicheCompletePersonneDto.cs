namespace TestMVCSharpC.Models.Dto;

using System.Text.Json.Serialization;

public class VFicheCompletePersonneDto
{
	public int? IdPersonne { get; set; }

	public string? Nom { get; set; }

	public string? Prenom { get; set; }

	public DateOnly? DateNaissance { get; set; }
	public DateOnly? DateNaissanceMin { get; set; }
	public DateOnly? DateNaissanceMax { get; set; }
	

	public int? Age { get; set; }
	public int? AgeMin { get; set; }
	public int? AgeMax { get; set; }
	

	public string? Biographie { get; set; }

	public long? NumeroSs { get; set; }
	public long? NumeroSsMin { get; set; }
	public long? NumeroSsMax { get; set; }
	

	public string? NumeroPasseport { get; set; }

	public DateOnly? DateExpiration { get; set; }
	public DateOnly? DateExpirationMin { get; set; }
	public DateOnly? DateExpirationMax { get; set; }
	

	public string? StatutPasseport { get; set; }

	public long? NombreAdresses { get; set; }
	public long? NombreAdressesMin { get; set; }
	public long? NombreAdressesMax { get; set; }
	

	public string? VillePrincipale { get; set; }

	public long? NombreProjets { get; set; }
	public long? NombreProjetsMin { get; set; }
	public long? NombreProjetsMax { get; set; }
	
	

  public VFicheCompletePersonne ToEntity() => new()
  {
    	Nom = Nom,
    	Prenom = Prenom,
    	DateNaissance = DateNaissance,
    	Age = Age,
    	Biographie = Biographie,
    	NumeroSs = NumeroSs,
    	NumeroPasseport = NumeroPasseport,
    	DateExpiration = DateExpiration,
    	StatutPasseport = StatutPasseport,
    	NombreAdresses = NombreAdresses,
    	VillePrincipale = VillePrincipale,
    	NombreProjets = NombreProjets
    	
  };

  public VFicheCompletePersonne ToEntity(int id) => new()
  {
    	IdPersonne = id
    ,
    	Nom = Nom,
    	Prenom = Prenom,
    	DateNaissance = DateNaissance,
    	Age = Age,
    	Biographie = Biographie,
    	NumeroSs = NumeroSs,
    	NumeroPasseport = NumeroPasseport,
    	DateExpiration = DateExpiration,
    	StatutPasseport = StatutPasseport,
    	NombreAdresses = NombreAdresses,
    	VillePrincipale = VillePrincipale,
    	NombreProjets = NombreProjets
    	
  };
}
