using UnityEngine;

namespace blap.framework.webdownloader.responses
{
  public class WebDownloadTextureResponse : AbstractWebDownloadResponse
  {
    public Texture2D texture2D { get; private set; }

    public WebDownloadTextureResponse(WWW httpResponse, bool downloadSuccess, short errorCode, string errorMessage)
      : base(httpResponse, downloadSuccess, errorCode, errorMessage)
    {
      if (base.success)
      {
        texture2D = httpResponse.texture;
      }
    }
  }
}
