namespace TestMVCSharpC.Repositories.Generic;

using System.Linq.Expressions;
using TestMVCSharpC.Utils;

public interface IRepository<T, in TKey> where T : class
{
    Task<IEnumerable<T>> GetAllWithIncludes(int page, int pageSize, string? sorting, params Expression<Func<T, object?>>[]? includeProperties);
    Task<PagedResponse<T>> SearchAsync(
        Expression<Func<T, bool>> predicate,
        int page,
        int pageSize,
        string? sorting,
        params Expression<Func<T, object?>>[]? includes);
    Task<T?> GetByIdAsync(TKey id, params Expression<Func<T, object?>>[]? includeProperties);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(TKey id);
    Task<bool> ExistsAsync(TKey id);
    Task<long> GetTotalCountAsync();
}
