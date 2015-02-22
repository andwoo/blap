using blap.framework.coroutinerunner.interfaces;
using blap.framework.debug.commands;
using blap.framework.debug.events;
using blap.framework.debug.interfaces;
using blap.framework.debug.mediators;
using blap.framework.debug.models;
using blap.framework.debug.utils;
using blap.framework.debug.views;
using blap.framework.facebook.commands;
using blap.framework.facebook.events;
using blap.framework.facebook.interfaces;
using blap.framework.facebook.services;
using blap.framework.webdownloader.commands;
using blap.framework.webdownloader.events;
using blap.framework.webdownloader.interfaces;
using blap.framework.webdownloader.services;
using blap.framework.www.factories;
using blap.root.commands;
using framework.coroutinerunner.runners;
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
        debugCommands.AddCommand("cls", DebugConsoleEvent.CLEAR_CONSOLE);
        debugCommands.AddCommand("clear", DebugConsoleEvent.CLEAR_CONSOLE);
        debugCommands.AddCommand("dl_img", WebDownloadEvent.DOWNLOAD_TEXTURE);
        debugCommands.AddCommand("fb_act", FacebookServiceEvent.ACTIVATE_APP);
        debugCommands.AddCommand("fb_init", FacebookServiceEvent.INITIALIZE);
        debugCommands.AddCommand("fb_login", FacebookServiceEvent.LOGIN);
        debugCommands.AddCommand("fb_friends", FacebookServiceEvent.GET_FRIENDS);
        debugCommands.AddCommand("fb_me", FacebookServiceEvent.GET_USER_DETAILS);
        
        injectionBinder.Bind<ICommandContainer>().ToValue(debugCommands).ToSingleton();
      #endregion
    }

    private void MapServices()
    {
      #region web_downloader
        injectionBinder.Bind<IWebDownloadService>().To<WebDownloadService>().ToSingleton();
      #endregion

      #region routine_runner
      CoroutineRunner runner = (base.contextView as GameObject).GetComponent<CoroutineRunner>();
      if (runner != null)
      {
        WWWFactory.instance.SetRoutineRunner(runner);
        injectionBinder.Bind<ISimpleRoutineRunner>().ToValue(runner).ToSingleton();
      }
      else
      {
        Trace.Log("CoroutineRunner was not found on ContextView", LogType.Error);
      }
      #endregion

      #region facebook
        injectionBinder.Bind<IFacebookService>().To<FacebookService>().ToSingleton();
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

      #region facebook
        commandBinder.Bind(FacebookServiceEvent.INITIALIZE).To<FacebookInitializeCommand>();
        commandBinder.Bind(FacebookServiceEvent.LOGIN).To<FacebookLoginCommand>();
        commandBinder.Bind(FacebookServiceEvent.ACTIVATE_APP).To<FacebookActivateAppCommand>();
        commandBinder.Bind(FacebookServiceEvent.GET_FRIENDS).To<FacebookGetFriendsCommand>();
        commandBinder.Bind(FacebookServiceEvent.GET_USER_DETAILS).To<FacebookGetUserDetailsCommand>();
      #endregion
    }

    private void MapDebugCommands()
    {
      #region debug_console
        commandBinder.Bind(DebugConsoleEvent.COMMAND_ENTERED).To<DebugRouteCommand>();
      #endregion
    }
  }
}
