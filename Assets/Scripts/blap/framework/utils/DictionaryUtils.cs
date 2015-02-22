using UnityEngine;
using System.Collections.Generic;

namespace blap.framework.utils
{
  static class DictionaryUtils
  {
    public static string ToQueryString(IDictionary<string, string> parameters, bool escapeCharacters)
    {
      string[] urlParams = new string[parameters.Count];
      int count = 0;
      foreach(var kvp in parameters)
      {
        urlParams[count++] = string.Format("{0}={1}", escapeCharacters ? WWW.EscapeURL(kvp.Key) : kvp.Key, escapeCharacters ? WWW.EscapeURL(kvp.Value) : kvp.Value);
      }
      return "?" + string.Join("&", urlParams);
    }
  }
}
