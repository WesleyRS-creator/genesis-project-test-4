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

public class PersonneService : BaseService<Personne, int, PersonneDto>, IPersonneService
{
    private readonly IPersonneRepository _repository;

    public PersonneService(IPersonneRepository repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResponse<Personne>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<Personne, object?>>[]? includes)
    {
        try
        {
            return await _repository.SearchAsync(
                _ => true, page, pageSize, sorting, includes);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while retrieving all personne", ex);
        }
    }

    public override async Task<PagedResponse<Personne>> SearchAsync(
        PersonneDto searchDto,
        int page, int pageSize, string? sorting)
    {
        try
        {
            var predicate = new PersonneFilterBuilder().Build(searchDto).Combine();
            return await _repository.SearchAsync(predicate, page, pageSize, sorting );
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while searching personne", ex);
        }
    }  

    public override async Task<Personne?> GetByIdAsync(int id, params Expression<Func<Personne, object?>>[]? includeProperties)
    {
        try
        {
          return await _repository.GetByIdAsync(id, includeProperties);
        }
        catch (Exception ex)
        {
          throw new ResourceNotFoundException("Personne not found with id : " + id);
        }
    }
      
    public override async Task AddAsync(Personne personne)
    {
        try
        {
          await _repository.AddAsync(personne);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while creating personne", ex);
        }
    }

     
    public override async Task UpdateAsync(Personne personne)
    {
        try
        {
          await _repository.UpdateAsync(personne);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while updating personne", ex);
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
        throw new InternalServerErrorException("Error while deleting personne", ex);
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
        throw new InternalServerErrorException("Error while checking the existence of personne", ex);
      }
    }
}
