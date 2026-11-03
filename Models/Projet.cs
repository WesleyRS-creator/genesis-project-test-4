namespace TestMVCSharpC.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

[Table("projet")]
public class Projet  
{
	[Column("budget_estime")]
	public double? BudgetEstime { get; set; }

	[Column("date_debut")]
	public DateOnly? DateDebut { get; set; }

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("id")]
	public int Id { get; set; }


	[Column("nom_projet")]
	[StringLength(100)]
	public string? NomProjet { get; set; }
	

}
