using blap.baseclasses.mediators;
using blap.debug.events;
using blap.debug.views;

namespace blap.debug.mediators
{
  class DebugConsoleMediator : BlapMediator<DebugConsoleView>
  {
    public override void OnRegister()
    {
      base.OnRegister();
      view.dispatcher.AddListener(DebugConsoleEvent.COMMAND_ENTERED, Redispatch);
    }

    public override void OnRemove()
    {
      base.OnRemove();
      view.dispatcher.RemoveListener(DebugConsoleEvent.COMMAND_ENTERED, Redispatch);
    }
  }
}
