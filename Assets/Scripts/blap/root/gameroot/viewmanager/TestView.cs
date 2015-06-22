using debugconsole;
using gameroot;
using UnityEngine;
using viewmanager;
using DG.Tweening;

namespace Assets.Scripts.blap.root.gameroot.viewmanager
{
  public class TestView : MonoBehaviour, IView
  {
    public ViewInfo viewInfo { get; private set; }

    public void SetViewInfo(ViewInfo info, object viewData)
    {
      viewInfo = info;
      Trace.Log(viewData.ToString());
    }

    public void TransitionIn(TransitionComplete transitionComplete)
    {
      Tweener tween = transform.DOScale(Vector3.one, 3f);
      tween.OnComplete(delegate() { transitionComplete();
      CloseView();
      });
      tween.Play();
    }
    public void TransitionOut(TransitionComplete transitionComplete)
    {
      Tweener tween = transform.DOScale(Vector3.zero, 3f);
      tween.OnComplete(delegate() { transitionComplete(); });
      tween.Play();
    }
    public void CloseView()
    {
      GameRoot.viewManager.PopView(viewInfo.id);
    }
  }
}
