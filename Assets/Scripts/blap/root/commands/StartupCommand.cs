using blap.framework.viewmanager.managers;
using blap.framework.debug.utils;
using blap.framework.debug.views;
using blap.root.views;
using strange.extensions.command.impl;
using UnityEngine;

namespace blap.root.commands
{
  class StartupCommand : Command
  {
    public override void Execute()
    {
      GameObject.Find("ContextView").GetComponent<RootContextView>().root.AddView(BlapViewManager.CreateBlapViewFromPrefab<DebugConsoleView>("framework/debug/DebugConsole"));
      Trace.Log("App Startup Complete");
    }
  }
}
