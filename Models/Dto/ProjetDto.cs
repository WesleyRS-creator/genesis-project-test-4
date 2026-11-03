namespace TestMVCSharpC.Models.Dto;

using System.Text.Json.Serialization;

public class ProjetDto
{
	public double? BudgetEstime { get; set; }
	public double? BudgetEstimeMin { get; set; }
	public double? BudgetEstimeMax { get; set; }
	

	public DateOnly? DateDebut { get; set; }
	public DateOnly? DateDebutMin { get; set; }
	public DateOnly? DateDebutMax { get; set; }
	

	public int? Id { get; set; }

	public string? NomProjet { get; set; }
	

  public Projet ToEntity() => new()
  {
    	BudgetEstime = BudgetEstime,
    	DateDebut = DateDebut,
    	NomProjet = NomProjet
    	
  };

  public Projet ToEntity(int id) => new()
  {
    	BudgetEstime = BudgetEstime,
    	DateDebut = DateDebut,
    	Id = id
    ,
    	NomProjet = NomProjet
    	
  };
}
