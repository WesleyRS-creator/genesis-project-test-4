namespace TestMVCSharpC.Services.Generic;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestMVCSharpC.Utils;

public abstract class BaseService<T, TKey, TSearch> : IService<T, TKey, TSearch> where T : class
{
    // Implémentation générique de l'export CSV
    public virtual Task<string> ExportToCsvAsync(IEnumerable<IDictionary<string, object>> data)
    {
        var csv = ExportUtil.GenerateCsv(data);
        return Task.FromResult(csv);
    }
    
    // Déclaration abstraite des autres méthodes qui devront être implémentées par les services enfants
    public abstract Task<PagedResponse<T>> GetAllAsync(int page, int pageSize, string? sorting, params Expression<Func<T, object?>>[]? includeProperties);
    public abstract Task<PagedResponse<T>> SearchAsync(TSearch searchDto, int page, int pageSize, string? sorting);
    public abstract Task<T?> GetByIdAsync(TKey id, params Expression<Func<T, object?>>[]? includeProperties);
    public abstract Task AddAsync(T entity);
    public abstract Task UpdateAsync(T entity);
    public abstract Task DeleteAsync(TKey id);
    public abstract Task<bool> ExistsAsync(TKey id);
}
