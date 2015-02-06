using blap.baseclasses.views;
using strange.extensions.mediation.impl;

namespace blap.baseclasses.mediators
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
  }
}
