using System.Collections;
using UnityEngine;

namespace coroutinerunner
{
  public static class CoroutineRunner
  {
    private static bool _isInitialized = false;
    private static ICoroutineRunner _runner = null;

    private static void Initialize()
    {
      if (!_isInitialized)
      {
        _isInitialized = true;
        GameObject obj = new GameObject();
        Object.DontDestroyOnLoad(obj);
        obj.hideFlags = HideFlags.HideInHierarchy;
        _runner = obj.AddComponent<CoroutineRunnerGameObject>();
      }
    }

    public static Coroutine StartCoroutine(IEnumerator routine)
    {
      Initialize();
      return _runner.StartCoroutine(routine);
    }

    public static void StopCoroutine(IEnumerator routine)
    {
      Initialize();
      _runner.StopCoroutine(routine);
    }

    public static void StopCoroutine(Coroutine routine)
    {
      Initialize();
      _runner.StopCoroutine(routine);
    }
  }

  public class CoroutineRunnerGameObject : MonoBehaviour, ICoroutineRunner { }
}
