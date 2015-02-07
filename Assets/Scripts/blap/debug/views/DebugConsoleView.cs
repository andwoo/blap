using blap.baseclasses.views;
using blap.debug.events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace blap.debug.views
{
  public class DebugConsoleView : BlapView
  {
    [SerializeField]
    private GameObject[] _toggleObjectViews = null;

    [SerializeField]
    private InputField _textInput = null;

    [SerializeField]
    private Scrollbar _scrollBar = null;

    private bool _openConsole = false;

    protected override void OnLoadFinished()
    {
      base.OnLoadFinished();
      ShowConsoleElements(_openConsole);
    }

    private void ShowConsoleElements(bool show)
    {
      for (int i = 0; i < _toggleObjectViews.Length; i++)
      {
        _toggleObjectViews[i].SetActive(show);
      }

      if(show)
      {
        _textInput.OnPointerClick(new PointerEventData(EventSystem.current));
      }
    }

    /*private void OnGUI()
    {
      if (!_openConsole)
      {
        return;
      }

      if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
      {
        OnHandleCommand();
      }
    }*/

    private void OnHandleCommand()
    {
      string input = _textInput.text;
      _textInput.text = "";

      if (!string.IsNullOrEmpty(input))
      {
        dispatcher.Dispatch(DebugConsoleEvent.COMMAND_ENTERED, input);
      }
    }

    public void GUI_OnOpenConsole()
    {
      _openConsole = !_openConsole;
      ShowConsoleElements(_openConsole);
    }

    public void GUI_OnEnterCommand()
    {
      OnHandleCommand();
    }

    public void GUI_OnScrollChanged()
    {
      Debug.Log(_scrollBar.value.ToString());
    }
  }
}
