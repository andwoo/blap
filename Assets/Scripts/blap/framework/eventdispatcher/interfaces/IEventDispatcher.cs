using System;

namespace eventdispatcher
{
  public delegate void EventHandler(object data);

  public interface IEventDispatcher
  {
    void AddEventListener(IComparable eventName, EventHandler handler);
    void RemoveEventListener(IComparable eventName, EventHandler handler);
    void DispatchEvent(IComparable eventName, object data);
    void RemoveAllEventListeners();
  }
}
