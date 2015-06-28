using eventdispatcher;

namespace gameroot
{
  public static partial class GameRoot
  {
    private static EventDispatcher _dispatcher;

    public static void SetEventDispatcher(EventDispatcher dispatcher)
    {
      _dispatcher = dispatcher;
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
