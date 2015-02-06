using blap.debug.mediators;
using blap.debug.views;
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

    override protected void mapBindings()
    {
      MapViews();
      MapModels();
      MapServices();
      MapCommands();
    }

    override protected void postBindings()
    {
    }

    private void MapViews()
    {
      mediationBinder.Bind<DebugConsoleView>().To<DebugConsoleMediator>();
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
