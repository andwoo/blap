using framework.singleton;

namespace root.player
{
  public class PlayerModel : Singleton<PlayerModel>
  {
    public string name = string.Empty;
    public int swagLevel = 0;
    public int chainLevel = 0;
    public int pantsLevel = 0;
    public int fiyaLevel = 0;

    public PlayerModel() { }

    public static void OverwriteInstance(PlayerModel model)
    {
      _instance = model;
    }
  }
}
