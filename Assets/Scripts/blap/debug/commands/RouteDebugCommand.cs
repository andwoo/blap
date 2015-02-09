using blap.debug.utils;
using strange.extensions.command.impl;
using UnityEngine;

namespace blap.debug.commands
{
  class RouteDebugCommand : EventCommand
  {
    public override void Execute()
    {
      //handles parsing of the input
      Trace.Log(evt.data);
      Trace.Log(evt.data, LogType.Warning);
      Trace.Log(evt.data, LogType.Error);
    }
  }
}
