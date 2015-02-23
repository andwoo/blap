using Facebook;

namespace blap.framework.facebook.requests
{
  class FacebookGetUserPermissionsRequest : AbstractFacebookApiRequest
  {
    public FacebookGetUserPermissionsRequest()
      : base("/me/permissions", HttpMethod.GET)
    {
    }
  }
}
