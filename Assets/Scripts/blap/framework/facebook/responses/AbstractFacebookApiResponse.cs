using blap.framework.extensions;
using TinyJSON;

namespace blap.framework.facebook.responses
{
  public abstract class AbstractFacebookApiResponse : AbstractFacebookResponse
  {
    protected Variant data { get; private set; }

    public AbstractFacebookApiResponse(FBResult result)
      : base(result)
    {
      if (base.success)
      {
        if(base.returnData.ContainsKey("data"))
        {
          data = base.returnData["data"];
        }
      }
    }
  }
}
