namespace TestMVCSharpC.Models.Dto;

using System.Text.Json.Serialization;

public class PasseportDto
{
	public DateOnly? DateExpiration { get; set; }
	public DateOnly? DateExpirationMin { get; set; }
	public DateOnly? DateExpirationMax { get; set; }
	

	public int? Id { get; set; }

	public int? IdPersonne { get; set; }
	public PersonneDto? IdpersonnePersonne { get; set; }


	public string? Numero { get; set; }
	

  public Passeport ToEntity() => new()
  {
    	DateExpiration = DateExpiration,
    	IdPersonne = IdpersonnePersonne?.Id
    	,
    	Numero = Numero
    	
  };

  public Passeport ToEntity(int id) => new()
  {
    	DateExpiration = DateExpiration,
    	Id = id
    ,
    	IdPersonne = IdpersonnePersonne?.Id
    	,
    	Numero = Numero
    	
  };
}
