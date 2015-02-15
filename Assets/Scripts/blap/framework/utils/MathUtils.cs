using System;

namespace blap.framework.utils
{
  static class MathUtils
  {
    /// <summary>
    /// If a number of type T exceeds the min and max, it will be clamped down to fit in the desired boundaries.
    /// </summary>
    /// <typeparam name="T">The type of Number</typeparam>
    /// <param name="val">the value we want to clamp between min and max</param>
    /// <param name="min">The minimum value we will accept</param>
    /// <param name="max">The maximum value we will accept</param>
    /// <returns>A number that will be clamped if it exceeds the desired limits</returns>
    public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
    {
      //http://stackoverflow.com/questions/2683442/where-can-i-find-the-clamp-function-in-net
      if (val.CompareTo(min) < 0) return min;
      else if (val.CompareTo(max) > 0) return max;
      else return val;
    }
  }
}
