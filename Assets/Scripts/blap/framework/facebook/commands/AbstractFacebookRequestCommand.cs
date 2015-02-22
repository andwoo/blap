﻿using blap.framework.debug.utils;
using blap.framework.facebook.events;
using blap.framework.facebook.interfaces;
using blap.framework.facebook.requests;
using blap.framework.facebook.responses;
using strange.extensions.command.impl;

namespace blap.framework.facebook.commands
{
  class AbstractFacebookRequestCommand : EventCommand
  {
    [Inject]
    public IFacebookService fbService { get; set; }

    private FacebookServiceEvent _completeEvent;

    protected void SendApiRequest<T>(AbstractFacebookApiRequest request, FacebookServiceEvent completeEvent) where T : AbstractFacebookApiResponse
    {
      this.Retain();
      _completeEvent = completeEvent;
      fbService.SendApiRequest<T>(request, OnApiRequestComplete);
    }

    private void OnApiRequestComplete(AbstractFacebookApiResponse response)
    {
      Trace.Log("API REQUEST COMPLETE BITCH");
      dispatcher.Dispatch(_completeEvent, response);
      this.Release();
    }

    protected void SendLoginRequest<T>(string loginExtendedPermissions, FacebookServiceEvent completeEvent) where T : AbstractFacebookResponse
    {
      this.Retain();
      _completeEvent = completeEvent;
      fbService.Login<T>(loginExtendedPermissions, OnLoginRequestComplete);
    }

    private void OnLoginRequestComplete(AbstractFacebookResponse response)
    {
      Trace.Log("LOGIN REQUEST COMPLETE BITCH");
      dispatcher.Dispatch(_completeEvent, response);
      this.Release();
    }
  }
}
