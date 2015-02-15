using blap.framework.webdownloader.requests;
using blap.framework.webdownloader.responses;
using UnityEngine;

namespace blap.framework.webdownloader.interfaces
{
  public delegate void OnDownloadFinished(AbstractWebDownloadResponse response);

  interface IWebDownloadService
  {
    event OnDownloadFinished downloadCompleteEvent;
    void SendRequest<T>(WebDownloadRequest request) where T : AbstractWebDownloadResponse;
  }
}
