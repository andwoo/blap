using blap.framework.debug.utils;
using blap.framework.facebook.events;
using blap.framework.facebook.interfaces;
using blap.framework.facebook.requests;
using blap.framework.facebook.responses;
using strange.extensions.command.impl;

namespace blap.framework.facebook.commands
{
  class AbstractFacebookApiRequestCommand : EventCommand
  {
    [Inject]
    public IFacebookService fbService { get; set; }

    private FacebookServiceEvent _completeEvent;

    protected void SendApiRequest<T>(AbstractFacebookApiRequest request, FacebookServiceEvent completeEvent) where T : AbstractFacebookApiResponse
    {
      this.Retain();
      _completeEvent = completeEvent;
      fbService.SendApiRequest<T>(request, OnRequestComplete);
    }

    private void OnRequestComplete(AbstractFacebookApiResponse response)
    {
      Trace.Log("REQUEST COMPLETE BITCH");
      dispatcher.Dispatch(_completeEvent, response);
      this.Release();
    }
  }
}
