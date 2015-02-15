using blap.framework.webdownloader.requests;
using blap.framework.webdownloader.responses;
using UnityEngine;

namespace blap.framework.webdownloader.interfaces
{
  //move this into connection WWW interface
  public delegate void WebDownloadCompleteHandler(WWW response);
  public delegate void WebDownloadFailedHandler(WWW response, int errorCode, string errorMessage);

  public delegate void OnDownloadFinished(AbstractWebDownloadResponse response);

  interface IWebDownloadService
  {
    event OnDownloadFinished downloadCompleteEvent;
    void SendRequest<T>(WebDownloadRequest request) where T : AbstractWebDownloadResponse;
  }
}
