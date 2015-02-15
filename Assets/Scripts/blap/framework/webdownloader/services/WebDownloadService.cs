using blap.framework.webdownloader.interfaces;
using blap.framework.webdownloader.responses;
using blap.framework.webdownloader.requests;

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

    }

    private void OnDownloadComplete()
    {
      downloadCompleteEvent(null);
    }
  }
}
