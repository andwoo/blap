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
