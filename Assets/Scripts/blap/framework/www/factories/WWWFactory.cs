using blap.framework.utils;
using blap.framework.www.httprequests;

namespace blap.framework.www.factories
{
  public static class WWWFactory
  {
    public static GetRequest CreateGetRequest(float timeOutLimit, short retryLimit, bool useBackoff)
    {
      return new GetRequest(timeOutLimit, retryLimit, useBackoff);
    }
  }
}
