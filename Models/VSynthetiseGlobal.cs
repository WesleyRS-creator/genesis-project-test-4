namespace TestMVCSharpC.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

[Table("v_synthetise_global")]
public class VSynthetiseGlobal  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("id_personne")]
	public int IdPersonne { get; set; }


	[Column("nom_complet")]
	[StringLength(2147483647)]
	public string? NomComplet { get; set; }

	[Column("tranche_age")]
	[StringLength(2147483647)]
	public string? TrancheAge { get; set; }

	[Column("total_participations")]
	public long? TotalParticipations { get; set; }

	[Column("role_principal")]
	[StringLength(50)]
	public string? RolePrincipal { get; set; }

	[Column("budget_moyen_projets")]
	public double? BudgetMoyenProjets { get; set; }

	[Column("budget_global_moyen")]
	public double? BudgetGlobalMoyen { get; set; }

	[Column("position_budget")]
	[StringLength(2147483647)]
	public string? PositionBudget { get; set; }

	[Column("nombre_villes")]
	public long? NombreVilles { get; set; }

	[Column("latitude_moyenne")]
	public double? LatitudeMoyenne { get; set; }
	

}
