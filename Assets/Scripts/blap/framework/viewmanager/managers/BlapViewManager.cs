using blap.framework.viewmanager.views;
using UnityEngine;

namespace blap.framework.viewmanager.managers
{
  class BlapViewManager
  {
    public static TView CreateBlapViewFromPrefab<TView>(string resourcePath) where TView : BlapView
    {
      return (GameObject.Instantiate(Resources.Load(resourcePath)) as GameObject).GetComponent<TView>();
    }
  }
}
