namespace TestMVCSharpC.Utils;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public static class ExportUtil
{
    // Generate CSV from a list of objects using reflection
    public static string GenerateCsv<T>(List<T> data)
    {
        if (data == null || !data.Any())
        {
            return string.Empty;
        }

        var stringBuilder = new StringBuilder();
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Header
        var header = string.Join(",", properties.Select(p => p.Name));
        stringBuilder.AppendLine(header);

        // Data rows
        foreach (var item in data)
        {
            var line = properties.Select(property =>
            {
                var value = property.GetValue(item);
                return value != null ? EscapeCsv(value.ToString()) : string.Empty;
            });
            stringBuilder.AppendLine(string.Join(",", line));
        }

        return stringBuilder.ToString();
    }

    private static string EscapeCsv(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        var escaped = value.Replace("\"", "\"\"");

        if (escaped.Contains(",") || escaped.Contains("\"") || escaped.Contains("\n") || escaped.Contains("\r"))
        {
            return $"\"{escaped}\"";
        }

        return escaped;
    }

            // Generate CSV from a list of dictionaries (pour gérer les FK aplaties)
    public static string GenerateCsv(IEnumerable<IDictionary<string, object>> data)
    {
        if (data == null || !data.Any()) return string.Empty;

        var stringBuilder = new StringBuilder();
        var firstRow = data.First();

        // Header
        stringBuilder.AppendLine(string.Join(",", firstRow.Keys.Select(EscapeCsv)));

        // Data rows
        foreach (var row in data)
        {
            var line = firstRow.Keys.Select(key => 
                row.TryGetValue(key, out var value) && value != null ? EscapeCsv(value.ToString()) : string.Empty
            );
            stringBuilder.AppendLine(string.Join(",", line));
        }

        return stringBuilder.ToString();
    }
}
