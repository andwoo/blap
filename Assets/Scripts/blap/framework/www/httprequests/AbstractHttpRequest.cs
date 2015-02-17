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
    private OnGetRequestSuccessHandler _successHandler;
    private OnGetRequestFailedHandler _failHandler;

    //www vars
    private WWW _httpRequest;
    private string _url;
    private byte[] _postData;
    Dictionary<string, string> _headers;

    //timeout vars
    private bool _abortTimeout;
    private bool _timedOut;
    private int _timeoutLimit = 5;
    private float _timeoutCount;

    //retry vars
    private short _retryLimit = 3;
    private short _retryCount;

    public AbstractHttpRequest(ISimpleRoutineRunner runner)
    {
      _runner = runner;
      _retryCount = 1;
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
      _url = url;
      _postData = postData;
      _headers = headers;
      _successHandler = onSuccessHandler;
      _failHandler = onFailHandler;
      RunRequest();
    }

    private void RunRequest()
    {
      _httpRequest = new WWW(_url, _postData, _headers);
      _timeoutCount = 0f;
      _abortTimeout = false;
      _timedOut = false;
      _runner.RunRoutine(StartHttpRequest());
      _runner.RunRoutine(StartTimeout());
    }

    private void RequestFailed(short errorCode, string errorMessage)
    {
      if (++_retryCount > _retryLimit)
      {
        _failHandler(_httpRequest, errorCode, errorMessage);
        _httpRequest.Dispose();
        _httpRequest = null;
      }
      else
      {
        _httpRequest.Dispose();
        _httpRequest = null;
        RunRequest();
      }
    }

    private IEnumerator StartHttpRequest()
    {
      float timeSpent = Time.realtimeSinceStartup;
      Trace.Log(string.Format("[{0}/{1}] starting GET Request to {2}", _retryCount, _retryLimit, _httpRequest.url));
      yield return _httpRequest;

      if (!_timedOut)
      {
         _abortTimeout = true;
        timeSpent = Time.realtimeSinceStartup - timeSpent;
        Trace.Log(string.Format("[{0}/{1}] GET Request to {2} took {3} seconds", _retryCount, _retryLimit, _httpRequest.url, timeSpent.ToString()));

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

          Trace.Log(string.Format("[{0}/{1}] GET Request error {2}", _retryCount, _retryLimit, errorMessage));
          RequestFailed(errorCode, errorMessage);
        }

        _httpRequest.Dispose();
        _httpRequest = null;
      }
    }

    private IEnumerator StartTimeout()
    {
      while (!_abortTimeout && _timeoutCount < _timeoutLimit)
      {
        _timeoutCount += _runner.DeltaTime();
        yield return new WaitForFixedUpdate();
      }

      if (!_abortTimeout)
      {
        Trace.Log(string.Format("[{0}/{1}] GET Request {2} timed out after {3} seconds", _retryCount, _retryLimit, _httpRequest.url, _timeoutLimit));
        _timedOut = true;
        RequestFailed((short)-1, "Request timed out");
      }
    }
  }
}
