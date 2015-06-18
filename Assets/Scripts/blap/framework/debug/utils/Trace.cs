using blap.framework.debug.views;
using UnityEngine;

namespace blap.framework.debug.utils
{
  public static class Trace
  {
    /// <summary>
    /// Outputs the message to the debug log
    /// </summary>
    /// <param name="message">the message to be outputted to the debug console</param>
    /// <param name="type">The log type. ex. error, warning, log, ect...</param>
    public static void Log(object message, LogType type = LogType.Log)
    {
#if UNITY_EDITOR
      switch (type)
      {
        case LogType.Error:
          Debug.LogError(message);
          break;
        case LogType.Warning:
          Debug.LogWarning(message);
          break;
        case LogType.Exception:
        case LogType.Log:
        case LogType.Assert:
        default:
          Debug.Log(message);
          break;
      }
#else
      DebugConsole.Log(message.ToString(), type);
#endif

    }
  }
}
