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

    public RootContext root { get; private set; }

    private void Awake()
    {
      root = new RootContext(this, _startupFlag);

      switch (_startupFlag)
      {
        case ContextStartupFlags.MANUAL_MAPPING:
          root.Start();
          break;
        case ContextStartupFlags.MANUAL_LAUNCH:
          root.Launch();
          break;
        case ContextStartupFlags.AUTOMATIC:
        default:
          break;
      }
    }
  }
}
