namespace blap.framework.webdownloader.requests
{
  class WebDownloadRequest
  {
    public string url { get; private set; }
    public float timeOutLimit { get; private set; }
    public short retryLimit { get; private set; }

    public WebDownloadRequest(string downloadUrl, float timeoutLimitSeconds, short retryCount, bool backoff)
    {
      url = downloadUrl;
      timeOutLimit = timeoutLimitSeconds;
      retryLimit = retryCount;
    }
  }
}
