using blap.framework.debug.interfaces;
using System;
using System.Collections.Generic;

namespace blap.framework.debug.models
{
  public class CommandContainer : ICommandContainer
  {
    private IDictionary<string, IComparable> _commandList;

    public CommandContainer()
    {
      _commandList = new Dictionary<string, IComparable>();
    }

    public void AddCommand(string command, IComparable dispatchEvent)
    {
      if (!_commandList.ContainsKey(command))
      {
        _commandList.Add(command, dispatchEvent);
      }
    }

    public bool HasEvent(string command)
    {
      return _commandList.ContainsKey(command);
    }

    public IComparable GetEvent(string command)
    {
      if (_commandList.ContainsKey(command))
      {
        return _commandList[command];
      }
      return null;
    }
  }
}
