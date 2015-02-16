using blap.framework.webdownloader.interfaces;
using blap.framework.webdownloader.responses;
using blap.framework.webdownloader.requests;
using blap.framework.www.factories;
using blap.framework.www.httprequests;
using UnityEngine;
using System;

namespace blap.framework.webdownloader.services
{
  class WebDownloadService : IWebDownloadService
  {
    public event OnDownloadFinished downloadCompleteEvent;

    public WebDownloadService()
    {
    }

    public void SendRequest<T>(WebDownloadRequest request) where T : AbstractWebDownloadResponse
    {
      WWWFactory.instance.CreateGetRequest().SendRequest(request.url, 
      (delegate(WWW httpRequest)
      {
        object[] args = new object[4];
        args[0] = httpRequest;
        args[1] = true;
        args[2] = -1;
        args[3] = "";
        downloadCompleteEvent((AbstractWebDownloadResponse)Activator.CreateInstance(typeof(T), args));
      }), 
      (delegate(WWW httpRequest, short errorCode, string errorMessage)
      {
        object[] args = new object[4];
        args[0] = httpRequest;
        args[1] = false;
        args[2] = errorCode;
        args[3] = errorMessage;
        downloadCompleteEvent((AbstractWebDownloadResponse)Activator.CreateInstance(typeof(T), args));
      }));
    }
  }
}