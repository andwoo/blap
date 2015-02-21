using blap.framework.facebook.interfaces;
using Facebook;
using System;

namespace blap.framework.facebook.services
{
  class FacebookService : IFacebookService
  {
    public FacebookService() { }

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
  }
}
