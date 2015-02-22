using blap.framework.debug.utils;
using blap.framework.facebook.interfaces;
using blap.framework.facebook.requests;
using blap.framework.facebook.responses;
using Facebook;
using System;
using TinyJSON;

namespace blap.framework.facebook.services
{
  class FacebookService : IFacebookService
  {
    public FacebookService() { }

    public void ActivateApp()
    {
      FB.ActivateApp();
    }

    public bool IsInitialized()
    {
      return FB.IsInitialized;
    }

    public void Initialize(InitDelegate onInitComplete, HideUnityDelegate hideUnityDelegate, string authResponse)
    {
      if (!FB.IsInitialized)
      {
        FB.Init(onInitComplete, hideUnityDelegate, authResponse);
      }
    }

    public bool IsLoggedIn()
    {
      return FB.IsLoggedIn;
    }

    public void Login<T>(string scope, LoginCompleteHandler completeHandler) where T : AbstractFacebookResponse
    {
      FB.Login(scope, (delegate(FBResult result)
      {
        Trace.Log(string.Format("Login complete.\ndata: {0}\nerror: {1}", result.Text, result.Error));
        completeHandler((AbstractFacebookResponse)Activator.CreateInstance(typeof(T), new object[] { result }));
      }));
    }

    public string GetAccessToken()
    {
      return FB.AccessToken;
    }

    public bool IsAccessTokenExpired(DateTime currentDate)
    {
      if(IsLoggedIn())
      {
        int compare = currentDate.CompareTo(FB.AccessTokenExpiresAt);
        if(compare > 0)
        {
          return true;
        }
        return false;
      }
      else
      {
        return true;
      }
    }

    public string GetUserId()
    {
      return FB.UserId;
    }

    public void SendApiRequest<T>(AbstractFacebookApiRequest request, ApiCompleteHandler completeHandler) where T : AbstractFacebookApiResponse
    {
      Trace.Log(string.Format("Sending api request.\nendPoint: {0}\nhttpMethod: {1}\npostData: {2}", request.GetApiCall(), request.httpMethod.ToString(), request.postData != null ? JSON.Dump(request.postData) : "No data"));
      FB.API(request.GetApiCall(), request.httpMethod, (delegate(FBResult result)
      {
        Trace.Log(string.Format("Api request complete.\ndata: {0}\nerror: {1}", result.Text, result.Error));
        completeHandler((AbstractFacebookApiResponse)Activator.CreateInstance(typeof(T), new object[] { result }));
      }), request.postData);
    }
  }
}
