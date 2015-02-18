using blap.framework.facebook.interfaces;
using Facebook;

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

    public void Login(string scope, FacebookDelegate callback)
    {
      FB.Login(scope, callback);
    }

    public bool IsLoggedIn()
    {
      return FB.IsLoggedIn;
    }
  }
}
