namespace viewmanager
{
  public class ViewInfo
  {
    public int id { get; private set; }
    public int layerId { get; private set; }
    public string prefabPath { get; private set; }
    public string assetBundePath { get; private set; }
    public bool active { get; set; }

    public ViewInfo(int viewId, int parentLayerId, string prefabResourcePath, string bundlePath)
    {
      id = viewId;
      layerId = parentLayerId;
      prefabPath = prefabResourcePath;
      assetBundePath = bundlePath;
    }
  }
}
