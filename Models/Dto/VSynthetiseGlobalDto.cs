namespace TestMVCSharpC.Models.Dto;

using System.Text.Json.Serialization;

public class VSynthetiseGlobalDto
{
	public int? IdPersonne { get; set; }

	public string? NomComplet { get; set; }

	public string? TrancheAge { get; set; }

	public long? TotalParticipations { get; set; }
	public long? TotalParticipationsMin { get; set; }
	public long? TotalParticipationsMax { get; set; }
	

	public string? RolePrincipal { get; set; }

	public double? BudgetMoyenProjets { get; set; }
	public double? BudgetMoyenProjetsMin { get; set; }
	public double? BudgetMoyenProjetsMax { get; set; }
	

	public double? BudgetGlobalMoyen { get; set; }
	public double? BudgetGlobalMoyenMin { get; set; }
	public double? BudgetGlobalMoyenMax { get; set; }
	

	public string? PositionBudget { get; set; }

	public long? NombreVilles { get; set; }
	public long? NombreVillesMin { get; set; }
	public long? NombreVillesMax { get; set; }
	

	public double? LatitudeMoyenne { get; set; }
	public double? LatitudeMoyenneMin { get; set; }
	public double? LatitudeMoyenneMax { get; set; }
	
	

  public VSynthetiseGlobal ToEntity() => new()
  {
    	NomComplet = NomComplet,
    	TrancheAge = TrancheAge,
    	TotalParticipations = TotalParticipations,
    	RolePrincipal = RolePrincipal,
    	BudgetMoyenProjets = BudgetMoyenProjets,
    	BudgetGlobalMoyen = BudgetGlobalMoyen,
    	PositionBudget = PositionBudget,
    	NombreVilles = NombreVilles,
    	LatitudeMoyenne = LatitudeMoyenne
    	
  };

  public VSynthetiseGlobal ToEntity(int id) => new()
  {
    	IdPersonne = id
    ,
    	NomComplet = NomComplet,
    	TrancheAge = TrancheAge,
    	TotalParticipations = TotalParticipations,
    	RolePrincipal = RolePrincipal,
    	BudgetMoyenProjets = BudgetMoyenProjets,
    	BudgetGlobalMoyen = BudgetGlobalMoyen,
    	PositionBudget = PositionBudget,
    	NombreVilles = NombreVilles,
    	LatitudeMoyenne = LatitudeMoyenne
    	
  };
}
