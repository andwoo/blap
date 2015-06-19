using debugconsole;
using Facebook;
using System;
using System.Collections.Generic;

namespace facebookservices
{
  public delegate void FacebookServiceInitialized();
  public delegate void FacebookServiceResponse<TResponse>(TResponse response) where TResponse : AbstractFacebookResponse, new();

  public static class FacebookService
  {
    public static void Initialize(FacebookServiceInitialized callback)
    {
      if (!FB.IsInitialized)
      {
        FB.Init(delegate()
        {
          FB.ActivateApp();
          callback();
        });
      }
      else
      {
        Trace.Log("Facebook service is already initialized", UnityEngine.LogType.Warning);
      }
    }

    public static bool IsLoggedIn()
    {
      return FB.IsLoggedIn;
    }

    public static string GetAccessToken()
    {
      return FB.AccessToken;
    }

    public static bool IsAccessTokenExpired(DateTime currentDate)
    {
      if (IsLoggedIn())
      {
        int compare = currentDate.CompareTo(FB.AccessTokenExpiresAt);
        if (compare > 0)
        {
          return true;
        }
        return false;
      }
      else
      {
        return true;
      }
    }

    public static void Login(string[] loginPermissions, FacebookServiceResponse<FacebookLoginResponse> callback)
    {
      FB.Login(loginPermissions != null ? string.Join(",", loginPermissions) : string.Empty,
        delegate(FBResult result)
        {
          HandleFacebookResponse<FacebookLoginResponse>(result, callback);
        });
    }

    public static void Logout()
    {
      FB.Logout();
    }

    public static void GetUserPermission(FacebookServiceResponse<FacebookGetUserPermissionsResponse> callback)
    {
      SendApiRequest<FacebookGetUserPermissionsResponse>("/me/permissions", HttpMethod.GET, null, callback);
    }

    public static void GetFriends(string[] fields, FacebookServiceResponse<FacebookFriendsResponse> callback)
    {
      SendApiRequest<FacebookFriendsResponse>(BuildApiFieldsCall("/me/friends", fields), HttpMethod.GET, null, callback);
    }

    public static void GetUserDetails(string[] fields, FacebookServiceResponse<FacebookGetUserDetailsResponse> callback)
    {
      SendApiRequest<FacebookGetUserDetailsResponse>(BuildApiFieldsCall("/me", fields), HttpMethod.GET, null, callback);
    }

    private static void SendApiRequest<TResponse>(string apiCall, HttpMethod httpMethod, Dictionary<string, string> postData, FacebookServiceResponse<TResponse> callback) where TResponse : AbstractFacebookResponse, new()
    {
      FB.API(apiCall, httpMethod, 
        delegate(FBResult result)
        {
          HandleFacebookResponse<TResponse>(result, callback);
        }, postData);
    }

    private static void HandleFacebookResponse<TResponse>(FBResult result, FacebookServiceResponse<TResponse> callback) where TResponse : AbstractFacebookResponse, new()
    {
      TResponse response = new TResponse();
      response.ParseResponse(result);
      callback(response);
    }

    private static string BuildApiFieldsCall(string call, string[] fields)
    {
      return fields != null && fields.Length > 0 ? call += "fields=" + string.Join(",", fields) : call;
    }
  }
}
