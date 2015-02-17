#undef TEST_LOG_HANDLER

using blap.framework.utils;
using blap.framework.viewmanager.views;
using blap.framework.debug.interfaces;
using blap.framework.debug.utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace blap.framework.debug.views
{
  public class DebugConsoleView : BlapView, IDebugConsole
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

    private readonly float _DEFAULT_LINE_SPACING = 5f;
    private TextGenerationSettings _textGenSettings;
    private TextGenerator _textGenerator;

    private event InputCommandHandler InputCommandDispatcher;

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
      _textGenSettings = _log.GetGenerationSettings(_log.rectTransform.rect.size);
      _textGenerator = new TextGenerator();
      _numberOfLinesToDisplay = Convert.ToInt32(Math.Floor(_log.rectTransform.rect.size.y / (_log.fontSize + (_log.lineSpacing * _DEFAULT_LINE_SPACING))));
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
        input = input.Trim();
        InsertLogMessage(input, LogType.Log);
        string[] paramaters = input.Split(' ');
        InputCommandDispatcher(paramaters[0].ToLowerInvariant(), paramaters.Length > 1 ? paramaters.SubArray(1) : null);
      }
    }

    private void InsertLogMessage(string input, LogType type)
    {
      _textGenerator.Populate(input, _textGenSettings);
      UILineInfo[] lines = _textGenerator.GetLinesArray();
      for (int i = 0; i < lines.Length; i++)
      {
        int blockLength = (i + 1) < lines.Length ? ((lines[i + 1].startCharIdx/* - 1*/) - lines[i].startCharIdx) : -1;
        string line = blockLength > 0 ? input.Substring(lines[i].startCharIdx, blockLength) : input.Substring(lines[i].startCharIdx);
        _logContent.Add(String.Format("<color={0}>{1}</color>", GetColourForLogType(type), line.TrimEnd('\r','\n')));
      }
      
      UpdateScrollbar();
      if (_scrollBar.value == 1 || _scrollBar.size == 1)
      {
        UpdateLogView();
      }
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
      int destination = Convert.ToInt32(Math.Floor((_logContent.NumberOfEntries() - _numberOfLinesToDisplay) * (_scrollBar.value * (1 - (_logContent.NumberOfEntries() / _logContent.maxLines)))));
      List<string> messageBlock = _logContent.GetRange(destination, _numberOfLinesToDisplay);
      string block = "";
      for (int i = 0; i < messageBlock.Count; i++)
      {
        block += messageBlock[i] + "\n";
      }

      _log.text = block;
    }

    public void AddInputCommandLister(InputCommandHandler handler)
    {
      InputCommandDispatcher += handler;
    }

    public void RemoveInputCommandLister(InputCommandHandler handler)
    {
      InputCommandDispatcher -= handler;
    }

    /// <summary>
    /// Clears the log cache and resets the view and scrollbar
    /// </summary>
    public void ClearLog()
    {
      _logContent.Clear();
      UpdateScrollbar();
      UpdateLogView();
    }

    /// <summary>
    /// Method toggles the visuals of the debug console
    /// </summary>
    public void GUI_OnOpenConsole()
    {
      _openConsole = !_openConsole;
      ShowConsoleElements(_openConsole);
    }

    /// <summary>
    /// Takes the text in the textinput and triggers a command, the textinput then clears.
    /// </summary>
    public void GUI_OnEnterCommand()
    {
      OnHandleCommand();
    }

    /// <summary>
    /// Called each time the scroll bar is moved. Log view is updated with new text.
    /// </summary>
    public void GUI_OnScrollChanged()
    {
      UpdateLogView();
    }
  }
}
