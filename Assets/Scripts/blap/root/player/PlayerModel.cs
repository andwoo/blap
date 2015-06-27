using framework.singleton;

namespace root.player
{
  public class PlayerModel : Singleton<PlayerModel>
  {
    public string name;
    public int swagLevel;
    public int chainLevel;
    public int pantsLevel;
    public int fiyaLevel;

    public PlayerModel() { }

    public static void OverwriteInstance(PlayerModel model)
    {
      _instance = model;
    }
  }
}
