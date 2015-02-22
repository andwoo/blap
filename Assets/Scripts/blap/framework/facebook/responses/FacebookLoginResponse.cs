
using blap.framework.debug.utils;
using System;
namespace blap.framework.facebook.responses
{
  public class FacebookLoginResponse : AbstractFacebookResponse
  {
    public bool isLoggedIn { get; private set; }
    public string userId { get; private set; }
    public string accessToken { get; private set; }
    public DateTime accessTokenExpireDate { get; private set; }

    public FacebookLoginResponse(FBResult result)
      : base(result)
    {
      if(success)
      {
        isLoggedIn = base.returnData["is_logged_in"];
        userId = base.returnData["user_id"];
        accessToken = base.returnData["access_token"];
        accessTokenExpireDate = !string.IsNullOrEmpty(base.returnData["access_token_expires_at"]) ? Convert.ToDateTime((string)base.returnData["access_token_expires_at"]) : DateTime.MinValue;
      }

      Trace.Log(string.Format("success: {0}\nisLoggedIn: {1}\nuserId: {2}\naccessToken: {3}\nexpiryDate: {4}", success, isLoggedIn, userId, accessToken, accessTokenExpireDate.ToShortDateString()));
    }
  }
}
