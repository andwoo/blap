using blap.framework.viewmanager.views;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;

namespace blap.framework.viewmanager.mediators
{
  class BlapMediator<TView> : EventMediator where TView : BlapView
  {
    [Inject]
    public TView view { get; set; }

    public override void OnRegister()
    {
      base.OnRegister();
    }

    public override void OnRemove()
    {
      base.OnRemove();
    }

    protected void Redispatch(IEvent evt)
    {
      dispatcher.Dispatch(evt.type, evt.data);
    }
  }
}
