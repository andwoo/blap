using System;

namespace extensions
{
  public static class ClampExtension
  {
    //http://stackoverflow.com/questions/2683442/where-can-i-find-the-clamp-function-in-net
    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
      if (val.CompareTo(min) < 0) return min;
      else if (val.CompareTo(max) > 0) return max;
      else return val;
    }
  }
}
