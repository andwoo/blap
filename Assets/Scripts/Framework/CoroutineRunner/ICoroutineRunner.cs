using System.Collections;
using UnityEngine;

namespace Burzum.Runner
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator routine);
    void StopCoroutine(IEnumerator routine);
    void StopCoroutine(Coroutine routine);
  }
}
