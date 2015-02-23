using System.Collections.Generic;

namespace blap.framework.facebook.responses
{
  class FacebookGetUserPermissionsResponse : AbstractFacebookApiResponse
  {
    public List<FacebookPermission> permissions { get; private set; }

    public FacebookGetUserPermissionsResponse(FBResult result)
      : base(result)
    {
      if (base.success)
      {
        List<FacebookPermission> temp;
        base.data.Make<List<FacebookPermission>>(out temp);
        permissions = temp;
      }
    }
  }

  class FacebookPermission
  {
    public string permission;
    public string status;
  }
}
