#undef TEST_LOG_HANDLER

using blap.baseclasses.utils;
using blap.baseclasses.views;
using blap.debug.events;
using blap.debug.utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    private int _numberOfLinesToCache = 3000;

    private int _fontSize = 0;
    private int _numberOfLinesToDisplay = 0;
    private int _numberOfCharsPerLine = 0;
    private bool _openConsole = false;
    private ConsoleBuffer _logContent;

    private void UnityHandleLog(string logString, string stackTrace, LogType type)
    {
      switch (type)
      {
        case LogType.Log:
        case LogType.Warning:
          InsertLogMessage(type.ToString() + " : " + logString, type);
          break;
        case LogType.Assert:
        case LogType.Error:
        case LogType.Exception:
        default:
          InsertLogMessage(type.ToString() + " : " + logString + "\n" + stackTrace, type);
          break;
      }
    }

    protected override void OnLoadFinished()
    {
      base.OnLoadFinished();

      _fontSize = _log.fontSize;
      _numberOfLinesToDisplay = Convert.ToInt32(Math.Floor(_log.rectTransform.rect.height / (_fontSize + (_fontSize / 8))));
      _numberOfCharsPerLine = Convert.ToInt32(Math.Floor(_log.rectTransform.rect.width / (_fontSize - (_fontSize / 2))));
      _logContent = new ConsoleBuffer(_numberOfLinesToCache);

#if !UNITY_EDITOR || TEST_LOG_HANDLER
      Application.RegisterLogCallback(UnityHandleLog);
#endif
      ShowConsoleElements(_openConsole);
    }

    private void OnDestroy()
    {
#if !UNITY_EDITOR || TEST_LOG_HANDLER
      Application.RegisterLogCallback(null);
#endif
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

    private void OnHandleCommand()
    {
      string input = _textInput.text;
      _textInput.text = "";

      if (!string.IsNullOrEmpty(input))
      {
        InsertLogMessage(input, LogType.Log);
        dispatcher.Dispatch(DebugConsoleEvent.COMMAND_ENTERED, input);
      }
    }

    private void InsertLogMessage(string logString, LogType type)
    {
      List<string> original = new List<string>();
      original.AddRange(Regex.Split(logString, "\r\n"));

      List<string> newstuff = new List<string>();
      _logContent.Add(SplitLogMessages(0, original, newstuff));
      UpdateScrollbar();
      if (_scrollBar.value == 1 || _scrollBar.size == 1)
      {
        UpdateLogView();
      }
    }

    private List<string> SplitLogMessages(int index, List<string> original, List<string> output)
    {
      if (index < original.Count)
      {
        if(original[index].Length > _numberOfCharsPerLine)
        {
          int strLen = original[index].Length;
          int localIndex = 0;
          while(localIndex < strLen)
          {
            if(localIndex + _numberOfCharsPerLine < strLen)
            {
              string parsed = original[index].Substring(localIndex, _numberOfCharsPerLine);
              output.Add(parsed);
              localIndex += parsed.Length;
            }
            else
            {
              output.Add(original[index].Substring(localIndex));
              break;
            }
          }
        }
        else
        {
          output.Add(original[index]);
        }
        SplitLogMessages(index + 1, original, output);
      }
      return output;
    }

    private void UpdateScrollbar()
    {
      float size = MathUtils.Clamp<float>(Convert.ToSingle(_numberOfLinesToDisplay) / Convert.ToSingle(_logContent.NumberOfEntries()), 0, 1);
      _scrollBar.size = size;
    }

    private void UpdateLogView()
    {
      int destination = Convert.ToInt32(Math.Floor((_logContent.NumberOfEntries() - _numberOfLinesToDisplay) * _scrollBar.value));
      List<string> log = _logContent.GetRange(destination, _numberOfLinesToDisplay);
      _log.text = "";
      for (int i = 0; i < log.Count; i++)
      {
        _log.text += log[i] + "\n";
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
      UpdateLogView();
    }
  }
}
