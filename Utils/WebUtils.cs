namespace TestMVCSharpC.Utils;

using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

public class WebUtils
{
    public static readonly string _asc = "asc";
    public static readonly string _desc = "desc";
    public static readonly string _sortCriteriaDelimiter = ";";
    public static readonly string _sortParsDelimiter = ",";

    public static IEnumerable<(string PropertyName, bool Ascending)> ParseSortingString(string input)
    {
        return input
        .Split(_sortCriteriaDelimiter, StringSplitOptions.RemoveEmptyEntries)
        .Select(part =>
        {
          var elements = part.Split(_sortParsDelimiter, StringSplitOptions.TrimEntries);
            if (elements.Length != 2)
              throw new ArgumentException("Each sort criteria must have exactly 2 parts: property and direction");

            string propertyName = elements[0];
            bool ascending = true;
            if(elements[1].Equals(_asc, StringComparison.OrdinalIgnoreCase)) 
            {
                ascending = true;
            }
            else if(elements[1].Equals(_desc, StringComparison.OrdinalIgnoreCase))
            {
                ascending = false;
            }
            else
            {
              throw new ArgumentException($"Sort direction must be either '{_asc}' or '{_desc}'");
            }

            return (propertyName, ascending);
        });
    }

    public static IQueryable<T> ApplySort<T>(IQueryable<T> query, 
          IEnumerable<(string PropertyName, bool Ascending)> sortCriteria)
    {
      var result = query;
      bool isFirst = true;

      foreach (var (propertyName, ascending) in sortCriteria)
        {
          var propertyInfo = typeof(T).GetProperty(propertyName,
          BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

          if (propertyInfo == null)
            throw new ArgumentException($"Property {propertyName} not found for type {typeof(T).FullName}");

          var parameter = Expression.Parameter(typeof(T));

          var property = Expression.Property(parameter, propertyInfo);

          var lambda = Expression.Lambda(property, parameter);

          string methodName;
          if (isFirst)
          {
              methodName = ascending ? "OrderBy" : "OrderByDescending";
                                                       isFirst = false;
          }
          else
          {
              methodName = ascending ? "ThenBy" : "ThenByDescending";
          }

          var methodInfo = typeof(Queryable).GetMethods()
              .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
              .Single(m => m.GetParameters().Length == 2);

          var genericMethod = methodInfo.MakeGenericMethod(typeof(T), propertyInfo.PropertyType);

          result = (IQueryable<T>)genericMethod.Invoke(null, new object[] { result, lambda });
        }

        return result;
    }

    public static string AddSort(string existingSortOrder, string newSortOrder)
    {
      if (string.IsNullOrWhiteSpace(newSortOrder))
      return existingSortOrder ?? "";

      var parts = newSortOrder.Split(',', StringSplitOptions.RemoveEmptyEntries);
      if (parts.Length != 2)
        return existingSortOrder ?? "";

      string newColumn = parts[0].Trim();
      string newDirection = parts[1].Trim().ToLower();

      var sorts = string.IsNullOrWhiteSpace(existingSortOrder)
        ? new List<string>()
        : existingSortOrder.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();

      bool found = false;
      for (int i = 0; i < sorts.Count; i++)
      {
        var sParts = sorts[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (sParts.Length != 2) continue;

        string col = sParts[0].Trim();
        string dir = sParts[1].Trim().ToLower();

        if (col.Equals(newColumn, StringComparison.OrdinalIgnoreCase))
        {
          if (dir == "asc")
          {
            sorts[i] = $"{col},desc";
          }
          else
          {
            sorts.RemoveAt(i);
          }
          found = true;
          break;
        }
      }

      if (!found)
        sorts.Add($"{newColumn},{newDirection}");

      return string.Join(";", sorts);
    }
}
