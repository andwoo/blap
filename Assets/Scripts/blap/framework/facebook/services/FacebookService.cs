using blap.framework.debug.utils;
using blap.framework.facebook.interfaces;
using blap.framework.facebook.requests;
using blap.framework.facebook.responses;
using Facebook;
using System;

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

    public void Login(string scope, FacebookDelegate callback)
    {
      FB.Login(scope, callback);
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
      Trace.Log("Facebook request to: " + request.GetApiCall());
      FB.API(request.GetApiCall(), request.httpMethod, (delegate(FBResult result)
      {
        Trace.Log("Facebook request complete");
        completeHandler((AbstractFacebookApiResponse)Activator.CreateInstance(typeof(T), new object[] { result }));
      }), request.postData);
    }
  }
}
