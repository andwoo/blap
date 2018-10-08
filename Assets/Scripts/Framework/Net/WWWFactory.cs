using System.Text;

namespace Burzum.Net
{
  public static class WWWFactory
  {
    public static GetRequest CreateGetRequest(string url, bool cacheBust, float timeOutLimit, short retryLimit, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      return new GetRequest(url, cacheBust, timeOutLimit, retryLimit, onSuccessHandler, onFailHandler);
    }

    public static PostRequest CreatePostRequest(string url, string postData, bool isJsonPostData, bool cacheBust, float timeOutLimit, short retryLimit, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      return new PostRequest(url, postData, isJsonPostData, cacheBust, timeOutLimit, retryLimit, onSuccessHandler, onFailHandler);
    }

    public static PostRequest CreatePostJSONRequest(string url, object postData, bool cacheBust, float timeOutLimit, short retryLimit, OnGetRequestSuccessHandler onSuccessHandler, OnGetRequestFailedHandler onFailHandler)
    {
      return new PostRequest(url, TinyJSON.JSON.Dump(postData), true, cacheBust, timeOutLimit, retryLimit, onSuccessHandler, onFailHandler);
    }
  }
}
