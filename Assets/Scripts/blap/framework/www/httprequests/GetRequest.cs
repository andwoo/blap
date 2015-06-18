namespace www
{
  public class GetRequest : AbstractHttpRequest
  {
    public GetRequest(string url, bool cacheBust, float timeOutLimit, short retryLimit, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
      : base(url, null, false, cacheBust, timeOutLimit, retryLimit, onSuccessHandler, onFailHandler)
    { }
  }
}
