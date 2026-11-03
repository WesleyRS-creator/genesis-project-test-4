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

public class VSynthetiseGlobalService : BaseService<VSynthetiseGlobal, int, VSynthetiseGlobalDto>, IVSynthetiseGlobalService
{
    private readonly IVSynthetiseGlobalRepository _repository;

    public VSynthetiseGlobalService(IVSynthetiseGlobalRepository repository)
    {
        _repository = repository;
    }

    public override async Task<PagedResponse<VSynthetiseGlobal>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<VSynthetiseGlobal, object?>>[]? includes)
    {
        try
        {
            return await _repository.SearchAsync(
                _ => true, page, pageSize, sorting, includes);
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while retrieving all v synthetise global", ex);
        }
    }

    public override async Task<PagedResponse<VSynthetiseGlobal>> SearchAsync(
        VSynthetiseGlobalDto searchDto,
        int page, int pageSize, string? sorting)
    {
        try
        {
            var predicate = new VSynthetiseGlobalFilterBuilder().Build(searchDto).Combine();
            return await _repository.SearchAsync(predicate, page, pageSize, sorting );
        }
        catch (Exception ex)
        {
            throw new InternalServerErrorException("Error while searching v synthetise global", ex);
        }
    }  

    public override async Task<VSynthetiseGlobal?> GetByIdAsync(int id, params Expression<Func<VSynthetiseGlobal, object?>>[]? includeProperties)
    {
        try
        {
          return await _repository.GetByIdAsync(id, includeProperties);
        }
        catch (Exception ex)
        {
          throw new ResourceNotFoundException("VSynthetiseGlobal not found with idPersonne : " + id);
        }
    }
    
    public override async Task AddAsync(VSynthetiseGlobal vSynthetiseGlobal)
    {
        throw new NotSupportedException("Operation not supported for view-based entity.");
    }
    

    
    public override async Task UpdateAsync(VSynthetiseGlobal vSynthetiseGlobal)
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
        throw new InternalServerErrorException("Error while checking the existence of v synthetise global", ex);
      }
    }
}
