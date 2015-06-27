using System;
using System.Collections.Generic;

namespace framework.actionqueue
{
  public delegate void ActionQueueComplete();

  public class ActionQueue : IDisposable
  {
    public event ActionQueueComplete queueComplete;

    private Queue<IAction> _queue;
    private bool _inProgress;

    public ActionQueue()
    {
      _queue = new Queue<IAction>();
      _inProgress = false;
    }

    public void Dispose()
    {
      _inProgress = false;
      while (_queue.Count > 0)
      {
        _queue.Dequeue().Dispose();
      }
    }

    public void Enqueue(IAction action)
    {
      _queue.Enqueue(action);
    }

    public void StartQueue()
    {
      _inProgress = true;
      NextQueueItem();
    }

    public int QueueSize()
    {
      return _queue.Count;
    }

    private void NextQueueItem()
    {
      if (_inProgress)
      {
        if (_queue.Count > 0)
        {
          IAction action = _queue.Dequeue();
          action.StartAction(delegate()
          {
            action.Dispose();
            NextQueueItem();
          });
        }
        else if (queueComplete != null)
        {
          Dispose();
          queueComplete();
        }
      }
    }
  }
}
