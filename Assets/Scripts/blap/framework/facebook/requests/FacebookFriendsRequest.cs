using Facebook;

namespace blap.framework.facebook.requests
{
  class FacebookFriendsRequest : AbstractFacebookApiRequest
  {
    public FacebookFriendsRequest(string fields)
      : base("/me/friends", HttpMethod.GET)
    {
      AddUrlParameter("fields", fields);
    }
  }
}
