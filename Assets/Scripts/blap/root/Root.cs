using DG.Tweening;
using gameroot;
using root.introloading;
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
      GameRoot.InitializeGlobalEventDispatcher();

      GameRoot.viewManager.PushView((int)ViewEnum.INTRO_LOADING);

      IntroLoadQueue initialLoad = new IntroLoadQueue();
      initialLoad.Enqueue(new LoadPlayerModelAction());
      initialLoad.StartLoadQueue();
    }
  }
}
