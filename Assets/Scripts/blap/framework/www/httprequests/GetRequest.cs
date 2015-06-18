using System.Collections.Generic;

namespace blap.framework.www.httprequests
{
  public class GetRequest : AbstractHttpRequest
  {
    public GetRequest(float timeOutLimit, short retryLimit, bool useBackoff)
      : base(timeOutLimit, retryLimit, useBackoff) { }

    public void SendGetRequest(string url, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      base.SendRequest(url, null, null, onSuccessHandler, onFailHandler);
    }

    public void SendGetRequest(string url, bool nocache, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      base.SendRequest(url, null, GetHeaders(null, nocache), onSuccessHandler, onFailHandler);
    }

    public void SendGetRequest(string url, bool nocache, IDictionary<string, string> headers, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      base.SendRequest(url, null, GetHeaders(headers, nocache), onSuccessHandler, onFailHandler);
    }
  }
}
