namespace TestMVCSharpC.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

[Table("participer")]
public class Participer  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Column("id")]
	public int Id { get; set; }


	[Column("id_personne")]
	public int? IdPersonne { get; set; }
	[ForeignKey(nameof(IdPersonne))]
	public virtual Personne? IdpersonnePersonne { get; set; }


	[Column("id_projet")]
	public int? IdProjet { get; set; }
	[ForeignKey(nameof(IdProjet))]
	public virtual Projet? IdprojetProjet { get; set; }


	[Column("date_inscription", TypeName = "timestamp without time zone")]
	public DateTime? DateInscription  { get; set; }


	[Column("role_dans_projet")]
	[StringLength(50)]
	public string? RoleDansProjet { get; set; }
	

}
