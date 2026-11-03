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

public class ParticiperService : BaseService<Participer, int, ParticiperDto>, IParticiperService
{
    private readonly IParticiperRepository _repository;

    public ParticiperService(IParticiperRepository repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResponse<Participer>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<Participer, object?>>[]? includes)
    {
        try
        {
            return await _repository.SearchAsync(
                _ => true, page, pageSize, sorting, includes);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while retrieving all participer", ex);
        }
    }

    public override async Task<PagedResponse<Participer>> SearchAsync(
        ParticiperDto searchDto,
        int page, int pageSize, string? sorting)
    {
        try
        {
            var predicate = new ParticiperFilterBuilder().Build(searchDto).Combine();
            return await _repository.SearchAsync(predicate, page, pageSize, sorting,e => e.IdpersonnePersonne,e => e.IdprojetProjet );
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while searching participer", ex);
        }
    }  

    public override async Task<Participer?> GetByIdAsync(int id, params Expression<Func<Participer, object?>>[]? includeProperties)
    {
        try
        {
          return await _repository.GetByIdAsync(id, includeProperties);
        }
        catch (Exception ex)
        {
          throw new ResourceNotFoundException("Participer not found with id : " + id);
        }
    }
      
    public override async Task AddAsync(Participer participer)
    {
        try
        {
          await _repository.AddAsync(participer);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while creating participer", ex);
        }
    }

     
    public override async Task UpdateAsync(Participer participer)
    {
        try
        {
          await _repository.UpdateAsync(participer);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while updating participer", ex);
        }
    }

     
    public override async Task DeleteAsync(int id)
    {
      try 
      {
        await _repository.DeleteAsync(id);
      }
      catch (Exception ex)
      {
        throw new InternalServerErrorException("Error while deleting participer", ex);
      }
    }

    public override async Task<bool> ExistsAsync(int id)
    {
      try 
      {
        return await _repository.ExistsAsync(id);
      }
      catch (Exception ex)
      {
        throw new InternalServerErrorException("Error while checking the existence of participer", ex);
      }
    }
}
