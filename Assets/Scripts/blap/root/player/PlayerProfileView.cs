using DG.Tweening;
using filesystem;
using gameroot;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using viewenums;
using viewmanager;

namespace root.player
{
  public class PlayerProfileView : MonoBehaviour, IView
  {
    [SerializeField]
    private CanvasGroup _canvasGroup = null;
    [SerializeField]
    private float _fadeDuration = 0f;

    [SerializeField]
    private InputField _name = null;

    [SerializeField]
    private Text _swagLevel = null;

    [SerializeField]
    private Text _chainLevel = null;

    [SerializeField]
    private Text _pantsLevel = null;

    [SerializeField]
    private Text _fiyaLevel = null;

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

    public void Start()
    {
      SetPlayerProfile();
    }

    private void SetPlayerProfile()
    {
      _name.text = PlayerModel.instance.name;
      _swagLevel.text = PlayerModel.instance.swagLevel.ToString();
      _chainLevel.text = PlayerModel.instance.chainLevel.ToString();
      _pantsLevel.text = PlayerModel.instance.pantsLevel.ToString();
      _fiyaLevel.text = PlayerModel.instance.fiyaLevel.ToString();
    }

    public void GUI_SaveProfile()
    {
      string newName = _name.text.Trim();
      if (!string.IsNullOrEmpty(newName) && PlayerModel.instance.name != newName)
      {
        PlayerModel.instance.name = newName;
        FileSystem.WriteFileAsJSON("/gameData/playermodel.json", PlayerModel.instance);
      }
    }

    public void GUI_ShowMainMenu()
    {
      GameRoot.viewManager.PushView((int)ViewEnum.MAIN_MENU);
    }
  }
}
