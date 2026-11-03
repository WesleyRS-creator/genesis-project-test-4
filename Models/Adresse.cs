namespace TestMVCSharpC.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

[Table("adresse")]
public class Adresse  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("id")]
	public int Id { get; set; }


	[Column("id_personne")]
	public int? IdPersonne { get; set; }
	[ForeignKey(nameof(IdPersonne))]
	public virtual Personne? IdpersonnePersonne { get; set; }


	[Column("latitude")]
	public double? Latitude { get; set; }

	[Column("code_postal")]
	[StringLength(10)]
	public string? CodePostal { get; set; }

	[Column("ville")]
	[StringLength(50)]
	public string? Ville { get; set; }

	[Column("rue")]
	[StringLength(100)]
	public string? Rue { get; set; }
	

}
