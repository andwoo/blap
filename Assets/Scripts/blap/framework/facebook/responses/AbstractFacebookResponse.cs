using blap.framework.debug.utils;
using TinyJSON;
namespace blap.framework.facebook.responses
{
  public abstract class AbstractFacebookResponse
  {
    public bool success { get; protected set; }
    public string errorMessage { get; protected set; }
    public Variant returnData { get; protected set; }

    public AbstractFacebookResponse(FBResult result)
    {
      success = string.IsNullOrEmpty(result.Error) ? true : false;
      errorMessage = !success ? result.Error : "";
      returnData = !string.IsNullOrEmpty(result.Text) ? JSON.Load(result.Text) : JSON.Load("{}");
      Trace.Log(string.Format("success: {0}\nerrorMessage: {1}\nReturnData: {2}", success, errorMessage, result.Text));
      result.Dispose();
    }
  }
}
