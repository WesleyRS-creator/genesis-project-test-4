namespace TestMVCSharpC.Utils;

using System.Linq.Expressions;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> True<T>()  => _ => true;
    public static Expression<Func<T, bool>> False<T>() => _ => false;

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var p = left.Parameters[0];
        var body = Expression.AndAlso(left.Body,
                     Expression.Invoke(right, p));
        return Expression.Lambda<Func<T, bool>>(body, p);
    }
}
