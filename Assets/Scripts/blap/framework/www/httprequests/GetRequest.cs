using blap.framework.coroutinerunner.interfaces;
using blap.framework.debug.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace blap.framework.www.httprequests
{
  class GetRequest : AbstractHttpRequest
  {
    public GetRequest(ISimpleRoutineRunner runner)
      : base(runner) {}

    public void SendGetRequest(string url, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _httpRequest = new WWW(url);
      base.SendRequest(onSuccessHandler, onFailHandler);
    }

    public void SendGetRequest(string url, bool nocache, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _httpRequest = new WWW(url, null, GetHeaders(null, nocache));
      base.SendRequest(onSuccessHandler, onFailHandler);
    }

    public void SendGetRequest(string url, bool nocache, IDictionary<string, string> headers, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _httpRequest = new WWW(url, null, GetHeaders(headers, nocache));
      base.SendRequest(onSuccessHandler, onFailHandler);
    }
  }
}
