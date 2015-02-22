using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blap.framework.facebook.responses
{
  class FacebookGetUserDetailsResponse : AbstractFacebookApiResponse
  {
    public FacebookUser userDetails { get; private set; }

    public FacebookGetUserDetailsResponse(FBResult result)
      : base(result)
    {
      if (base.success)
      {
        FacebookUser temp = new FacebookUser();
        base.returnData.Make<FacebookUser>(out temp);
        userDetails = temp;
      }
    }
  }

  class FacebookUser
  {
    public string id = "";
    public string first_name = "";
    public string gender = "";
    public string last_name = "";
    public string link = "";
    public string locale = "";
    public string name = "";
    public int timezone = 0;
    public string updated_time = "";
    public bool verified = false;
  }
}
