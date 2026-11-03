namespace TestMVCSharpC.Specification;

using System.Linq.Expressions;
using TestMVCSharpC.Models;
using TestMVCSharpC.Models.Dto;
using TestMVCSharpC.Utils;

public sealed class PasseportFilterBuilder : FilterBuilder<Passeport, PasseportDto>
{
    public override PasseportFilterBuilder Build(PasseportDto dto)
    {
    	AddRange(e => e.DateExpiration,  dto.DateExpirationMin,  dto.DateExpirationMax);
    	AddEquals(e => e.DateExpiration, dto.DateExpiration);
    

    	AddEquals(e => e.Id, dto.Id);

    	AddEquals(e => e.IdPersonne, dto.IdPersonne);  
    	if (dto.IdpersonnePersonne is { } idpersonnePersonne)
            	AddEquals(e => e.IdPersonne, idpersonnePersonne.Id);
    

    	AddLike(e => e.Numero, dto.Numero);
    	
    	
        return this;
    }
}
