using blap.framework.webdownloader.interfaces;
using blap.framework.webdownloader.requests;
using blap.framework.webdownloader.responses;
using System;
using UnityEngine;
using www;

namespace blap.framework.webdownloader.services
{
  class WebDownloadService : IWebDownloadService
  {
    public event OnDownloadFinished downloadCompleteEvent;

    public WebDownloadService(){}

    public void SendRequest<T>(WebDownloadRequest request) where T : AbstractWebDownloadResponse
    {
      WWWFactory.CreateGetRequest(request.url, true, request.timeOutLimit, request.retryLimit,
      delegate(WWW httpRequest)
      {
        downloadCompleteEvent((T)Activator.CreateInstance(typeof(T), new object[] { httpRequest, true, (short)-1, "" }));
      },
      delegate(WWW httpRequest, short errorCode, string errorMessage)
      {
        downloadCompleteEvent((T)Activator.CreateInstance(typeof(T), new object[] { httpRequest, false, errorCode, errorMessage }));
      }).SendRequest();
    }
  }
}