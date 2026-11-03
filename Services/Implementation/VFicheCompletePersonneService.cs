using System.Collections.Generic;
using System.Linq.Expressions;
using TestMVCSharpC.Exceptions;
using TestMVCSharpC.Models;
using TestMVCSharpC.Repositories.Interface;
using TestMVCSharpC.Repositories.Implementation;
using TestMVCSharpC.Services.Interface;
using TestMVCSharpC.Services.Generic;
using TestMVCSharpC.Specification;
using TestMVCSharpC.Models.Dto;
using TestMVCSharpC.Utils;

namespace TestMVCSharpC.Services.Implementation;

public class VFicheCompletePersonneService : BaseService<VFicheCompletePersonne, int, VFicheCompletePersonneDto>, IVFicheCompletePersonneService
{
    private readonly IVFicheCompletePersonneRepository _repository;

    public VFicheCompletePersonneService(IVFicheCompletePersonneRepository repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResponse<VFicheCompletePersonne>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<VFicheCompletePersonne, object?>>[]? includes)
    {
        try
        {
            return await _repository.SearchAsync(
                _ => true, page, pageSize, sorting, includes);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while retrieving all v fiche complete personne", ex);
        }
    }

    public override async Task<PagedResponse<VFicheCompletePersonne>> SearchAsync(
        VFicheCompletePersonneDto searchDto,
        int page, int pageSize, string? sorting)
    {
        try
        {
            var predicate = new VFicheCompletePersonneFilterBuilder().Build(searchDto).Combine();
            return await _repository.SearchAsync(predicate, page, pageSize, sorting );
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while searching v fiche complete personne", ex);
        }
    }  

    public override async Task<VFicheCompletePersonne?> GetByIdAsync(int id, params Expression<Func<VFicheCompletePersonne, object?>>[]? includeProperties)
    {
        try
        {
          return await _repository.GetByIdAsync(id, includeProperties);
        }
        catch (Exception ex)
        {
          throw new ResourceNotFoundException("VFicheCompletePersonne not found with idPersonne : " + id);
        }
    }
    
    public override async Task AddAsync(VFicheCompletePersonne vFicheCompletePersonne)
    {
        throw new NotSupportedException("Operation not supported for view-based entity.");
    }
    

    
    public override async Task UpdateAsync(VFicheCompletePersonne vFicheCompletePersonne)
    {
        throw new NotSupportedException("Operation not supported for view-based entity.");
    }
    

    
    public override async Task DeleteAsync(int id)
    {
        throw new NotSupportedException("Operation not supported for view-based entity.");
    }
    

    public override async Task<bool> ExistsAsync(int id)
    {
      try 
      {
        return await _repository.ExistsAsync(id);
      }
      catch (Exception ex)
      {
        throw new InternalServerErrorException("Error while checking the existence of v fiche complete personne", ex);
      }
    }
}
