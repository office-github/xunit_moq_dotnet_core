using System;
using System.Collections.Generic;

namespace DotnetConsole
{
  public static class Extension
  {
    public static Dictionary<int, string> ToEnum(this Type type)
    {
      var dictionary = new Dictionary<int, string>();
      var properties = type.GetProperties();

      for(int i = 0; i < properties.Length; i++)
      {
        dictionary.Add(i, properties[i].Name);
      }

      return dictionary;
    }

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this Type e)
    {
      var dictionary = new Dictionary<TKey, TValue>();

      if (e.IsEnum)
      {
        var array = e.GetEnumValues();

        foreach (var t in array)
        {
          dictionary.Add((TKey)t, (TValue)t);
        }
      }

      return dictionary;
    }

    public static List<object> ToList(this Type e)
    {
      var list = new List<object>();

      if (e.IsEnum)
      {
        var array = e.GetEnumValues();

        foreach(var item in array)
        {
          list.Add(item);
        }
      }

      return list;
    }

    public static Array ToArray(this Type e)
    {
      Array array = null;

      if (e.IsEnum)
      {
        array = e.GetEnumValues();
      }

      return array;
    }

    public static Dictionary<int, T> ToDictionary<T>(this IEnumerable<T> ts)
    {
      var dictionary = new Dictionary<int, T>();
      int i = 0;

      foreach (var t in ts)
      {
        dictionary.Add(i++, t);
      }

      return dictionary;
    }

    public static dynamic Value(this Enum @enum)
    {
      return @enum.GetType();
    }
  }
}
