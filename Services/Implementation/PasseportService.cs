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

public class PasseportService : BaseService<Passeport, int, PasseportDto>, IPasseportService
{
    private readonly IPasseportRepository _repository;

    public PasseportService(IPasseportRepository repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResponse<Passeport>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<Passeport, object?>>[]? includes)
    {
        try
        {
            return await _repository.SearchAsync(
                _ => true, page, pageSize, sorting, includes);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while retrieving all passeport", ex);
        }
    }

    public override async Task<PagedResponse<Passeport>> SearchAsync(
        PasseportDto searchDto,
        int page, int pageSize, string? sorting)
    {
        try
        {
            var predicate = new PasseportFilterBuilder().Build(searchDto).Combine();
            return await _repository.SearchAsync(predicate, page, pageSize, sorting,e => e.IdpersonnePersonne );
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while searching passeport", ex);
        }
    }  

    public override async Task<Passeport?> GetByIdAsync(int id, params Expression<Func<Passeport, object?>>[]? includeProperties)
    {
        try
        {
          return await _repository.GetByIdAsync(id, includeProperties);
        }
        catch (Exception ex)
        {
          throw new ResourceNotFoundException("Passeport not found with id : " + id);
        }
    }
      
    public override async Task AddAsync(Passeport passeport)
    {
        try
        {
          await _repository.AddAsync(passeport);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while creating passeport", ex);
        }
    }

     
    public override async Task UpdateAsync(Passeport passeport)
    {
        try
        {
          await _repository.UpdateAsync(passeport);
        }
        catch (Exception ex)
        {
          throw new InternalServerErrorException("Error while updating passeport", ex);
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
        throw new InternalServerErrorException("Error while deleting passeport", ex);
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
        throw new InternalServerErrorException("Error while checking the existence of passeport", ex);
      }
    }
}
