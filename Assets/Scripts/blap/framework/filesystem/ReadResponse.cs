namespace filesystem
{
  public class ReadResponse
  {
    public bool success { get; private set; }
    public bool fileExists { get; private set; }
    public string data { get; private set; }

    public ReadResponse(bool succ, bool fExists, string dat)
    {
      success = succ;
      fileExists = fExists;
      data = dat;
    }
  }

  public class ReadJSONResponse<TResponse> : ReadResponse
  {
    public TResponse jsonData { get; private set; }

    public ReadJSONResponse(bool succ, bool fExists, TResponse dat)
      : base(succ, fExists, string.Empty)
    {
      jsonData = dat;
    }
  }
}
