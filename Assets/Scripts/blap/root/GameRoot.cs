using blap.framework.debug.utils;
using blap.framework.debug.views;
using UnityEngine;

namespace Assets.Scripts.blap.root
{
  public class GameRoot : MonoBehaviour
  {
    private void Start()
    {
      GameObject go = Instantiate(Resources.Load("framework/debug/DebugConsole")) as GameObject;
      DebugConsole console = go.GetComponent<DebugConsole>();
      console.AddInputCommandLister(delegate(string command, string[] args)
      {
        Trace.Log("YUP: " + command);
      });
      Trace.Log("App Startup Complete");
    }
  }
}
