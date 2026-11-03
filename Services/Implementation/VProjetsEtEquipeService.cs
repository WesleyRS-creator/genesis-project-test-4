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

public class VProjetsEtEquipeService : BaseService<VProjetsEtEquipe, int, VProjetsEtEquipeDto>, IVProjetsEtEquipeService
{
    private readonly IVProjetsEtEquipeRepository _repository;

    public VProjetsEtEquipeService(IVProjetsEtEquipeRepository repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResponse<VProjetsEtEquipe>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<VProjetsEtEquipe, object?>>[]? includes)
    {
        try
        {
            return await _repository.SearchAsync(
                _ => true, page, pageSize, sorting, includes);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while retrieving all v projets et equipe", ex);
        }
    }

    public override async Task<PagedResponse<VProjetsEtEquipe>> SearchAsync(
        VProjetsEtEquipeDto searchDto,
        int page, int pageSize, string? sorting)
    {
        try
        {
            var predicate = new VProjetsEtEquipeFilterBuilder().Build(searchDto).Combine();
            return await _repository.SearchAsync(predicate, page, pageSize, sorting );
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while searching v projets et equipe", ex);
        }
    }  

    public override async Task<VProjetsEtEquipe?> GetByIdAsync(int id, params Expression<Func<VProjetsEtEquipe, object?>>[]? includeProperties)
    {
        try
        {
          return await _repository.GetByIdAsync(id, includeProperties);
        }
        catch (Exception ex)
        {
          throw new ResourceNotFoundException("VProjetsEtEquipe not found with idProjet : " + id);
        }
    }
    
    public override async Task AddAsync(VProjetsEtEquipe vProjetsEtEquipe)
    {
        throw new NotSupportedException("Operation not supported for view-based entity.");
    }
    

    
    public override async Task UpdateAsync(VProjetsEtEquipe vProjetsEtEquipe)
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
        throw new InternalServerErrorException("Error while checking the existence of v projets et equipe", ex);
      }
    }
}
