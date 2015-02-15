using UnityEngine;
namespace blap.framework.webdownloader.responses
{
  class WebDownloadTextureResponse : AbstractWebDownloadResponse
  {
    public Texture2D texture2D { get; private set; }

    public WebDownloadTextureResponse(WWW httpResponse, bool downloadSuccess)
      : base(httpResponse, downloadSuccess)
    {
      if (base.success)
      {
        texture2D = httpResponse.texture;
        DestroyHttpResponse(httpResponse);
      }
    }
  }
}
