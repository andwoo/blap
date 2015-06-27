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
      ViewManager.instance.RegisterLayer((int)LayerEnum.BACKGROUND, "Background", LayerTypeEnum.SINGLE_VIEW_LAYER);
      ViewManager.instance.RegisterLayer((int)LayerEnum.HUD, "HUD", LayerTypeEnum.SINGLE_VIEW_LAYER);
      ViewManager.instance.RegisterLayer((int)LayerEnum.GUI, "GUI", LayerTypeEnum.SINGLE_VIEW_LAYER);
      ViewManager.instance.RegisterLayer((int)LayerEnum.MODAL, "Modal", LayerTypeEnum.MULTI_VIEW_LAYER);
    }

    private static void RegisterViews()
    {
      ViewManager.instance.RegisterView((int)ViewEnum.INTRO_LOADING, (int)LayerEnum.GUI, "introloading/IntroLoading");
      ViewManager.instance.RegisterView((int)ViewEnum.MAIN_MENU, (int)LayerEnum.GUI, "mainmenu/MainMenu");
      ViewManager.instance.RegisterView((int)ViewEnum.PLAYER_PROFILE, (int)LayerEnum.GUI, "player/PlayerProfile");
    }
  }
}
