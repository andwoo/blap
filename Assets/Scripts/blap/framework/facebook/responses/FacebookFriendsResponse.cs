using System.Collections.Generic;

namespace facebookservices
{
  public class FacebookFriendsResponse : AbstractFacebookApiResponse
  {
    public List<FacebookFriend> friends { get; private set; }

    public override void ParseResponse(FBResult result)
    {
      base.ParseResponse(result);
      if (base.success)
      {
        friends = data.Make<List<FacebookFriend>>();
      }
    }

    public override string ToString()
    {
      return TinyJSON.JSON.Dump(this, TinyJSON.EncodeOptions.PrettyPrint);
    }
  }

  public class FacebookFriend
  {
    public string id = "";
    public string about = "";
    public string name = "";
    public string first_name = "";
    public string last_name = "";
  }
}
