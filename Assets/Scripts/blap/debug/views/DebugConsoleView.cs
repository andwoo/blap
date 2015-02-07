using blap.baseclasses.utils;
using blap.baseclasses.views;
using blap.debug.events;
using blap.debug.utils;
using System;
using System.Collections.Generic;
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

    [SerializeField]
    private Text _log = null;

    [SerializeField]
    private int _numberOfLinesToDisplay = 10;

    [SerializeField]
    private int _numberOfLinesToCache = 3000;

    private bool _openConsole = false;
    private ConsoleBuffer _logContent;

    protected override void OnLoadFinished()
    {
      base.OnLoadFinished();
      _logContent = new ConsoleBuffer(_numberOfLinesToCache);
      _log.text = "";
      //Application.RegisterLogCallback(HandleLog);

      ShowConsoleElements(_openConsole);
    }

    private void OnDestroy()
    {
        Application.RegisterLogCallback(null);
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

    private void OnGUI()
    {
      if (!_openConsole)
      {
        return;
      }

      if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
      {
        OnHandleCommand();
      }
    }

    private void Update()
    {
      if (_scrollBar.value == 1)
      {
        //always show the lastest debug data if the scroll bar is at the bottom
        DisplayLog();
      }
    }

    private void OnHandleCommand()
    {
      string input = _textInput.text;
      _textInput.text = "";

      if (!string.IsNullOrEmpty(input))
      {
        _logContent.Add(input);
        UpdateScrollbar();
        dispatcher.Dispatch(DebugConsoleEvent.COMMAND_ENTERED, input);
        DisplayLog();
      }
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
      switch(type)
      {
        case LogType.Log:
          _logContent.Add(logString);
          break;
        case LogType.Assert:
        case LogType.Error:
        case LogType.Exception:
        case LogType.Warning:
        default:
          _logContent.Add(logString + "\n" + stackTrace);
          break;
      }
    }

    private void DisplayLog()
    {
      Debug.Log("Displaying log " + _scrollBar.value);
      List<string> log = _logContent.GetRange(Convert.ToInt32(Math.Floor((_logContent.NumberOfEntries() - _numberOfLinesToDisplay) * _scrollBar.value)), _numberOfLinesToDisplay);
      _log.text = "";
      for(int i = 0; i < log.Count; i++)
      {
        _log.text += log[i] + "\n";
      }
    }

    private void UpdateScrollbar()
    {
      float size = MathUtils.Clamp<float>(Convert.ToSingle(_numberOfLinesToDisplay) / Convert.ToSingle(_logContent.NumberOfEntries()), 0, 1);
      _scrollBar.size = size;
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
      DisplayLog();
    }
  }
}
