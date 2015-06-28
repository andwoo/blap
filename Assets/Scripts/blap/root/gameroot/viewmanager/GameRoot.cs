using UnityEngine;
using viewenums;
using viewmanager;

namespace gameroot
{
  public static partial class GameRoot
  {
    private static ViewManager _instance;

    public static void SetViewManager(ViewManager manager)
    {
      _instance = manager;
      RegisterLayers();
      RegisterViews();
    }

    
    public static ViewManager viewManager
    {
      get
      {
        return _instance;
      }
    }

    private static void RegisterLayers()
    {
      viewManager.RegisterLayer((int)LayerEnum.BACKGROUND, "Background", LayerTypeEnum.SINGLE_VIEW_LAYER);
      viewManager.RegisterLayer((int)LayerEnum.HUD, "HUD", LayerTypeEnum.SINGLE_VIEW_LAYER);
      viewManager.RegisterLayer((int)LayerEnum.GUI, "GUI", LayerTypeEnum.SINGLE_VIEW_LAYER);
      viewManager.RegisterLayer((int)LayerEnum.MODAL, "Modal", LayerTypeEnum.MULTI_VIEW_LAYER);
    }

    private static void RegisterViews()
    {
      viewManager.RegisterView((int)ViewEnum.INTRO_LOADING, (int)LayerEnum.GUI, "introloading/IntroLoading");
      viewManager.RegisterView((int)ViewEnum.MAIN_MENU, (int)LayerEnum.GUI, "mainmenu/MainMenu");
      viewManager.RegisterView((int)ViewEnum.PLAYER_PROFILE, (int)LayerEnum.GUI, "player/PlayerProfile");
    }
  }
}
