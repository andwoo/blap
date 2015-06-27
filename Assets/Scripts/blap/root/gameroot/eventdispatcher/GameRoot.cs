using eventdispatcher;

namespace gameroot
{
  public partial class GameRoot
  {
    private static EventDispatcher _dispatcher;

    public static void InitializeGlobalEventDispatcher()
    {
      _dispatcher = new EventDispatcher();
    }

    public static EventDispatcher globalDispatcher
    {
      get
      {
        return _dispatcher;
      }
    }
  }
}
