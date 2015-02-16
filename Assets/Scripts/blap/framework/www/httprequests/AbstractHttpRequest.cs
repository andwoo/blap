using blap.framework.coroutinerunner.interfaces;
using blap.framework.debug.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace blap.framework.www.httprequests
{
  public delegate void OnGetRequestSuccessHandler(WWW httpRequest);
  public delegate void OnGetRequestFailedHandler(WWW httpRequest, short errorCode, string errorMessage);

  abstract class AbstractHttpRequest
  {
    private int _timeoutLimit = 5;
    private float _currentTimeoutTime;
    private ISimpleRoutineRunner _runner;
    protected WWW _httpRequest;
    protected OnGetRequestSuccessHandler _successHandler;
    protected OnGetRequestFailedHandler _failHandler;

    public AbstractHttpRequest(ISimpleRoutineRunner runner)
    {
      _runner = runner;
      _currentTimeoutTime = 0f;
    }

    protected Dictionary<string, string> GetHeaders(IDictionary<string, string> headers, bool nocache)
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

    protected void SendRequest(OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _successHandler = onSuccessHandler;
      _failHandler = onFailHandler;
      _runner.RunRoutine(StartHttpRequest());
    }

    private IEnumerator StartHttpRequest()
    {
      float timeSpent = Time.realtimeSinceStartup;
      Trace.Log("starting GET Request to " + _httpRequest.url);
      yield return _httpRequest;
      timeSpent = Time.realtimeSinceStartup - timeSpent;
      Trace.Log("GET Request to " + _httpRequest.url + " took " + timeSpent.ToString() + " seconds");

      if (string.IsNullOrEmpty(_httpRequest.error))
      {
        _successHandler(_httpRequest);
      }
      else
      {
        short errorCode = -1;
        string errorMessage = _httpRequest.error;
        try
        {
          errorCode = Convert.ToInt16(_httpRequest.error.Split(' ')[0]);
        }
        catch { }

        Trace.Log("GET request error: " + errorMessage);
        _failHandler(_httpRequest, errorCode, errorMessage);
      }

      _httpRequest.Dispose();
      _httpRequest = null;
    }

    private IEnumerator StartTimeout()
    {
      while (_currentTimeoutTime < _timeoutLimit && _httpRequest != null)
      {
        _currentTimeoutTime += _runner.DeltaTime();
        yield return new WaitForFixedUpdate();
      }
    }
  }
}
