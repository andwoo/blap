using blap.root.commands;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace blap.root.context
{
  class RootContext : MVCSContext
  {
    #region constructors
      public RootContext()
        : base()
      {
      }

      public RootContext(MonoBehaviour view)
        : base(view)
      {
      }

      public RootContext(MonoBehaviour view, bool autoMapping)
        : base(view, autoMapping)
      {
      }

      public RootContext(MonoBehaviour view, ContextStartupFlags flags)
        : base(view, flags)
      {
      }
    #endregion

    public void MapBindings()
    {
      MapViews();
      MapModels();
      MapServices();
      MapCommands();
    }

    private void MapViews()
    {

    }

    private void MapModels()
    {

    }

    private void MapServices()
    {

    }

    private void MapCommands()
    {
      commandBinder.Bind(ContextEvent.START).To<StartupCommand>();
    }
  }
}
