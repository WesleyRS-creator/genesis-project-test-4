namespace TestMVCSharpC.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

[Table("v_projets_et_equipes")]
public class VProjetsEtEquipe  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("id_projet")]
	public int IdProjet { get; set; }


	[Column("nom_projet")]
	[StringLength(100)]
	public string? NomProjet { get; set; }

	[Column("date_debut")]
	public DateOnly? DateDebut { get; set; }

	[Column("budget_estime")]
	public double? BudgetEstime { get; set; }

	[Column("duree_jours")]
	public int? DureeJours { get; set; }

	[Column("nombre_participants")]
	public long? NombreParticipants { get; set; }

	[Column("budget_par_participant")]
	public double? BudgetParParticipant { get; set; }

	[Column("liste_equipe")]
	[StringLength(2147483647)]
	public string? ListeEquipe { get; set; }

	[Column("roles_distribues")]
	[StringLength(2147483647)]
	public string? RolesDistribues { get; set; }

	[Column("derniere_inscription", TypeName = "timestamp without time zone")]
	public DateTime? DerniereInscription  { get; set; }

	

}
