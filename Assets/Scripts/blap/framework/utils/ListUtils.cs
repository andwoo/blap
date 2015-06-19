using System.Collections.Generic;
using System.Text;

namespace utils
{
  public static class ListUtils
  {
    public static string ToStringList<T>(this List<T> list)
    {
      StringBuilder str = new StringBuilder();

      for (int i = 0; i < list.Count; i++)
      {
        str.AppendLine(list[i].ToString());
      }

      return str.ToString();
    }
  }
}
