#undef TEST_LOG_HANDLER

using blap.baseclasses.utils;
using blap.baseclasses.views;
using blap.debug.events;
using blap.debug.utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace blap.debug.views
{
  public class DebugConsoleView : BlapView
  {
    /// <summary>
    /// List of Game objects in which we toggle their view when the console is to be displayed
    /// </summary>
    [SerializeField]
    private GameObject[] _toggleObjectViews = null;

    /// <summary>
    /// The text input bar that we type our commands into
    /// </summary>
    [SerializeField]
    private InputField _textInput = null;

    /// <summary>
    /// The scroll bar that controls what text gets viewed
    /// </summary>
    [SerializeField]
    private Scrollbar _scrollBar = null;

    /// <summary>
    /// The log textfield in which we display our console output
    /// </summary>
    [SerializeField]
    private Text _log = null;

    /// <summary>
    /// The number of console logs we will store. Lines will be removed as we hit the cap of stored logs
    /// </summary>
    [SerializeField]
    private int _numberOfLinesToCache = 3000;

    /// <summary>
    /// The colour of the default log messages
    /// </summary>
    [SerializeField]
    private Color _logColour = default(Color);

    /// <summary>
    /// The colour of messages that are tagged with assert
    /// </summary>
    [SerializeField]
    private Color _assertColour = default(Color);

    /// <summary>
    /// The colour of messages that are tagged with warning
    /// </summary>
    [SerializeField]
    private Color _warningColour = default(Color);

    /// <summary>
    /// The colour of messages that are tagged with error
    /// </summary>
    [SerializeField]
    private Color _errorColour = default(Color);

    /// <summary>
    /// The colour of messages that are tagged with exception
    /// </summary>
    [SerializeField]
    private Color _exceptionColour = default(Color);

    /// <summary>
    ///number of lines the log textfield can display, used for calculating the amount of text to show when scrolling
    /// </summary>
    private int _numberOfLinesToDisplay = 0;
    /// <summary>
    /// number of characters per line the log textfield can display, used for calculating the amount of text to show when scrolling
    /// </summary>
    private int _numberOfCharsPerLine = 0;
    /// <summary>
    /// used for toggling the view of the console's main components
    /// </summary>
    private bool _openConsole = false;
    /// <summary>
    /// the buffer in which we store our console logs
    /// </summary>
    private ConsoleBuffer _logContent;
    /// <summary>
    /// the storage of our log colours. used to highlight special text in the log textfield
    /// </summary>
    private Dictionary<LogType, Color> _consoleColours;

    /// <summary>
    /// This callback is called each time Unity's Debug methods are invoked. We use it to display content logged into our debug console.
    /// </summary>
    /// <param name="logString">The message object that was logged</param>
    /// <param name="stackTrace">The stack trace leading to the point where the log was invoked</param>
    /// <param name="type">The type of log. ex. error, warning, ect...</param>
    private void UnityHandleLog(string logString, string stackTrace, LogType type)
    {
      switch (type)
      {
        case LogType.Log:
        case LogType.Warning:
          InsertLogMessage(logString, type);
          break;
        case LogType.Assert:
        case LogType.Error:
        case LogType.Exception:
        default:
          InsertLogMessage(logString + "\n" + stackTrace, type);
          break;
      }
    }

    protected override void OnCreateFinished()
    {
      base.OnCreateFinished();

      //setup all the colour values to be quickly accessed
      _consoleColours = new Dictionary<LogType, Color>();
      _consoleColours.Add(LogType.Log, _logColour);
      _consoleColours.Add(LogType.Assert, _assertColour);
      _consoleColours.Add(LogType.Warning, _warningColour);
      _consoleColours.Add(LogType.Error, _errorColour);
      _consoleColours.Add(LogType.Exception, _exceptionColour);
    }

    protected override void OnLoadFinished()
    {
      base.OnLoadFinished();

      //determine the available text dimensions
      _numberOfLinesToDisplay = Convert.ToInt32(Math.Floor(_log.rectTransform.rect.height / (_log.fontSize + (_log.fontSize / 8))));
      _numberOfCharsPerLine = Convert.ToInt32(Math.Floor(_log.rectTransform.rect.width / (_log.fontSize - (_log.fontSize / 2))));
      //initialize the console buffer storage with the max amount of lines it can handle
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
      _logContent.Add(SplitLogMessages(0, Regex.Split(logString, "\r\n"), new List<string>(), type));
      UpdateScrollbar();
      if (_scrollBar.value == 1 || _scrollBar.size == 1)
      {
        UpdateLogView();
      }
    }

    private List<string> SplitLogMessages(int index, string[] original, List<string> output, LogType type)
    {
      if (index < original.Length)
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
              output.Add("<color=" + GetColourForLogType(type) + ">" + parsed + "</color>");
              localIndex += parsed.Length;
            }
            else
            {
              output.Add("<color=" + GetColourForLogType(type) + ">" + original[index].Substring(localIndex) + "</color>");
              break;
            }
          }
        }
        else
        {
          output.Add("<color=" + GetColourForLogType(type) + ">" + original[index] + "</color>");
        }
        SplitLogMessages(index + 1, original, output, type);
      }
      return output;
    }

    private string GetColourForLogType(LogType type)
    {
      return ColourUtils.ColourToHexValue(_consoleColours[type], true);
    }

    private void UpdateScrollbar()
    {
      float size = MathUtils.Clamp<float>(Convert.ToSingle(_numberOfLinesToDisplay) / Convert.ToSingle(_logContent.NumberOfEntries()), 0, 1);
      _scrollBar.size = size;
    }

    private void UpdateLogView()
    {
      int destination = Convert.ToInt32(Math.Floor((_logContent.NumberOfEntries() - _numberOfLinesToDisplay) * _scrollBar.value));
      List<string> messageBlock = _logContent.GetRange(destination, _numberOfLinesToDisplay);
      string block = "";
      for (int i = 0; i < messageBlock.Count; i++)
      {
        block += messageBlock[i] + "\n";
      }

      _log.text = block;
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
