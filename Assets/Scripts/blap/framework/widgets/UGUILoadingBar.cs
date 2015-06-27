using extensions;
using UnityEngine;
using UnityEngine.UI;

namespace framework.widgets
{
  [ExecuteInEditMode]
  public class UGUILoadingBar : MonoBehaviour
  {
    [SerializeField]
    private Image _loadingBar = null;

    [SerializeField, Range(0f, 1f)]
    private float _percentage = 0f;

    public void SetPercentage(float loadPercentage)
    {
      _percentage = loadPercentage.Clamp<float>(0f, 1f);
    }

    void Update()
    {
      if (_loadingBar != null && _loadingBar.rectTransform.localScale.x != _percentage)
      {
        _loadingBar.rectTransform.localScale = new Vector3(_percentage, 1f, 1f);
      }
    }
  }
}
