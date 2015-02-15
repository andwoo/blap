using blap.framework.debug.interfaces;
using blap.framework.debug.utils;
using blap.framework.debug.vos;
using strange.extensions.command.impl;

namespace blap.framework.debug.commands
{
  class DebugRouteCommand : EventCommand
  {
    [Inject]
    public ICommandContainer commands { get; set; }

    public override void Execute()
    {
      DebugCommandVO vo = (DebugCommandVO)evt.data;
      //if a matching event matches the string command triggered by the debug console, dispatch the event type and data into the system
      if (commands.HasEvent(vo.command))
      {
        dispatcher.Dispatch(commands.GetEvent(vo.command), vo.parameters);
      }
      else
      {
        Trace.Log("Unknown debug command " + vo.command);
      }
    }
  }
}
