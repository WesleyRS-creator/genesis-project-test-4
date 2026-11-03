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

public class ProjetService : BaseService<Projet, int, ProjetDto>, IProjetService
{
    private readonly IProjetRepository _repository;

    public ProjetService(IProjetRepository repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResponse<Projet>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<Projet, object?>>[]? includes)
    {
        try
        {
            return await _repository.SearchAsync(
                _ => true, page, pageSize, sorting, includes);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while retrieving all projet", ex);
        }
    }

    public override async Task<PagedResponse<Projet>> SearchAsync(
        ProjetDto searchDto,
        int page, int pageSize, string? sorting)
    {
        try
        {
            var predicate = new ProjetFilterBuilder().Build(searchDto).Combine();
            return await _repository.SearchAsync(predicate, page, pageSize, sorting );
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while searching projet", ex);
        }
    }  

    public override async Task<Projet?> GetByIdAsync(int id, params Expression<Func<Projet, object?>>[]? includeProperties)
    {
        try
        {
          return await _repository.GetByIdAsync(id, includeProperties);
        }
        catch (Exception ex)
        {
          throw new ResourceNotFoundException("Projet not found with id : " + id);
        }
    }
      
    public override async Task AddAsync(Projet projet)
    {
        try
        {
          await _repository.AddAsync(projet);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while creating projet", ex);
        }
    }

     
    public override async Task UpdateAsync(Projet projet)
    {
        try
        {
          await _repository.UpdateAsync(projet);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while updating projet", ex);
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
        throw new InternalServerErrorException("Error while deleting projet", ex);
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
        throw new InternalServerErrorException("Error while checking the existence of projet", ex);
      }
    }
}
