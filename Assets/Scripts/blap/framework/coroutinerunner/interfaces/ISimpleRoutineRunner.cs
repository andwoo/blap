using System.Collections;

namespace blap.framework.coroutinerunner.interfaces
{
  public interface ISimpleRoutineRunner
  {
    void RunRoutine(IEnumerator routine);
    float DeltaTime();
  }
}
