using blap.framework.debug.utils;

namespace blap.framework.facebook.responses
{
  class FacebookFriendsApiResponse : AbstractFacebookApiResponse
  {
    public FacebookFriendsApiResponse(FBResult result)
      : base(result)
    {
      //{"data":[{"name":"Alec Loldwin","id":"1572987722940420"}]
      Trace.Log("Result: " + result.Text);

      //todo parse that shit
      //make nice logs for facebook naw mean
    }
  }
}
