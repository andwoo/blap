using System;
using System.Collections.Generic;

namespace eventdispatcher
{
  public class EventDispatcher : IEventDispatcher, IDisposable
  {
    private IDictionary<IComparable, EventObject> _observers;
    
    public EventDispatcher()
    {
      _observers = new Dictionary<IComparable, EventObject>();
    }

    public void AddEventListener(IComparable eventName, EventHandler handler)
    {
      if (!HasEvent(eventName))
      {
        _observers[eventName] = new EventObject();
      }
      _observers[eventName].dispatchEvent += handler;
    }

    public void RemoveEventListener(IComparable eventName, EventHandler handler)
    {
      if (HasEvent(eventName))
      {
        _observers[eventName].dispatchEvent -= handler;

        if (_observers[eventName].TotalSubscribers() == 0)
        {
          _observers.Remove(eventName);
        }
      }
    }

    public void DispatchEvent(IComparable eventName, object data)
    {
      if (HasEvent(eventName))
      {
        _observers[eventName].Dispatch(data);
      }
    }

    private bool HasEvent(IComparable eventName)
    {
      return _observers != null && _observers.ContainsKey(eventName) ? true : false;
    }

    public void RemoveAllEventListeners()
    {
      if (_observers != null && _observers.Count > 0)
      {
        foreach (KeyValuePair<IComparable, EventObject> kvp in _observers)
        {
          kvp.Value.RemoveAllEventListeners();
        }
      }
      _observers.Clear();
      _observers = null;
    }

    public void Dispose()
    {
      RemoveAllEventListeners();
    }
  }

  class EventObject
  {
    public event EventHandler dispatchEvent;

    public EventObject() { }

    public void Dispatch(object data)
    {
      if (dispatchEvent != null)
      {
        dispatchEvent(data);
      }
    }

    public void RemoveAllEventListeners()
    {
      dispatchEvent = null;
    }

    public int TotalSubscribers()
    {
      return dispatchEvent != null ? dispatchEvent.GetInvocationList().Length : 0;
    }
  }
}
