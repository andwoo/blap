using viewenums;
using viewmanager;

namespace gameroot
{
  public partial class GameRoot
  {
    public static void InitializeViewManager()
    {
      RegisterLayers();
      RegisterViews();
    }

    public static ViewManager viewManager
    {
      get
      {
        return ViewManager.instance;
      }
    }

    private static void RegisterLayers()
    {
      ViewManager.instance.RegisterLayer((int)LayerEnum.BACKGROUND, "Background");
      ViewManager.instance.RegisterLayer((int)LayerEnum.HUD, "HUD");
      ViewManager.instance.RegisterLayer((int)LayerEnum.GUI, "GUI");
      ViewManager.instance.RegisterLayer((int)LayerEnum.MODAL, "Modal");
    }

    private static void RegisterViews()
    {
      ViewManager.instance.RegisterView((int)ViewEnum.TEST_VIEW, (int)LayerEnum.GUI, "root/TestView");
    }
  }
}
