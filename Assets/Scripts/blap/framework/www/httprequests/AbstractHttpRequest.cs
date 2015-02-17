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
    private ISimpleRoutineRunner _runner;
    private WWW _httpRequest;
    private OnGetRequestSuccessHandler _successHandler;
    private OnGetRequestFailedHandler _failHandler;

    //timeout vars
    private bool _abortTimeout;
    private bool _timedOut;
    private int _timeoutLimit = 5;
    private float _currentTimeoutTime;

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

    protected void SendRequest(string url, byte[] postData, Dictionary<string, string> headers, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _httpRequest = new WWW(url, postData, headers);
      _successHandler = onSuccessHandler;
      _failHandler = onFailHandler;
      _runner.RunRoutine(StartHttpRequest());
      _runner.RunRoutine(StartTimeout());
    }

    private IEnumerator StartHttpRequest()
    {
      float timeSpent = Time.realtimeSinceStartup;
      Trace.Log("starting GET Request to " + _httpRequest.url);
      yield return _httpRequest;

      if (!_timedOut)
      {
         _abortTimeout = true;
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
      else
      {
        Trace.Log("Aborting main request routine");
      }
    }

    private IEnumerator StartTimeout()
    {
      while (!_abortTimeout && _currentTimeoutTime < _timeoutLimit)
      {
        _currentTimeoutTime += _runner.DeltaTime();
        Trace.Log("_currentTimeoutTime: " + _currentTimeoutTime);
        yield return new WaitForFixedUpdate();
      }

      if (!_abortTimeout)
      {
        Trace.Log("Request " + _httpRequest.url + " timed out after " + _timeoutLimit.ToString() + "seconds");
        _timedOut = true;
        _failHandler(_httpRequest, (short)-1, "Request timed out");
        _httpRequest.Dispose();
        _httpRequest = null;
      }
      else
      {
        Trace.Log("Request timed out check aborted");
      }
    }
  }
}
