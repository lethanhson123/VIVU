using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VIVU.Ultils.Extensions
{
    public static class ObjectExtension
    {
        public static void MapFromDictionary(this object obj, Dictionary<string, string> dict)
        {
            if (obj != null &&
               dict != null)
            {
                var props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (dict.ContainsKey(prop.Name) &&
                        prop.CanWrite)
                    {
                        prop.SetValue(obj: obj,
                            value: Convert.ChangeType(dict[prop.Name], prop.PropertyType));
                    }
                }
            }
        }

        public static Dictionary<string, object?>? ToDictionaryStringObject(this object obj)
        {
            if (obj != null)
            {
                return obj.GetType().GetProperties().ToDictionary
                (
                    propInfo => propInfo.Name,
                    propInfo => propInfo.GetValue(obj, null)
                );
            }
            return null;
        }

        public static T? MapFromMeta<T>(this IEnumerable<T> metas)
        {
            T? obj = (T)(Activator.CreateInstance(typeof(T))!);

            if (metas != null)
            {
                PropertyInfo[] propInfos = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var meta in metas)
                {
                    string Key = meta?.GetType().GetProperty("Key")?.GetValue(meta)?.ToString()!;
                    object Value = meta?.GetType().GetProperty("Value")?.GetValue(meta)!;

                    obj.SetValue(Key, Value, propInfos);
                }
            }

            return obj;
        }

        public static Dictionary<string, string?>? ToDictionaryStringString(this object obj)
        {
            if (obj != null)
            {
                Dictionary<string, string?> result = new();
                PropertyInfo[] props = obj.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (!string.IsNullOrEmpty(prop.GetValue(obj)?.ToString()))
                    {
                        result.Add(prop.Name, prop.GetValue(obj)?.ToString());
                    }
                }
                return result;
            }
            return null;
        }

        public static List<T> MapToMeta<T>(this object obj)
        {
            var instances = new List<T>();

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select new KeyValuePair<string, object>(p.Name, p.GetValue(obj, null)!);

            foreach (var prop in properties)
            {
                var newInstance = (T)(Activator.CreateInstance(typeof(T))!);

                PropertyInfo[] propInfos = newInstance!.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                newInstance.SetValue("Key", prop.Key, propInfos);
                newInstance.SetValue("Value", prop.Value, propInfos);

                instances.Add(newInstance);
            }

            return instances;
        }

        public static string ToQueryStringData(this object obj)
        {
            if (obj != null)
            {
                var data = obj.ToDictionaryStringString();
                if (data != null)
                {
                    StringBuilder query = new();
                    foreach (KeyValuePair<string, string?> kv in data)
                    {
                        if (!String.IsNullOrEmpty(kv.Value))
                        {
                            query.Append(kv.Key + "=" + HttpUtility.UrlEncode(kv.Value) + "&");
                        }
                    }
                    return query.ToString().Remove(query.ToString().Length - 1);
                }
            }
            return string.Empty;
        }

        public static string GetMemberType(string DispName, PropertyInfo[] propInfo)
        {
            foreach (var m in propInfo)
            {
                var atts = m.GetCustomAttributes(typeof(DisplayAttribute), false);

                if (atts == null || 0 == atts.Length)
                {
                    atts = new DisplayAttribute[1];
                    atts[0] = new DisplayAttribute();
                    ((DisplayAttribute)atts[0]).Name = "";
                }
                if (((DisplayAttribute)atts[0]).Name == DispName || m.Name == DispName)
                {
                    if (m.PropertyType.IsGenericType && m.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return m.PropertyType.GetGenericArguments()[0].Name + "?";
                    }
                    return m.PropertyType.Name;
                }
            }

            return "";
        }

        public static void SetValue(this object obj, string key, object? value, PropertyInfo[] props)
        {
            switch (GetMemberType(key, props))
            {
                case "Boolean":
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                        value = false;
                    else if (value?.ToString()?.ToUpper() == "TRUE")
                        value = true;
                    else
                        value = false;
                    break;
                case "Int32":
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                        value = 0;
                    else
                        value = int.Parse(value?.ToString()!);
                    break;
                case "String":
                    value = value?.ToString() ?? "";
                    break;
                case "Double":
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                        value = 0.0;
                    else
                        value = double.Parse(value?.ToString()!);
                    break;
                case "Double?":
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                        value = null;
                    else
                        value = double.Parse(value?.ToString()!);
                    break;
                case "Decimal":
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                        value = 0;
                    else
                        value = decimal.Parse(value?.ToString()!);
                    break;
                case "Decimal?":
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                        value = null;
                    else
                        value = decimal.Parse(value?.ToString()!);
                    break;
                case "DateTime?":
                    if (string.IsNullOrWhiteSpace(value?.ToString()))
                        value = null;
                    else
                        value = DateTime.Parse(value?.ToString()!);
                    break;
            }

            var property = obj.GetType().GetProperty(key);
            if (property != null)
            {
                property.SetValue(obj, value);
            }
        }
    }
}
