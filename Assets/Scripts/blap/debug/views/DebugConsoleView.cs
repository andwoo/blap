using Assets.Scripts.blap.debug.events;
using blap.baseclasses.views;

namespace blap.debug.views
{
  public class DebugConsoleView : BlapView
  {
    public void GUI_OnTestClick()
    {
      dispatcher.Dispatch(DebugConsoleEvent.TEST_CLICKED);
    }
  }
}
