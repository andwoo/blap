using DG.Tweening;
using gameroot;
using UnityEngine;
using viewenums;

namespace root
{
  public class Root : MonoBehaviour
  {
    private void Start()
    {
      DOTween.Init();

      GameRoot.InitializeDebugConsole();
      GameRoot.InitializeViewManager();

      GameRoot.viewManager.PushView((int)ViewEnum.TEST_VIEW, "yoyoma");
      GameRoot.viewManager.PushView((int)ViewEnum.TEST_VIEW, "yoyoma");
      GameRoot.viewManager.PushView((int)ViewEnum.TEST_VIEW, "yoyoma");
      GameRoot.viewManager.PushView((int)ViewEnum.TEST_VIEW, "yoyoma");
      GameRoot.viewManager.PushView((int)ViewEnum.TEST_VIEW, "yoyoma");
    }
  }
}
