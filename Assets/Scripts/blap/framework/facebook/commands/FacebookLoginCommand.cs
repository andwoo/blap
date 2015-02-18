using blap.framework.debug.utils;
using blap.framework.facebook.events;
using blap.framework.facebook.interfaces;
using strange.extensions.command.impl;

namespace blap.framework.facebook.commands
{
  class FacebookLoginCommand : EventCommand
  {
    [Inject]
    public IFacebookService fbService { get; set; }

    public override void Execute()
    {
      this.Retain();
      fbService.Login("", OnLoginComplete);
    }

    private void OnLoginComplete(FBResult result)
    {
      if(fbService.IsLoggedIn())
      {
        Trace.Log("LOGIN_COMPLETE_SUCCESS");
        dispatcher.Dispatch(FacebookServiceEvent.LOGIN_COMPLETE_SUCCESS);
      }
      else
      {
        Trace.Log("LOGIN_COMPLETE_FAILED: " + result.Error);
        dispatcher.Dispatch(FacebookServiceEvent.LOGIN_COMPLETE_FAILED);
      }
      this.Release();
    }
  }
}
