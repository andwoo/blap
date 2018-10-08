using System.IO;
using UnityEngine;

namespace Burzum.IO
{
  public static class FileSystem
  {
    private static string ResolvePath(string path)
    {
      if (!string.IsNullOrEmpty(path) && path.IndexOf('/') == 0)
      {
        path = path.Substring(1);
      }
      string originPath;
#if UNITY_EDITOR
      originPath = Application.dataPath + "/../";
#else
      originPath = Application.persistentDataPath + "/";
#endif
      return Path.Combine(originPath, path);
    }

    public static bool FileExists(string path, bool prependFullPath = false)
    {
      return File.Exists(prependFullPath ? ResolvePath(path) : path);
    }

    public static bool DirectoryExists(string path, bool removeFileName = true, bool prependFullPath = false)
    {
      path = prependFullPath ? ResolvePath(path) : path;
      if (removeFileName)
      {
        path = Path.GetDirectoryName(path);
      }
      return Directory.Exists(path);
    }

    public static void CreateDirectory(string path, bool prependFullPath = false)
    {
      path = prependFullPath ? ResolvePath(path) : path;
      Directory.CreateDirectory(Path.GetDirectoryName(path));
    }

    public static bool WriteFile(string path, string data)
    {
      path = ResolvePath(path);
      if (!DirectoryExists(path))
      {
        CreateDirectory(path);
      }

      bool success = false;

      try
      {
        using(StreamWriter stream = new StreamWriter(path))
        {
          stream.Write(data);
        }
        success = true;
      }
      catch { }

      return success;
    }

    public static bool WriteFileAsJSON(string path, object data)
    {
      return WriteFile(path, TinyJSON.JSON.Dump(data));
    }

    public static ReadResponse ReadFile(string path)
    {
      path = ResolvePath(path);
      bool fileExists = FileExists(path);

      if (fileExists)
      {
        bool success = false;
        string data = string.Empty;
        try
        {
          using (StreamReader stream = new StreamReader(path))
          {
            data = stream.ReadToEnd();
          }
          success = true;
        }
        catch { }

        return new ReadResponse(success, fileExists, data);
      }
      else
      {
        return new ReadResponse(false, fileExists, string.Empty);
      }
    }

    public static ReadJSONResponse<TResponse> ReadJSONFileAndMake<TResponse>(string path)
    {
      ReadResponse response = ReadFile(path);

      if (response.Success)
      {
        bool success = false;
        TResponse data = default(TResponse);
        try
        {
          data = TinyJSON.JSON.Load(response.Data).Make<TResponse>();
          success = true;
        }
        catch { }

        return new ReadJSONResponse<TResponse>(success, response.FileExists, data);
      }
      else
      {
        return new ReadJSONResponse<TResponse>(response.Success, response.FileExists, default(TResponse));
      }
    }
  }
}
