using extensions;
using TinyJSON;

namespace facebookservices
{
  public abstract class AbstractFacebookApiResponse : AbstractFacebookResponse
  {
    protected Variant data { get; private set; }

    public override void ParseResponse(FBResult result)
    {
      base.ParseResponse(result);
      if (base.success)
      {
        if (base.returnData.ContainsKey("data"))
        {
          data = base.returnData["data"];
        }
      }
    }
  }
}
