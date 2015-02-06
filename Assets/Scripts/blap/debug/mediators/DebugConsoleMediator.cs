using Assets.Scripts.blap.debug.events;
using blap.baseclasses.mediators;
using blap.debug.views;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace blap.debug.mediators
{
  class DebugConsoleMediator : BlapMediator<DebugConsoleView>
  {
    public override void OnRegister()
    {
      base.OnRegister();
      view.dispatcher.AddListener(DebugConsoleEvent.TEST_CLICKED, OnTestClicked);
    }

    public override void OnRemove()
    {
      base.OnRemove();
      view.dispatcher.RemoveListener(DebugConsoleEvent.TEST_CLICKED, OnTestClicked);
    }

    private void OnTestClicked(IEvent evt)
    {
      Debug.Log("Test Clicked");
    }
  }
}
