using System;

namespace blap.debug.interfaces
{
  public interface ICommandContainer
  {
    /// <summary>
    /// Add a debug string command to event translation. Events inserted in here will be triggered by the debug console if the text command matches
    /// </summary>
    /// <param name="command">Command to type in the debug console</param>
    /// <param name="dispatchEvent">Event that will be dispatched if the command is typed in</param>
    void AddCommand(string command, IComparable dispatchEvent);

    /// <summary>
    /// Check if the storage has an entry for the command
    /// </summary>
    /// <param name="command">Debug string command to search for</param>
    /// <returns>If there's an entry</returns>
    bool HasEvent(string command);

    /// <summary>
    /// Returns the matching dispatch event for the command
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    IComparable GetEvent(string command);
  }
}
