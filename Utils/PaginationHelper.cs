namespace TestMVCSharpC.Utils;

public class PaginationHelper
{
    public static async Task<PagedResponse<T>> CreatePagedResponse<T>(
    IEnumerable<T> items,
    int page,
    int pageSize,
    Func<Task<long>> totalCountFunc)
    {
      return new PagedResponse<T>
      {
        Content = items.ToList(),
        Page = new PageInfo
        {
          Size = pageSize,
          Number = page,
          TotalElements = await totalCountFunc(),
          TotalPages = (int)Math.Ceiling((double)await totalCountFunc() / pageSize)
        }
      };
    }
}
