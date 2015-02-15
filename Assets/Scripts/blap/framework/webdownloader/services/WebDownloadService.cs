using blap.framework.webdownloader.interfaces;
using blap.framework.webdownloader.responses;
using blap.framework.webdownloader.requests;
using blap.framework.www.factories;
using blap.framework.www.httprequests;
using UnityEngine;

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
      GetRequest httpRequest = WWWFactory.instance.CreateGetRequest();
      //httpRequest.SendRequest(request.url, (x) => 
    }
  }
}
