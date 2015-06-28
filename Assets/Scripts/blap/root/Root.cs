using debugconsole;
using DG.Tweening;
using eventdispatcher;
using gameroot;
using root.introloading;
using System.Collections;
using UnityEngine;
using viewenums;
using viewmanager;

namespace root
{
  public class Root : MonoBehaviour
  {
    private void Awake()
    {
      DOTween.Init();
    }

    private void Start()
    {
      InitializeEventDispatcher();
      InitializeDebugConsole();
      InitializeViewManager();
      StartApp();
    }

    private void InitializeEventDispatcher()
    {
      GameRoot.SetEventDispatcher(new EventDispatcher());
      Trace.Log("Global EventDispatcher set");
    }

    private void InitializeDebugConsole()
    {
      GameObject go = GameObject.Instantiate(Resources.Load("framework/debug/DebugConsole"), Vector3.zero, Quaternion.identity) as GameObject;
      GameRoot.SetDebugConsole(go.GetComponent<DebugConsole>());
      Trace.Log("DebugConsole set");
    }

    private void InitializeViewManager()
    {
      GameObject go = GameObject.Instantiate(Resources.Load("framework/viewmanager/ViewManager"), Vector3.zero, Quaternion.identity) as GameObject;
      GameRoot.SetViewManager(go.GetComponent<ViewManager>());
      Trace.Log("ViewManager set");
    }

    private void StartApp()
    {
      GameRoot.viewManager.PushView((int)ViewEnum.INTRO_LOADING);

      IntroLoadQueue initialLoad = new IntroLoadQueue();
      initialLoad.Enqueue(new LoadPlayerModelAction());
      initialLoad.StartLoadQueue();
    }
  }
}
