namespace TestMVCSharpC.Specification;

using System.Linq.Expressions;
using TestMVCSharpC.Models;
using TestMVCSharpC.Models.Dto;
using TestMVCSharpC.Utils;

public sealed class ParticiperFilterBuilder : FilterBuilder<Participer, ParticiperDto>
{
    public override ParticiperFilterBuilder Build(ParticiperDto dto)
    {
    	AddEquals(e => e.Id, dto.Id);

    	AddEquals(e => e.IdPersonne, dto.IdPersonne);  
    	if (dto.IdpersonnePersonne is { } idpersonnePersonne)
            	AddEquals(e => e.IdPersonne, idpersonnePersonne.Id);
    

    	AddEquals(e => e.IdProjet, dto.IdProjet);  
    	if (dto.IdprojetProjet is { } idprojetProjet)
            	AddEquals(e => e.IdProjet, idprojetProjet.Id);
    

    	AddRange(e => e.DateInscription,  dto.DateInscriptionMin,  dto.DateInscriptionMax);
    	AddEquals(e => e.DateInscription, dto.DateInscription);
    

    	AddLike(e => e.RoleDansProjet, dto.RoleDansProjet);
    	
    	
        return this;
    }
}
