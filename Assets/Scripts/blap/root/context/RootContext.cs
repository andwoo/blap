using blap.framework.debug.commands;
using blap.framework.debug.events;
using blap.framework.debug.interfaces;
using blap.framework.debug.mediators;
using blap.framework.debug.models;
using blap.framework.debug.views;
using blap.framework.webdownloader.commands;
using blap.framework.webdownloader.events;
using blap.framework.webdownloader.interfaces;
using blap.framework.webdownloader.services;
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
      MapDebugCommands();
    }

    override protected void postBindings()
    {
    }

    private void MapViews()
    {
      #region debug_console
        mediationBinder.Bind<DebugConsoleView>().To<DebugConsoleMediator>();
      #endregion
    }

    private void MapModels()
    {
      #region debug_console
        ICommandContainer debugCommands = new CommandContainer();
        debugCommands.AddCommand("clr", DebugConsoleEvent.CLEAR_CONSOLE);
        debugCommands.AddCommand("clear", DebugConsoleEvent.CLEAR_CONSOLE);
        debugCommands.AddCommand("dl_txt", WebDownloadEvent.DOWNLOAD_TEXTURE);
        injectionBinder.Bind<ICommandContainer>().ToValue(debugCommands).ToSingleton();
      #endregion
    }

    private void MapServices()
    {
      #region web_downloader
        injectionBinder.Bind<IWebDownloadService>().To<WebDownloadService>().ToSingleton();
      #endregion
    }

    private void MapCommands()
    {
      #region startup
        commandBinder.Bind(ContextEvent.START).To<StartupCommand>();
      #endregion

      #region web_downloader
        commandBinder.Bind(WebDownloadEvent.DOWNLOAD_TEXTURE).To<DownloadTextureCommand>();
      #endregion
    }

    private void MapDebugCommands()
    {
      #region debug_console
        commandBinder.Bind(DebugConsoleEvent.COMMAND_ENTERED).To<DebugRouteCommand>();
      #endregion

      #region debug_commands
      #endregion
    }
  }
}
