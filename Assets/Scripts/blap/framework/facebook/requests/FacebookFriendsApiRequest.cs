

using blap.framework.facebook.events;
using Facebook;
namespace blap.framework.facebook.requests
{
  class FacebookFriendsApiRequest : AbstractFacebookApiRequest
  {
    public static string _CALL = "/me/friends";

    public FacebookFriendsApiRequest(string fields)
      : base(_CALL, HttpMethod.GET)
    {
      if (!string.IsNullOrEmpty(fields))
      {
        AddUrlParameter("fields", fields);
      }
    }
  }
}
