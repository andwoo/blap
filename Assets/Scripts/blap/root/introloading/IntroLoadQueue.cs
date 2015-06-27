using coroutinerunner;
using filesystem;
using framework.actionqueue;
using gameroot;
using root.player;
using System;
using System.Collections;

namespace root.introloading
{
  public class IntroLoadQueue : IDisposable
  {
    private ActionQueue _loadQ;
    private int _initialLoadSize;

    public IntroLoadQueue()
    {
      _loadQ = new ActionQueue();
      _loadQ.queueComplete += OnLoadComplete;
    }

    public void Dispose()
    {
      _loadQ.queueComplete -= OnLoadComplete;
      _loadQ.Dispose();
    }

    public void Enqueue(IAction loadAction)
    {
      _loadQ.Enqueue(loadAction);
    }

    public void StartLoadQueue()
    {
      _initialLoadSize = _loadQ.QueueSize();
      CoroutineRunner.StartCoroutine(UpdateProgress());
      _loadQ.StartQueue();
    }

    private IEnumerator UpdateProgress()
    {
      while (_loadQ.QueueSize() > 0)
      {
        yield return null;
        GameRoot.globalDispatcher.DispatchEvent(IntroLoadEvent.UPDATE_PERCENTAGE, (float)(_loadQ.QueueSize() / _initialLoadSize));
      }
      GameRoot.globalDispatcher.DispatchEvent(IntroLoadEvent.UPDATE_PERCENTAGE, 1f);
    }

    private void OnLoadComplete()
    {
      Dispose();
      GameRoot.globalDispatcher.DispatchEvent(IntroLoadEvent.LOAD_COMPLETE, null);
    }
  }

  public class LoadPlayerModelAction : IAction
  {
    public void StartAction(ActionItemComplete onActionComplete)
    {
      GameRoot.globalDispatcher.DispatchEvent(IntroLoadEvent.UPDATE_LOAD_TEXT, "Loading Player Data");

      ReadJSONResponse<PlayerModel> response = FileSystem.ReadJSONFileAndMake<PlayerModel>("/gameData/playermodel.json");
      if (response.success)
      {
        PlayerModel.OverwriteInstance(response.jsonData);
      }
      onActionComplete();
    }

    public void Dispose() { }
  }
}
