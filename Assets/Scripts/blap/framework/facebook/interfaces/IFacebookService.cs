using blap.framework.facebook.requests;
using blap.framework.facebook.responses;
using System;

namespace blap.framework.facebook.interfaces
{
  public delegate void LoginCompleteHandler(AbstractFacebookResponse response);
  public delegate void ApiCompleteHandler(AbstractFacebookApiResponse response);

  interface IFacebookService
  {
    /// <summary>
    /// Calling ActivateApp notifies Facebook of the installation of your app. Use this if you're planning to use the mobile app install ads feature. Your impressions will be a function of, among other things, the number of installs you get.
    /// </summary>
    void ActivateApp();
    /// <summary>
    /// Returns a bool indicating if the Facebook SDK has been initialized
    /// </summary>
    /// <returns></returns>
    bool IsInitialized();
    /// <summary>
    /// Initializes the Facebook SDK.
    /// </summary>
    /// <param name="onInitComplete">A function that will be called once all data structures in the SDK are initialized; any code that should synchronize with the player's Facebook session should be in onInitComplete().</param>
    /// <param name="hideUnityDelegate">A function that will be called when Facebook tries to display HTML content within the boundaries of the Canvas. When called with its sole argument set to false, your game should pause and prepare to lose focus. If it's called with its argument set to true, your game should prepare to regain focus and resume play. Your game should check whether it is in fullscreen mode when it resumes, and offer the player a chance to go to fullscreen mode if appropriate.</param>
    /// <param name="authResponse">effective in Web Player only, rarely used A Facebook auth_response you have cached to preserve a session, represented in JSON. If an auth_response is provided, FB will initialize itself using the data from that session, with no additional checks.</param> 
    void Initialize(Facebook.InitDelegate onInitComplete, Facebook.HideUnityDelegate hideUnityDelegate, string authResponse);
    /// <summary>
    /// Check the user's authorization status. false if the user has not logged into Facebook, or hasn't authorized your app; true otherwise. Most often, this will be in the logic that determines whether to show a login control.
    /// </summary>
    /// <returns>The authorization status</returns>
    bool IsLoggedIn();
    /// <summary>
    /// Login using Facebook SDK
    /// </summary>
    /// <param name="scope">A list of Facebook permissions requested from the user</param>
    /// <param name="callback">A delegate that will be passed a AbstractFacebookResponse object. A platform-independent representation is available from the properties FB.UserId and FB.AccessToken, and via the boolean FB.IsLoggedIn.</param>
    void Login<T>(string scope, LoginCompleteHandler completeHandler) where T : AbstractFacebookResponse;
    /// <summary>
    /// The access token granted to your app when the user most recently authorized it; otherwise, an empty string. This value is used implicitly for any FB-namespace method that requires an access token.
    /// </summary>
    /// <returns>Access token granted to your app</returns>
    string GetAccessToken();
    /// <summary>
    /// Returns a bool representing if the access token is expired meaning the user no longer has a valid session with Facebook.
    /// </summary>
    /// <param name="currentDate"></param>
    /// <returns></returns>
    bool IsAccessTokenExpired(DateTime currentDate);
    /// <summary>
    /// The user's Facebook user ID, when a user is logged in and has authorized your app, or an empty string if not.
    /// </summary>
    /// <returns></returns>
    string GetUserId();

    void SendApiRequest<T>(AbstractFacebookApiRequest request, ApiCompleteHandler completeHandler) where T : AbstractFacebookApiResponse;
  }
}
