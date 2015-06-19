namespace facebookservices
{
  public class FacebookGetUserDetailsResponse : AbstractFacebookApiResponse
  {
    public FacebookUser userDetails { get; private set; }

    public override void ParseResponse(FBResult result)
    {
      base.ParseResponse(result);
      if (base.success)
      {
        userDetails = base.returnData.Make<FacebookUser>();
      }
    }

    public override string ToString()
    {
      return TinyJSON.JSON.Dump(this, TinyJSON.EncodeOptions.PrettyPrint);
    }
  }

  public class FacebookUser
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
