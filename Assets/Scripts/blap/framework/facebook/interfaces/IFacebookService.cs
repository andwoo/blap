namespace blap.framework.facebook.interfaces
{
  interface IFacebookService
  {
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
    /// Login using Facebook SDK
    /// </summary>
    /// <param name="scope">A list of Facebook permissions requested from the user</param>
    /// <param name="callback">A delegate that will be passed a FBResult object. A platform-independent representation is available from the properties FB.UserId and FB.AccessToken, and via the boolean FB.IsLoggedIn.</param>
    void Login(string scope, Facebook.FacebookDelegate callback);

    bool IsLoggedIn();
  }
}
