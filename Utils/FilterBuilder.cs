namespace TestMVCSharpC.Utils;

using System.Linq.Expressions;
using System.Reflection;

public abstract class FilterBuilder<TEntity, TSearch>
{
  protected readonly List<Expression<Func<TEntity, bool>>> Predicates = new();

  public abstract FilterBuilder<TEntity, TSearch> Build(TSearch dto);

  public Expression<Func<TEntity, bool>> Combine()
  => Predicates.Aggregate(PredicateBuilder.True<TEntity>(),
  (acc, pred) => acc.And(pred));

  protected void AddEquals<TProperty>(Expression<Func<TEntity, TProperty>> selector,
  TProperty? value)
  {
    if (value is null) return;
    var parameter = selector.Parameters.Single();
    var body = Expression.Equal(selector.Body,
    Expression.Constant(value, typeof(TProperty)));
    Predicates.Add(Expression.Lambda<Func<TEntity, bool>>(body, parameter));
  }

  protected void AddRange<TProperty>(Expression<Func<TEntity, TProperty?>> selector,
    TProperty? min, TProperty? max)
    where TProperty : struct, IComparable<TProperty>
  {
    if (min.HasValue || max.HasValue)
    {
      var parameter = selector.Parameters.Single();
      Expression propertyAccess = selector.Body;

      // Si la propriété est Nullable<T>, on accède à .Value
      if (propertyAccess.Type.IsGenericType &&
      propertyAccess.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
      {
        propertyAccess = Expression.Property(propertyAccess, "Value");
      }

      Expression? body = null;

      if (min.HasValue)
      {
        var minCondition = Expression.GreaterThanOrEqual(
        propertyAccess,
        Expression.Constant(min.Value)
        );
        body = minCondition;
      }

      if (max.HasValue)
      {
        var maxCondition = Expression.LessThanOrEqual(
        propertyAccess,
        Expression.Constant(max.Value)
        );
        body = body == null ? maxCondition : Expression.AndAlso(body, maxCondition);
      }

      if (body != null)
      {
        Predicates.Add(Expression.Lambda<Func<TEntity, bool>>(body, parameter));
      }
    }
  }

  protected void AddLike(Expression<Func<TEntity, string>> selector,string? value)
  {
    if (string.IsNullOrWhiteSpace(value)) return;

    var method = typeof(string).GetMethod("Contains",
    BindingFlags.Public | BindingFlags.Instance,
    new[] { typeof(string) })!;

    var parameter = selector.Parameters.Single();
    var toLower = Expression.Call(selector.Body, "ToLower", null);
    var contains = Expression.Call(toLower, method,
    Expression.Constant(value.ToLower()));
    Predicates.Add(Expression.Lambda<Func<TEntity, bool>>(contains, parameter));
  }

  protected void AddEqualsTimeOfDay<TProperty>(
      Expression<Func<TEntity, TProperty?>> selector,
      TProperty? value)
      where TProperty : struct
  {
    if (value is null) return;

    var parameter = selector.Parameters.Single();
    var propertyAccess = selector.Body;

    // Si c'est un Nullable<T>, on accède à .Value
    if (propertyAccess.Type.IsGenericType &&
    propertyAccess.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
    {
      propertyAccess = Expression.Property(propertyAccess, "Value");
    }

    // On vérifie que c'est bien un DateTimeOffset
    if (propertyAccess.Type != typeof(DateTimeOffset)) return;

    var timeOfDay = Expression.Property(propertyAccess, "TimeOfDay");
    var targetTime = Expression.Constant(((DateTimeOffset)(object)value).TimeOfDay);

    var body = Expression.Equal(timeOfDay, targetTime);
    Predicates.Add(Expression.Lambda<Func<TEntity, bool>>(body, parameter));
  }

  protected void AddRangeTimeOfDay<TProperty>(
      Expression<Func<TEntity, TProperty?>> selector,
      TProperty? min,
      TProperty? max)
      where TProperty : struct
  {
    if (!min.HasValue && !max.HasValue) return;

    var parameter = selector.Parameters.Single();
    var propertyAccess = selector.Body;

    if (propertyAccess.Type.IsGenericType &&
    propertyAccess.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
    {
      propertyAccess = Expression.Property(propertyAccess, "Value");
    }

    if (propertyAccess.Type != typeof(DateTimeOffset)) return;

    var timeOfDay = Expression.Property(propertyAccess, "TimeOfDay");

    Expression? body = null;

    if (min.HasValue)
    {
      var minTime = ((DateTimeOffset)(object)min.Value).TimeOfDay;
      var minCondition = Expression.GreaterThanOrEqual(timeOfDay, Expression.Constant(minTime));
      body = minCondition;
    }

    if (max.HasValue)
    {
      var maxTime = ((DateTimeOffset)(object)max.Value).TimeOfDay;
      var maxCondition = Expression.LessThanOrEqual(timeOfDay, Expression.Constant(maxTime));
      body = body == null ? maxCondition : Expression.AndAlso(body, maxCondition);
    }

    if (body != null)
    {
      Predicates.Add(Expression.Lambda<Func<TEntity, bool>>(body, parameter));
    }
  }
}
