namespace TestMVCSharpC.Utils;

public class GenericMapper
{
  public TModel Map<TSource, TModel>(TSource source) where TModel : new()
  {
    if (source == null)
    throw new ArgumentNullException(nameof(source));

    var sourceProps = typeof(TSource).GetProperties();
    var targetProps = typeof(TModel).GetProperties();

    var target = new TModel();

    foreach (var sProp in sourceProps)
    {
      var tProp = targetProps.FirstOrDefault(p =>
      p.Name.Equals(sProp.Name, StringComparison.OrdinalIgnoreCase) &&
      p.CanWrite && p.CanRead);

      if (tProp != null)
      {
        var value = sProp.GetValue(source);
        if (value != null)
        {
          try
          {
            // Tentative de conversion
            var convertedValue = Convert.ChangeType(value, Nullable.GetUnderlyingType(tProp.PropertyType) ?? tProp.PropertyType);
            tProp.SetValue(target, convertedValue);
          }
          catch (Exception ex)
          {
              // Afficher ou logguer l'erreur
              Console.WriteLine(ex.ToString());
          }
        }
      }
    }
    return target;
  }
}
