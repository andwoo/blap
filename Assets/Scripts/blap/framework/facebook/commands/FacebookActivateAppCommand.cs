using blap.framework.facebook.events;
using blap.framework.facebook.interfaces;
using strange.extensions.command.impl;

namespace blap.framework.facebook.commands
{
  class FacebookActivateAppCommand : EventCommand
  {
    [Inject]
    public IFacebookService fbService { get; set; }

    public override void Execute()
    {
      this.Retain();
      fbService.ActivateApp();
      dispatcher.Dispatch(FacebookServiceEvent.ACTIVATE_APP_COMPLETE);
      this.Release();
    }
  }
}
