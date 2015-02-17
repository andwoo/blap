using blap.framework.coroutinerunner.interfaces;
using blap.framework.utils;
using blap.framework.www.httprequests;

namespace blap.framework.www.factories
{
  class WWWFactory : Singleton<WWWFactory>
  {
    private ISimpleRoutineRunner _runner;

    public WWWFactory()
    {
    }

    public void SetRoutineRunner(ISimpleRoutineRunner runner)
    {
      _runner = runner;
    }

    public GetRequest CreateGetRequest(float timeOutLimit, short retryLimit, bool useBackoff)
    {
      return new GetRequest(_runner, timeOutLimit, retryLimit, useBackoff);
    }
  }
}
