using debugconsole;
using facebookservices;
using UnityEngine;

namespace Assets.Scripts.blap.root
{
  public class GameRoot : MonoBehaviour
  {
    private DebugConsole _console;

    private void Start()
    {
      GameObject go = Instantiate(Resources.Load("framework/debug/DebugConsole")) as GameObject;
      _console = go.GetComponent<DebugConsole>();
      _console.AddInputCommandLister(HandleCommands);
      Trace.Log("App Startup Complete");
    }

    private void HandleCommands(string command, string[] args)
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
      }
    }

    private void FBInitResponse()
    {
      Trace.Log("Facebook service initialized");
    }

    private void FBLoginResponse(FacebookLoginResponse response)
    {
      Trace.Log(response.ToString());
    }

    private void FBMeResponse(FacebookGetUserDetailsResponse response)
    {
      Trace.Log(response.ToString());
    }

    private void FBPermissionsResponse(FacebookGetUserPermissionsResponse response)
    {
      Trace.Log(response.ToString());
    }

    private void FBFriendsResponse(FacebookFriendsResponse response)
    {
      Trace.Log(response.ToString());
    }
  }
}
