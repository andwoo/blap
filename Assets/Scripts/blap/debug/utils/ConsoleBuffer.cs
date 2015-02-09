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

    public void Add(string content)
    {
      if (!string.IsNullOrEmpty(content))
      {
        if(_cache.Count + 1 > _maxLines)
        {
          _cache.RemoveAt(0);
        }
        _cache.Add(content);
      }
    }

    public void Add(List<string> content)
    {
      for(int i = 0; i < content.Count; i++)
      {
        Add(content[i]);
      }
    }

    public List<string> GetRange(int startIndex, int count)
    {
      if (startIndex < 0)
      {
        startIndex = 0;
      }
      else if (startIndex > _cache.Count)
      {
        startIndex = _cache.Count - 1;
      }
      if (count < 0)
      {
        count = 0;
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
