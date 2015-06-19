﻿using coroutinerunner;
using debugconsole;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace www
{
  public delegate void OnGetRequestSuccessHandler(WWW httpRequest);
  public delegate void OnGetRequestFailedHandler(WWW httpRequest, short errorCode, string errorMessage);

  public abstract class AbstractHttpRequest
  {
    //completion handlers
    private OnGetRequestSuccessHandler _successHandler;
    private OnGetRequestFailedHandler _failHandler;

    //www vars
    private WWW _httpRequest;
    private string _url;
    private byte[] _postData;
    Dictionary<string, string> _headers;
    private string _requestType;

    //timeout vars
    private bool _abortTimeout;
    private bool _timedOut;
    private float _timeoutLimit;
    private float _timeoutCount;

    //retry vars
    private short _retryLimit;
    private short _retryCount;
    private bool _useBackoff;

    public AbstractHttpRequest(string url, byte[] postData, bool isJsonPostData, bool cacheBust, float timeOutLimit, short retryLimit, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      _timeoutLimit = timeOutLimit;
      _retryLimit = retryLimit;
      _retryCount = 1;
      _useBackoff = true;
      _url = url;
      _postData = postData;
      _headers = GetHeaders(cacheBust, isJsonPostData);
      _successHandler = onSuccessHandler;
      _failHandler = onFailHandler;
      _requestType = postData != null ? "POST" : "GET";
    }

    protected Dictionary<string, string> GetHeaders(bool cacheBust, bool isJsonPostData)
    {
      Dictionary<string, string> headers = new Dictionary<string, string>();

      if (cacheBust)
      {
        headers.Add("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
        headers.Add("Pragma", "no-cache"); // HTTP 1.0.
        headers.Add("Expires", "0"); // Proxies.
      }

      if (isJsonPostData)
      {
        headers.Add("Content-Type", "application/json");
      }

      return headers;
    }

    public void SendRequest()
    {
      _httpRequest = new WWW(_url, _postData, _headers);
      _timeoutCount = 0f;
      _abortTimeout = false;
      _timedOut = false;
      CoroutineRunner.StartCoroutine(StartHttpRequest());
      CoroutineRunner.StartCoroutine(StartTimeout());
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
        if(_useBackoff)
        {
          CoroutineRunner.StartCoroutine(ExponentialBackoff(Convert.ToSingle(Math.Pow(2, _retryCount) - 1)));
        }
        else
        {
          SendRequest();
        }
      }
    }

    private IEnumerator ExponentialBackoff(float backOff)
    {
      Trace
        .Log(string.Format("Retrying request in {0} seconds", backOff));
      yield return new WaitForSeconds(backOff);
      SendRequest();
    }

    private IEnumerator StartHttpRequest()
    {
      float timeSpent = Time.realtimeSinceStartup;
      Trace.Log(string.Format("[{0}/{1}] starting {2} Request to {3}", _retryCount, _retryLimit, _requestType, _httpRequest.url));
      yield return _httpRequest;

      if (!_timedOut && _httpRequest != null && _httpRequest.isDone)
      {
        _abortTimeout = true;
        timeSpent = Time.realtimeSinceStartup - timeSpent;
        Trace.Log(string.Format("[{0}/{1}] {2} Request to {3} took {4} seconds", _retryCount, _retryLimit, _requestType, _httpRequest.url, timeSpent.ToString()));

        if (string.IsNullOrEmpty(_httpRequest.error))
        {
          _successHandler(_httpRequest);
          _httpRequest.Dispose();
          _httpRequest = null;
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

          Trace.Log(string.Format("[{0}/{1}] {2} Request error {3}", _retryCount, _retryLimit, _requestType, errorMessage));
          RequestFailed(errorCode, errorMessage);
        }
      }
    }

    private IEnumerator StartTimeout()
    {
      while (!_abortTimeout && _timeoutCount < _timeoutLimit)
      {
        _timeoutCount += Time.deltaTime;
        yield return new WaitForFixedUpdate();
      }

      if (!_abortTimeout)
      {
        Trace.Log(string.Format("[{0}/{1}] {2} Request {3} timed out after {4} seconds", _retryCount, _retryLimit, _requestType, _httpRequest.url, _timeoutLimit));
        _timedOut = true;
        RequestFailed((short)-1, "Request timed out");
      }
    }
  }
}
