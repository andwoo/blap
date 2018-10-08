using UnityEngine;

namespace Burzum.Utils
{
  public static class ColourUtils
  {
    /// <summary>
    /// Returns the Colour's value in hex form
    /// </summary>
    /// <param name="colour">Colour object in which we want the hex value</param>
    /// <param name="uppercase">return the hex string in uppercase or lower case</param>
    /// <returns>The hex value of the passed in colour</returns>
    public static string ColourToHexValue(Color colour, bool uppercase)
    {
      //http://stackoverflow.com/questions/14687786/converting-rgb-float-data-to-hex-string
      string caseVal = uppercase ? "X2" : "x2";
      return string.Format("#{0}{1}{2}",
        ((int)(colour.r * 255)).ToString(caseVal),
        ((int)(colour.g * 255)).ToString(caseVal),
        ((int)(colour.b * 255)).ToString(caseVal));
    }
  }
}
