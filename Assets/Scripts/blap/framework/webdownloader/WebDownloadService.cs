using UnityEngine;
using www;

namespace webdownloader
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
      delegate(WWW httpRequest)
      {
        callback(true, httpRequest.texture);
      },
      delegate(WWW httpRequest, short errorCode, string errorMessage)
      {
        callback(false, null);
      }).SendRequest();
    }

    public static void DownloadAudioClip(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadAudioClipResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      delegate(WWW httpRequest)
      {
        callback(true, httpRequest.audioClip);
      },
      delegate(WWW httpRequest, short errorCode, string errorMessage)
      {
        callback(false, null);
      }).SendRequest();
    }

    public static void DownloadAssetBunle(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadAssetBundleResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      delegate(WWW httpRequest)
      {
        callback(true, httpRequest.assetBundle);
      },
      delegate(WWW httpRequest, short errorCode, string errorMessage)
      {
        callback(false, null);
      }).SendRequest();
    }

    public static void DownloadText(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadTextResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      delegate(WWW httpRequest)
      {
        callback(true, httpRequest.text);
      },
      delegate(WWW httpRequest, short errorCode, string errorMessage)
      {
        callback(false, null);
      }).SendRequest();
    }

    public static void DownloadBytes(string url, bool cacheBust, float timeOutLimit, short retryLimit, WebDownloadBytesResponse callback)
    {
      WWWFactory.CreateGetRequest(url, cacheBust, timeOutLimit, retryLimit,
      delegate(WWW httpRequest)
      {
        callback(true, httpRequest.bytes);
      },
      delegate(WWW httpRequest, short errorCode, string errorMessage)
      {
        callback(false, null);
      }).SendRequest();
    }
  }
}