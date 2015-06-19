using UnityEngine;

namespace debugconsole
{
  public delegate void InputCommandHandler(string command, string[] args);

  interface IDebugConsole
  {
    /// <summary>
    /// Add a callback for when the debug console receives a command
    /// </summary>
    /// <param name="handler">Handler to play with debug console input</param>
    void AddInputCommandLister(InputCommandHandler handler);

    /// <summary>
    /// Unlisten to the command input event
    /// </summary>
    /// <param name="handler">Handler to remove</param>
    void RemoveInputCommandLister(InputCommandHandler handler);
  }
}
