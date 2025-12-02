using System.Collections;
using System.Reflection;
using System.Web;

namespace HealthShoper.BLL.Extensions;

public static class QueryStringExtensions
{
    public static string ToQueryString(this object obj)
    {
        if (obj == null) return string.Empty;

        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var query = HttpUtility.ParseQueryString(string.Empty);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj);
            if (value == null) continue;

            // Если свойство — коллекция (но не строка)
            if (value is IEnumerable enumerable && !(value is string))
            {
                foreach (var item in enumerable)
                {
                    query.Add(prop.Name, item.ToString());
                }
            }
            else
            {
                query.Add(prop.Name, value.ToString());
            }
        }

        var queryString = query.ToString();
        return string.IsNullOrWhiteSpace(queryString) ? string.Empty : "?" + queryString;
    }
}