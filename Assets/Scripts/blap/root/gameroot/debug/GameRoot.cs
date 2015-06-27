using debugconsole;
using facebookservices;
using UnityEngine;

namespace gameroot
{
  public partial class GameRoot
  {
    private static DebugConsole _console;

    public static void InitializeDebugConsole()
    {
      GameObject go = GameObject.Instantiate(Resources.Load("framework/debug/DebugConsole"), Vector3.zero, Quaternion.identity) as GameObject;
      _console = go.GetComponent<DebugConsole>();
      _console.AddInputCommandLister(HandleCommands);
      Trace.Log("App Startup Complete");
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
        case "wut":
          Debug.Log("BOP BOP");
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
