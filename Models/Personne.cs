namespace TestMVCSharpC.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

[Table("personne")]
public class Personne  
{
	[Column("date_naissance")]
	public DateOnly? DateNaissance { get; set; }

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("id")]
	public int Id { get; set; }


	[Column("numero_ss")]
	public long? NumeroSs { get; set; }

	[Column("nom")]
	[StringLength(50)]
	public string? Nom { get; set; }

	[Column("prenom")]
	[StringLength(50)]
	public string? Prenom { get; set; }

	[Column("biographie")]
	[StringLength(255)]
	public string? Biographie { get; set; }
	

}
