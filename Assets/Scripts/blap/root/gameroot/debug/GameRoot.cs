using debugconsole;
using facebookservices;
using UnityEngine;
using viewenums;

namespace gameroot
{
  public static partial class GameRoot
  {
    private static DebugConsole _console;

    public static void SetDebugConsole(DebugConsole console)
    {
      _console = console;
      _console.AddInputCommandLister(HandleCommands);
    }

    public static DebugConsole console
    {
      get
      {
        return _console;
      }
    }

    private static void HandleCommands(string command, string[] args)
    {
      switch (command)
      {
        case "cls":
        case "clear":
          _console.ClearLog();
          break;
        case "fb_init":
          FacebookService.Initialize(FBInitResponse);
          break;
        case "fb_login":
          FacebookService.Login(null, FBLoginResponse);
          break;
        case "fb_logout":
          FacebookService.Logout();
          break;
        case "fb_me":
          FacebookService.GetUserDetails(null, FBMeResponse);
          break;
        case "fb_perms":
          FacebookService.GetUserPermission(FBPermissionsResponse);
          break;
        case "fb_friends":
          FacebookService.GetFriends(null, FBFriendsResponse);
          break;
        case "load":
          GameRoot.viewManager.PushView((int)ViewEnum.INTRO_LOADING);
          break;
        case "player":
          GameRoot.viewManager.PushView((int)ViewEnum.PLAYER_PROFILE);
          break;
        case "menu":
          GameRoot.viewManager.PushView((int)ViewEnum.MAIN_MENU);
          break;
      }
    }

    private static void FBInitResponse()
    {
      Trace.Log("Facebook service initialized");
    }

    private static void FBLoginResponse(FacebookLoginResponse response)
    {
      Trace.Log(response.ToString());
    }

    private static void FBMeResponse(FacebookGetUserDetailsResponse response)
    {
      Trace.Log(response.ToString());
    }

    private static void FBPermissionsResponse(FacebookGetUserPermissionsResponse response)
    {
      Trace.Log(response.ToString());
    }

    private static void FBFriendsResponse(FacebookFriendsResponse response)
    {
      Trace.Log(response.ToString());
    }
  }
}
