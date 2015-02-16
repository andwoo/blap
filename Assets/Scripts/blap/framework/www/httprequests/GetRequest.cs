using blap.framework.coroutinerunner.interfaces;
using blap.framework.debug.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace blap.framework.www.httprequests
{
  public delegate void OnGetRequestSuccessHandler(WWW httpRequest);
  public delegate void OnGetRequestFailedHandler(WWW httpRequest, short errorCode, string errorMessage);

  class GetRequest
  {
    private ISimpleRoutineRunner _runner;
    private WWW _httpRequest;
    private OnGetRequestSuccessHandler _successHandler;
    private OnGetRequestFailedHandler _failHandler;

    public GetRequest(ISimpleRoutineRunner runner)
    {
      _runner = runner;
    }

    private Dictionary<string, string> GetHeaders(IDictionary<string, string> headers, bool nocache)
    {
      if (headers == null)
      {
        headers = new Dictionary<string, string>();
      }

      if (nocache)
      {
        headers.Add("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        headers.Add("Pragma", "no-cache"); // HTTP 1.0.
        headers.Add("Expires", "0"); // Proxies.
      }

      return (Dictionary<string, string>)headers;
    }

    public void SendRequest(string url, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _httpRequest = new WWW(url);
      Start(onSuccessHandler, onFailHandler);
    }

    public void SendRequest(string url, bool nocache, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _httpRequest = new WWW(url, null, GetHeaders(null, nocache));
      Start(onSuccessHandler, onFailHandler);
    }

    public void SendRequest(string url, bool nocache, IDictionary<string, string> headers, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _httpRequest = new WWW(url, null, GetHeaders(headers, nocache));
      Start(onSuccessHandler, onFailHandler);
    }

    private void Start(OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _successHandler = onSuccessHandler;
      _failHandler = onFailHandler;
      _runner.RunRoutine(StartHttpRequest());
    }

    IEnumerator StartHttpRequest()
    {
      float timeSpent = Time.realtimeSinceStartup;
      Trace.Log("starting GET Request to " + _httpRequest.url);
      yield return _httpRequest;
      timeSpent = Time.realtimeSinceStartup - timeSpent;
      Trace.Log("GET Request to " + _httpRequest.url + " took " + timeSpent.ToString() + " seconds");

      if (string.IsNullOrEmpty(_httpRequest.error))
      {
        Trace.Log("Request Success");
        _successHandler(_httpRequest);
      }
      else
      {
        Trace.Log(_httpRequest.error);
        string[] errorSplit = _httpRequest.error.Split(' ');
        _failHandler(_httpRequest, -1, "");
      }

      _httpRequest.Dispose();
      _httpRequest = null;
    }
  }
}
