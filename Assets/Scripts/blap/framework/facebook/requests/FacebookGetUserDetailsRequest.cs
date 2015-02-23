using Facebook;

namespace blap.framework.facebook.requests
{
  class FacebookGetUserDetailsRequest : AbstractFacebookApiRequest
  {
    public FacebookGetUserDetailsRequest(string fields)
      : base("/me", HttpMethod.GET)
    {
      AddUrlParameter("fields", fields);
    }
  }
}
