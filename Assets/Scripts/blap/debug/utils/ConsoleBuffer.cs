using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
namespace blap.debug.utils
{
  class ConsoleBuffer
  {
    private List<string> _cache;
    private int _maxLines;

    public ConsoleBuffer(int maxLines)
    {
      _cache = new List<string>();
      _maxLines = maxLines;
    }

    public void Add(string content, string splitOn = "\r\n")
    {
      if(!string.IsNullOrEmpty(content))
      {
        string[] entries = Regex.Split(content, splitOn);

        if (entries.Length + _cache.Count > _maxLines)
        {
          _cache.RemoveRange(0, entries.Length);
        }
        _cache.AddRange(entries);
      }
    }

    public List<string> GetRange(int startIndex, int count)
    {
      if (startIndex > _cache.Count)
      {
        startIndex = _cache.Count - 1;
        count = 1;
      }
      else if (startIndex + count > _cache.Count)
      {
        count = _cache.Count - startIndex;
      }
      return _cache.GetRange(startIndex, count);
    }

    public int NumberOfEntries()
    {
      return _cache.Count;
    }
  }
}
