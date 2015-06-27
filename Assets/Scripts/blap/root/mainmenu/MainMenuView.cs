using DG.Tweening;
using gameroot;
using UnityEngine;
using viewenums;
using viewmanager;

namespace root.mainmenu
{
  public class MainMenuView : MonoBehaviour, IView
  {
    [SerializeField]
    private CanvasGroup _canvasGroup = null;
    [SerializeField]
    private float _fadeDuration = 0f;

    #region IView
    public ViewInfo viewInfo { get; private set; }

    public void SetViewInfo(ViewInfo info, object viewData)
    {
      viewInfo = info;
    }

    public void TransitionIn(TransitionComplete transitionComplete)
    {
      _canvasGroup.DOFade(1f, _fadeDuration).OnComplete(delegate() { transitionComplete(); }).Play();
    }

    public void TransitionOut(TransitionComplete transitionComplete)
    {
      _canvasGroup.DOFade(0f, _fadeDuration).OnComplete(delegate() { transitionComplete(); }).Play();
    }

    public void CloseView()
    {
      GameRoot.viewManager.PopView(this);
    }
    #endregion

    private void Awake()
    {
      _canvasGroup.alpha = 0f;
    }

    public void GUI_OnPlayClicked()
    {
      
    }

    public void GUI_OnPlayerProfileClicked()
    {
      GameRoot.viewManager.PushView((int)ViewEnum.PLAYER_PROFILE);
    }
  }
}
