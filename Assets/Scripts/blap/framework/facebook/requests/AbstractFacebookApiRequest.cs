using Facebook;
using System.Collections.Generic;

namespace blap.framework.facebook.requests
{
  abstract class AbstractFacebookApiRequest
  {
    private string _call;
    public HttpMethod httpMethod { get; private set; }
    public Dictionary<string, string> urlParameters { get; private set; }
    public Dictionary<string, string> postData { get; private set; }

    public AbstractFacebookApiRequest(string apiCall, HttpMethod method)
    {
      _call = apiCall;
      httpMethod = method;
    }

    public string GetApiCall()
    {
      return _call;
    }

    protected void AddUrlParameter(string key, string parameter)
    {
      if (urlParameters == null)
      {
        urlParameters = new Dictionary<string, string>();
      }

      if (!urlParameters.ContainsKey(key))
      {
        urlParameters.Add(key, parameter);
      }
    }

    protected void AddPostDataParameter(string key, string data)
    {
      if (postData == null)
      {
        postData = new Dictionary<string, string>();
      }

      if (!postData.ContainsKey(key))
      {
        postData.Add(key, data);
      }
    }
  }
}
