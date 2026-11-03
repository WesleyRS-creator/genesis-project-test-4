namespace TestMVCSharpC.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

[Table("v_fiche_complete_personne")]
public class VFicheCompletePersonne  
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Column("id_personne")]
	public int IdPersonne { get; set; }


	[Column("nom")]
	[StringLength(50)]
	public string? Nom { get; set; }

	[Column("prenom")]
	[StringLength(50)]
	public string? Prenom { get; set; }

	[Column("date_naissance")]
	public DateOnly? DateNaissance { get; set; }

	[Column("age")]
	public int? Age { get; set; }

	[Column("biographie")]
	[StringLength(255)]
	public string? Biographie { get; set; }

	[Column("numero_ss")]
	public long? NumeroSs { get; set; }

	[Column("numero_passeport")]
	[StringLength(20)]
	public string? NumeroPasseport { get; set; }

	[Column("date_expiration")]
	public DateOnly? DateExpiration { get; set; }

	[Column("statut_passeport")]
	[StringLength(2147483647)]
	public string? StatutPasseport { get; set; }

	[Column("nombre_adresses")]
	public long? NombreAdresses { get; set; }

	[Column("ville_principale")]
	[StringLength(2147483647)]
	public string? VillePrincipale { get; set; }

	[Column("nombre_projets")]
	public long? NombreProjets { get; set; }
	

}
