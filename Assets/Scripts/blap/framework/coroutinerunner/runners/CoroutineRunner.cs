using blap.framework.coroutinerunner.interfaces;
using System.Collections;
using UnityEngine;

namespace framework.coroutinerunner.runners
{
  public class CoroutineRunner : MonoBehaviour, ISimpleRoutineRunner
  {
    public void RunRoutine(IEnumerator routine)
    {
      base.StartCoroutine(routine);
    }

    public float DeltaTime()
    {
      return Time.deltaTime;
    }
  }
}
