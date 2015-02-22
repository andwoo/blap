using blap.framework.debug.utils;
using blap.framework.facebook.events;
using blap.framework.facebook.interfaces;
using strange.extensions.command.impl;

namespace blap.framework.facebook.commands
{
  class FacebookInitializeCommand : EventCommand
  {
    [Inject]
    public IFacebookService fbService { get; set; }

    public override void Execute()
    {
      if (!fbService.IsInitialized())
      {
        this.Retain();
        fbService.Initialize(OnInitializeComplete, OnAppMinimized, null);
      }
    }

    private void OnInitializeComplete()
    {
      dispatcher.Dispatch(FacebookServiceEvent.INITIALIZE_COMPLETE);
      this.Release();
    }

    private void OnAppMinimized(bool isUnityShown)
    {
      if (isUnityShown)
      {
        dispatcher.Dispatch(FacebookServiceEvent.INITIALIZE_UNITY_RESUME);
      }
      else
      {
        dispatcher.Dispatch(FacebookServiceEvent.INITIALIZE_UNITY_MINIMIZE);
      }
    }
  }
}
