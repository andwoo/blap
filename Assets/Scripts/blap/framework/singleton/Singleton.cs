namespace framework.singleton
{
  public class Singleton<TClass> where TClass : new()
  {
    protected static TClass _instance = default(TClass);

    public static TClass instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new TClass();
        }
        return _instance;
      }
    }
  }
}
