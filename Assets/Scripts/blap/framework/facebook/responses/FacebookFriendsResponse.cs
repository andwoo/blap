using System.Collections.Generic;

namespace blap.framework.facebook.responses
{
  class FacebookFriendsResponse : AbstractFacebookApiResponse
  {
    public List<FacebookFriend> friends { get; private set; }

    public FacebookFriendsResponse(FBResult result)
      : base(result)
    {
      if (base.success)
      {
        List<FacebookFriend> temp;
        data.Make<List<FacebookFriend>>(out temp);
        friends = temp;
      }
    }
  }

  class FacebookFriend
  {
    public string id = "";
    public string about = "";
    public string name = "";
    public string first_name = "";
    public string last_name = "";
  }
}
