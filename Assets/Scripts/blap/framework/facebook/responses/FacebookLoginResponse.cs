using System;

namespace facebookservices
{
  public class FacebookLoginResponse : AbstractFacebookResponse
  {
    public bool isLoggedIn { get; private set; }
    public string userId { get; private set; }
    public string accessToken { get; private set; }
    public DateTime accessTokenExpireDate { get; private set; }

    public override void ParseResponse(FBResult result)
    {
      base.ParseResponse(result);
      if (base.success)
      {
        isLoggedIn = base.returnData["is_logged_in"];
        userId = base.returnData["user_id"];
        accessToken = base.returnData["access_token"];
        accessTokenExpireDate = !string.IsNullOrEmpty(base.returnData["access_token_expires_at"]) ? Convert.ToDateTime((string)base.returnData["access_token_expires_at"]) : DateTime.MinValue;
      }
    }

    public override string ToString()
    {
      return TinyJSON.JSON.Dump(this, TinyJSON.EncodeOptions.PrettyPrint);
    }
  }
}
