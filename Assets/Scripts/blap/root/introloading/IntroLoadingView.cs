using DG.Tweening;
using framework.widgets;
using gameroot;
using UnityEngine;
using UnityEngine.UI;
using viewenums;
using viewmanager;

namespace root.introloading
{
  public class IntroLoadingView : MonoBehaviour, IView
  {
    [SerializeField]
    private CanvasGroup _canvasGroup = null;

    [SerializeField]
    private float _fadeDuration = 0f;

    [SerializeField]
    private UGUILoadingBar _loadingBar = null;

    [SerializeField]
    private Text _loadingText = null;

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
      GameRoot.globalDispatcher.AddEventListener(IntroLoadEvent.UPDATE_PERCENTAGE, OnUpdatePercentage);
      GameRoot.globalDispatcher.AddEventListener(IntroLoadEvent.UPDATE_LOAD_TEXT, OnUpdateLoadText);
      GameRoot.globalDispatcher.AddEventListener(IntroLoadEvent.LOAD_COMPLETE, OnLoadComplete);
    }

    private void OnDestroy()
    {
      GameRoot.globalDispatcher.RemoveEventListener(IntroLoadEvent.UPDATE_PERCENTAGE, OnUpdatePercentage);
      GameRoot.globalDispatcher.RemoveEventListener(IntroLoadEvent.UPDATE_LOAD_TEXT, OnUpdateLoadText);
      GameRoot.globalDispatcher.RemoveEventListener(IntroLoadEvent.LOAD_COMPLETE, OnLoadComplete);
    }

    private void OnUpdatePercentage(object percentage)
    {
      _loadingBar.SetPercentage((float)percentage);
    }

    private void OnUpdateLoadText(object message)
    {
      _loadingText.text = (string)message;
    }

    private void OnLoadComplete(object data)
    {
      GameRoot.viewManager.PushView((int)ViewEnum.MAIN_MENU);
    }
  }
}
