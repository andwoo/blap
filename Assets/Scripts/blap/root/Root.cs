using DG.Tweening;
using gameroot;
using UnityEngine;

namespace root
{
  public class Root : MonoBehaviour
  {
    private void Start()
    {
      DOTween.Init();

      GameRoot.InitializeDebugConsole();
      GameRoot.InitializeViewManager();
    }
  }
}
