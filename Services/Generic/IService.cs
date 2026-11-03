namespace TestMVCSharpC.Services.Generic;

using System.Linq.Expressions;
using System.Collections.Generic;
using TestMVCSharpC.Utils;

public interface IService<T, in TKey, in TSearch> where T : class
{
    Task<string> ExportToCsvAsync(IEnumerable<IDictionary<string, object>> data);
    Task<PagedResponse<T>> GetAllAsync(
        int page, int pageSize, string? sorting,
        params Expression<Func<T, object?>>[]? includeProperties);
    Task<PagedResponse<T>> SearchAsync(
        TSearch searchDto, int page, int pageSize, string? sorting);
    Task<T?> GetByIdAsync(TKey id, params Expression<Func<T, object?>>[]? includeProperties);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(TKey id);
    Task<bool> ExistsAsync(TKey id);
}
