using System;

namespace utils
{
  //http://stackoverflow.com/questions/943635/c-sharp-arrays-getting-a-sub-array-from-an-existing-array
  static class ArrayUtils
  {
    /// <summary>
    /// Returns a segment of an array starting at the start index till the end of the array
    /// </summary>
    /// <typeparam name="T">The array type</typeparam>
    /// <param name="data"></param>
    /// <param name="start">The start index to grab from</param>
    /// <returns>A segmented array</returns>
    public static T[] SubArray<T>(this T[] data, int start)
    {
      return SubArray(data, start, data.Length - start);
    }

    /// <summary>
    /// Returns a segment of an array starting at the start index till the end of the array
    /// </summary>
    /// <typeparam name="T">The array type</typeparam>
    /// <param name="data"></param>
    /// <param name="start">The start index to grab from</param>
    /// <param name="length">The length of items to grab</param>
    /// <returns>A segmented array</returns>
    public static T[] SubArray<T>(this T[] data, int start, int length)
    {
      T[] result = new T[length];
      Array.Copy(data, start, result, 0, length);
      return result;
    }
  }
}
