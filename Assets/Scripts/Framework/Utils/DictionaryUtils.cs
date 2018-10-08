using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Burzum.Utils
{
  static class DictionaryUtils
  {
    public static string ToUrlQueryString(IDictionary<string, string> parameters, bool escapeCharacters)
    {
      string[] urlParams = new string[parameters.Count];
      int count = 0;
      foreach(var kvp in parameters)
      {
        urlParams[count++] = string.Format("{0}={1}", escapeCharacters ? WWW.EscapeURL(kvp.Key) : kvp.Key, escapeCharacters ? WWW.EscapeURL(kvp.Value) : kvp.Value);
      }
      return "?" + string.Join("&", urlParams);
    }

    public static string ToStringDictionary<K, V>(this IDictionary<K, V> dict)
    {
      StringBuilder str = new StringBuilder();
      foreach(KeyValuePair<K, V> kvp in dict)
      {
        str.AppendLine(string.Format("{2} = {1}", kvp.Key, kvp.Value));
      }
      return str.ToString();
    }
  }
}
