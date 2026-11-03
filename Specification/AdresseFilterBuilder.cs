namespace TestMVCSharpC.Specification;

using System.Linq.Expressions;
using TestMVCSharpC.Models;
using TestMVCSharpC.Models.Dto;
using TestMVCSharpC.Utils;

public sealed class AdresseFilterBuilder : FilterBuilder<Adresse, AdresseDto>
{
    public override AdresseFilterBuilder Build(AdresseDto dto)
    {
    	AddEquals(e => e.Id, dto.Id);

    	AddEquals(e => e.IdPersonne, dto.IdPersonne);  
    	if (dto.IdpersonnePersonne is { } idpersonnePersonne)
            	AddEquals(e => e.IdPersonne, idpersonnePersonne.Id);
    

    	AddRange(e => e.Latitude,  dto.LatitudeMin,  dto.LatitudeMax);
    	AddEquals(e => e.Latitude, dto.Latitude);
    

    	AddLike(e => e.CodePostal, dto.CodePostal);
    	

    	AddLike(e => e.Ville, dto.Ville);
    	

    	AddLike(e => e.Rue, dto.Rue);
    	
    	
        return this;
    }
}
