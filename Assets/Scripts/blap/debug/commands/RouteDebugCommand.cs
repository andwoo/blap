using strange.extensions.command.impl;
using UnityEngine;

namespace blap.debug.commands
{
  class RouteDebugCommand : EventCommand
  {
    public override void Execute()
    {
      //handles parsing of the input
      Debug.Log((string)evt.data);
    }
  }
}
