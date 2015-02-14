using blap.baseclasses.mediators;
using blap.debug.events;
using blap.debug.views;
using blap.debug.vos;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace blap.debug.mediators
{
  class DebugConsoleMediator : BlapMediator<DebugConsoleView>
  {
    public override void OnRegister()
    {
      base.OnRegister();
      view.AddInputCommandLister(OnCommandEntered);

      dispatcher.AddListener(DebugConsoleEvent.CLEAR_CONSOLE, OnClearConsole);
    }

    public override void OnRemove()
    {
      base.OnRemove();
      view.RemoveInputCommandLister(OnCommandEntered);

      dispatcher.RemoveListener(DebugConsoleEvent.CLEAR_CONSOLE, OnClearConsole);
    }

    /// <summary>
    /// Event handler for when the debug console receives a command
    /// </summary>
    /// <param name="command">command to be mapped to an event</param>
    /// <param name="args">command body. contains values to be used in a command</param>
    private void OnCommandEntered(string command, string[] args)
    {
      dispatcher.Dispatch(DebugConsoleEvent.COMMAND_ENTERED, new DebugCommandVO(command, args));
    }

    /// <summary>
    /// Clears the debug console
    /// </summary>
    /// <param name="evt">optional</param>
    private void OnClearConsole(IEvent evt)
    {
      view.ClearLog();
    }
  }
}
