using blap.framework.facebook.events;
using Facebook;

namespace blap.framework.facebook.requests
{
  class FacebookFriendsRequest : AbstractFacebookApiRequest
  {
    public static string _CALL = "/me/friends";

    public FacebookFriendsRequest(string fields)
      : base(_CALL, HttpMethod.GET)
    {
      AddUrlParameter("fields", fields);
    }
  }
}
