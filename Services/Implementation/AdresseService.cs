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

public class AdresseService : BaseService<Adresse, int, AdresseDto>, IAdresseService
{
    private readonly IAdresseRepository _repository;

    public AdresseService(IAdresseRepository repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResponse<Adresse>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<Adresse, object?>>[]? includes)
    {
        try
        {
            return await _repository.SearchAsync(
                _ => true, page, pageSize, sorting, includes);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while retrieving all adresse", ex);
        }
    }

    public override async Task<PagedResponse<Adresse>> SearchAsync(
        AdresseDto searchDto,
        int page, int pageSize, string? sorting)
    {
        try
        {
            var predicate = new AdresseFilterBuilder().Build(searchDto).Combine();
            return await _repository.SearchAsync(predicate, page, pageSize, sorting,e => e.IdpersonnePersonne );
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while searching adresse", ex);
        }
    }  

    public override async Task<Adresse?> GetByIdAsync(int id, params Expression<Func<Adresse, object?>>[]? includeProperties)
    {
        try
        {
          return await _repository.GetByIdAsync(id, includeProperties);
        }
        catch (Exception ex)
        {
          throw new ResourceNotFoundException("Adresse not found with id : " + id);
        }
    }
      
    public override async Task AddAsync(Adresse adresse)
    {
        try
        {
          await _repository.AddAsync(adresse);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while creating adresse", ex);
        }
    }

     
    public override async Task UpdateAsync(Adresse adresse)
    {
        try
        {
          await _repository.UpdateAsync(adresse);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while updating adresse", ex);
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
        throw new InternalServerErrorException("Error while deleting adresse", ex);
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
        throw new InternalServerErrorException("Error while checking the existence of adresse", ex);
      }
    }
}
