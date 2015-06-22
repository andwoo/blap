using System.Collections.Generic;
using UnityEngine;

namespace viewmanager
{
  public class MultiViewLayer : AbstractLayer
  {
    private IDictionary<int, List<IView>> _activeViews;

    public MultiViewLayer(GameObject container)
      : base(container)
    {
      _activeViews = new Dictionary<int, List<IView>>();
    }

    public override void PushView(ViewInfo info, object viewData)
    {
      IView view = InstantiatePrefabView(info, viewData);

      if (!_activeViews.ContainsKey(info.id))
      {
        _activeViews.Add(info.id, new List<IView>());
      }

      view.viewInfo.active = true;
      TransitionInView(view, delegate()
      {
        _activeViews[info.id].Add(view);
      });
    }

    public override void PopView(IView view)
    {
      if (_activeViews.ContainsKey(view.viewInfo.id))
      {
        view.viewInfo.active = false;
        TransitionOutView(view, delegate()
        {
          _activeViews[view.viewInfo.id].Remove(view);
          GameObject.Destroy(view.gameObject);
        });
      }
    }
  }
}
