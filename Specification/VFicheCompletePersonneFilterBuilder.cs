namespace TestMVCSharpC.Specification;

using System.Linq.Expressions;
using TestMVCSharpC.Models;
using TestMVCSharpC.Models.Dto;
using TestMVCSharpC.Utils;

public sealed class VFicheCompletePersonneFilterBuilder : FilterBuilder<VFicheCompletePersonne, VFicheCompletePersonneDto>
{
    public override VFicheCompletePersonneFilterBuilder Build(VFicheCompletePersonneDto dto)
    {
    	AddEquals(e => e.IdPersonne, dto.IdPersonne);

    	AddLike(e => e.Nom, dto.Nom);
    	

    	AddLike(e => e.Prenom, dto.Prenom);
    	

    	AddRange(e => e.DateNaissance,  dto.DateNaissanceMin,  dto.DateNaissanceMax);
    	AddEquals(e => e.DateNaissance, dto.DateNaissance);
    

    	AddRange(e => e.Age,  dto.AgeMin,  dto.AgeMax);
    	AddEquals(e => e.Age, dto.Age);
    

    	AddLike(e => e.Biographie, dto.Biographie);
    	

    	AddRange(e => e.NumeroSs,  dto.NumeroSsMin,  dto.NumeroSsMax);
    	AddEquals(e => e.NumeroSs, dto.NumeroSs);
    

    	AddLike(e => e.NumeroPasseport, dto.NumeroPasseport);
    	

    	AddRange(e => e.DateExpiration,  dto.DateExpirationMin,  dto.DateExpirationMax);
    	AddEquals(e => e.DateExpiration, dto.DateExpiration);
    

    	AddLike(e => e.StatutPasseport, dto.StatutPasseport);
    	

    	AddRange(e => e.NombreAdresses,  dto.NombreAdressesMin,  dto.NombreAdressesMax);
    	AddEquals(e => e.NombreAdresses, dto.NombreAdresses);
    

    	AddLike(e => e.VillePrincipale, dto.VillePrincipale);
    	

    	AddRange(e => e.NombreProjets,  dto.NombreProjetsMin,  dto.NombreProjetsMax);
    	AddEquals(e => e.NombreProjets, dto.NombreProjets);
    
    	
        return this;
    }
}
