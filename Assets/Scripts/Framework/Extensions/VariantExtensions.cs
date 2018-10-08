#pragma warning disable 0219

using TinyJSON;

namespace Burzum.Extensions
{
  public static class VariantExtensions
  {
    public static bool ContainsKey(this Variant variant, string key)
    {
      try
      {
        Variant item = variant[key];
        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
