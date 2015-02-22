using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blap.framework.facebook.requests
{
  class FacebookGetUserDetailsRequest : AbstractFacebookApiRequest
  {
    private static string _CALL = "/me";

    public FacebookGetUserDetailsRequest(string fields)
      : base(_CALL, HttpMethod.GET)
    {
      AddUrlParameter("fields", fields);
    }
  }
}
