using UnityEngine;

namespace blap.debug.utils
{
  public static class Trace
  {
    public static void Log(object message, LogType type = LogType.Log)
    {
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
    }
  }
}
