using System.Collections.Generic;

namespace debugconsole
{
  class ConsoleBuffer
  {
    /// <summary>
    /// Console string storage
    /// </summary>
    private List<string> _cache;
    /// <summary>
    /// The maximum amount of lines the buffer will store
    /// </summary>
    public int maxLines { get; private set; }

    /// <summary>
    /// The storage of console logs
    /// </summary>
    /// <param name="max">The maximum amount of lines the buffer will store</param>
    public ConsoleBuffer(int max)
    {
      _cache = new List<string>();
      maxLines = max;
    }

    /// <summary>
    /// Clears all the contents in the cache
    /// </summary>
    public void Clear()
    {
      _cache.Clear();
    }

    /// <summary>
    /// Add an element to the storage
    /// </summary>
    /// <param name="content">The content to store</param>
    public void Add(string content)
    {
      if (!string.IsNullOrEmpty(content))
      {
        if (_cache.Count + 1 > maxLines)
        {
          _cache.RemoveAt(0);
        }
        _cache.Add(content);
      }
    }

    /// <summary>
    /// Add elements to the storage
    /// </summary>
    /// <param name="content">The contents to be store</param>
    public void Add(List<string> content)
    {
      for(int i = 0; i < content.Count; i++)
      {
        Add(content[i]);
      }
    }

    /// <summary>
    /// Get a range of console logs. Will adjust startIndex and count if out of range
    /// </summary>
    /// <param name="startIndex">The start index of the range we want to get</param>
    /// <param name="count">The amount to gather from the start index</param>
    /// <returns>A list of strings</returns>
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

    /// <summary>
    /// Returns the number of entries we currently have in the log storage
    /// </summary>
    /// <returns>Returns the number of entries</returns>
    public int NumberOfEntries()
    {
      return _cache.Count;
    }
  }
}
