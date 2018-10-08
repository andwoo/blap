using UnityEngine;
using UnityEngine.Networking;

namespace Burzum.Net
{
  public delegate void WebDownloadTextureResponse(bool success, Texture2D texture);
  public delegate void WebDownloadAudioClipResponse(bool success, AudioClip audio);
  public delegate void WebDownloadAssetBundleResponse(bool success, AssetBundle bundle);
  public delegate void WebDownloadTextResponse(bool success, string text);
  public delegate void WebDownloadBytesResponse(bool success, byte[] bytes);

  public static class WebDownloadService
  {
    public static void DownloadImage(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadTextureResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      (UnityWebRequest httpRequest) =>
      {
        callback(true, (httpRequest.downloadHandler as DownloadHandlerTexture).texture);
      },
      (UnityWebRequest httpRequest, short errorCode, string errorMessage) =>
      {
        callback(false, null);
      }).SendRequest();
    }

    public static void DownloadAudioClip(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadAudioClipResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      (UnityWebRequest httpRequest) =>
      {
        callback(true, (httpRequest.downloadHandler as DownloadHandlerAudioClip).audioClip);
      },
      (UnityWebRequest httpRequest, short errorCode, string errorMessage) =>
      {
        callback(false, null);
      }).SendRequest();
    }

    public static void DownloadAssetBunle(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadAssetBundleResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      (UnityWebRequest httpRequest) =>
      {
        callback(true, (httpRequest.downloadHandler as DownloadHandlerAssetBundle).assetBundle);
      },
      (UnityWebRequest httpRequest, short errorCode, string errorMessage) =>
      {
        callback(false, null);
      }).SendRequest();
    }

    public static void DownloadText(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadTextResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      (UnityWebRequest httpRequest) =>
      {
        callback(true, httpRequest.downloadHandler.text);
      },
      (UnityWebRequest httpRequest, short errorCode, string errorMessage) =>
      {
        callback(false, null);
      }).SendRequest();
    }

    public static void DownloadBytes(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadBytesResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      (UnityWebRequest httpRequest) =>
      {
        callback(true, httpRequest.downloadHandler.data);
      },
      (UnityWebRequest httpRequest, short errorCode, string errorMessage) =>
      {
        callback(false, null);
      }).SendRequest();
    }
  }
}