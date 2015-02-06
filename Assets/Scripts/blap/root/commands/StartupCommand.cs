using strange.extensions.command.impl;
using UnityEngine;

namespace blap.root.commands
{
  class StartupCommand : Command
  {
    public override void Execute()
    {
      Debug.Log("App Startup Complete");
    }
  }
}
