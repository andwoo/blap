namespace blap.framework.webdownloader.requests
{
  class WebDownloadRequest
  {
    public string url { get; private set; }

    public WebDownloadRequest(string downloadUrl)
    {
      url = downloadUrl;
    }
  }
}
