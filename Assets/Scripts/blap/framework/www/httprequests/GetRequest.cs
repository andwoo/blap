using blap.framework.coroutinerunner.interfaces;
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
      yield return _httpRequest;

      if (string.IsNullOrEmpty(_httpRequest.error))
      {
        _successHandler(_httpRequest);
      }
      else
      {
        //handle error
        string[] errorSplit = _httpRequest.error.Split(' ');
        _failHandler(_httpRequest, -1, "");
      }
    }
  }
}
