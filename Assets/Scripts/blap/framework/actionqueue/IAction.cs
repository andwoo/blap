using System;

namespace framework.actionqueue
{
  public delegate void ActionItemComplete();

  public interface IAction : IDisposable
  {
    void StartAction(ActionItemComplete onActionComplete);
  }
}
