using blap.framework.utils;
using Facebook;
using System.Collections.Generic;

namespace blap.framework.facebook.requests
{
  abstract class AbstractFacebookApiRequest
  {
    private string _apiCall;
    private Dictionary<string, string> _urlParameters;
    public HttpMethod httpMethod { get; private set; }
    public Dictionary<string, string> postData { get; private set; }

    public AbstractFacebookApiRequest(string apiCall, HttpMethod method)
    {
      _apiCall = apiCall;
      httpMethod = method;
    }

    public string GetApiCall()
    {
      return _urlParameters != null ? _apiCall + DictionaryUtils.ToQueryString(_urlParameters, false) : _apiCall;
    }

    protected void AddUrlParameter(string key, string parameter)
    {
      if (string.IsNullOrEmpty(parameter))
      {
        return;
      }

      if (_urlParameters == null)
      {
        _urlParameters = new Dictionary<string, string>();
      }

      if (!_urlParameters.ContainsKey(key))
      {
        _urlParameters.Add(key, parameter);
      }
    }

    protected void AddPostDataParameter(string key, string data)
    {
      if (string.IsNullOrEmpty(data))
      {
        return;
      }

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
