using blap.root.context;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace blap.root.views
{
  class RootContextView : ContextView
  {
    [SerializeField]
    private ContextStartupFlags _startupFlag = ContextStartupFlags.AUTOMATIC;

    private RootContext _root;

    private void Awake()
    {
      _root = new RootContext(this, _startupFlag);

      switch (_startupFlag)
      {
        case ContextStartupFlags.MANUAL_MAPPING:
          _root.Start();
          _root.MapBindings();
          _root.Launch();
          break;
        case ContextStartupFlags.MANUAL_LAUNCH:
          _root.MapBindings();
          _root.Launch();
          break;
        case ContextStartupFlags.AUTOMATIC:
        default:
          break;
      }
    }
  }
}
