namespace Burzum.IO
{
  public class ReadResponse
  {
    public bool Success { get; private set; }
    public bool FileExists { get; private set; }
    public string Data { get; private set; }

    public ReadResponse(bool success, bool fileExists, string data)
    {
      Success = success;
      FileExists = fileExists;
      Data = data;
    }
  }

  public class ReadJSONResponse<TResponse> : ReadResponse
  {
    public TResponse jsonData { get; private set; }

    public ReadJSONResponse(bool success, bool fileExists, TResponse data)
      : base(success, fileExists, string.Empty)
    {
      jsonData = data;
    }
  }
}
