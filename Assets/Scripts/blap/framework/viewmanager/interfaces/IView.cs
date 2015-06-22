using UnityEngine;

namespace viewmanager
{
  public delegate void TransitionComplete();

  public interface IView
  {
    GameObject gameObject { get; }
    ViewInfo viewInfo { get; }

    void SetViewInfo(ViewInfo info, object viewData);
    void TransitionIn(TransitionComplete transitionComplete);
    void TransitionOut(TransitionComplete transitionComplete);
    void CloseView();
  }
}
