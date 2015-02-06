using blap.baseclasses.views;
using UnityEngine;

namespace blap.baseclasses.managers
{
  class BlapViewManager
  {
    public static TView CreateBlapViewFromPrefab<TView>(string resourcePath) where TView : BlapView
    {
      return (GameObject.Instantiate(Resources.Load(resourcePath)) as GameObject).GetComponent<TView>();
    }
  }
}
