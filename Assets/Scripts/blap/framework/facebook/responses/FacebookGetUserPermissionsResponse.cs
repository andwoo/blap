using System.Collections.Generic;

namespace facebookservices
{
  public class FacebookGetUserPermissionsResponse : AbstractFacebookApiResponse
  {
    public List<FacebookPermission> permissions { get; private set; }

    public override void ParseResponse(FBResult result)
    {
      base.ParseResponse(result);
      if (base.success)
      {
        permissions = base.data.Make<List<FacebookPermission>>();
      }
    }

    public override string ToString()
    {
      return TinyJSON.JSON.Dump(this, TinyJSON.EncodeOptions.PrettyPrint);
    }
  }

  public class FacebookPermission
  {
    public string permission = string.Empty;
    public string status = string.Empty;
  }
}
