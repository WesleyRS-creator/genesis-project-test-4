namespace TestMVCSharpC.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

[Table("passeport")]
public class Passeport  
{
	[Column("date_expiration")]
	public DateOnly? DateExpiration { get; set; }

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("id")]
	public int Id { get; set; }


	[Column("id_personne")]
	[Required(ErrorMessage = "IdPersonne cannot be null")]
	public int? IdPersonne { get; set; }
	[ForeignKey(nameof(IdPersonne))]
	public virtual Personne? IdpersonnePersonne { get; set; }


	[Column("numero")]
	[StringLength(20)]
	public string? Numero { get; set; }
	

}
