using gameroot;
using UnityEngine;
using viewmanager;

namespace root.background
{
  class BackgroundView : MonoBehaviour, IView
  {
    #region IView
    public ViewInfo viewInfo { get; private set; }

    public void SetViewInfo(ViewInfo info, object viewData)
    {
      viewInfo = info;
    }

    public void TransitionIn(TransitionComplete transitionComplete)
    {
      transitionComplete();
    }

    public void TransitionOut(TransitionComplete transitionComplete)
    {
      transitionComplete();
    }

    public void CloseView()
    {
      GameRoot.viewManager.PopView(this);
    }
    #endregion
  }
}
