using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.mediation.api;
using UnityEngine;

namespace blap.baseclasses.views
{
  public class BlapView : MonoBehaviour, IView
  {
    #region IView
      /// Indicates whether the View can work absent a context
      /// 
      /// Leave this value true most of the time. If for some reason you want
      /// a view to exist outside a context you can set it to false. The only
      /// difference is whether an error gets generated.
      private bool _requiresContext = true;
      public bool requiresContext { get { return _requiresContext; } set { _requiresContext = value; } }

      /// Indicates whether this View  has been registered with a Context
      private bool _registeredWithContext = true;
      public bool registeredWithContext { get { return _registeredWithContext; } set { _registeredWithContext = value; } }

      /// Exposure to code of the registerWithContext (Inspector) boolean. If false, the View won't try to register.
      public bool autoRegisterWithContext { get { return true; } }
    #endregion

    public IEventDispatcher dispatcher { get; private set; }

    private void Awake()
    {
      dispatcher = new EventDispatcher();
      OnCreateFinished();
    }

    private void Start()
    {
      OnLoadFinished();
    }

    protected virtual void OnCreateFinished()
    {
    }

    protected virtual void OnLoadFinished()
    {
    }
  }
}
