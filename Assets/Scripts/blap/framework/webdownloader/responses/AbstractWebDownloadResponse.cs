using UnityEngine;

namespace blap.framework.webdownloader.responses
{
  public class AbstractWebDownloadResponse
  {
    public bool success { get; protected set; }
    public string downloadPath { get; private set; }

    public AbstractWebDownloadResponse(WWW httpResponse, bool downloadSuccess)
    {
      success = downloadSuccess;
      downloadPath = httpResponse.url;
    }

    protected void DestroyHttpResponse(WWW httpResponse)
    {
      if (httpResponse != null)
      {
        httpResponse.Dispose();
        httpResponse = null;
      }
    }
  }
}
