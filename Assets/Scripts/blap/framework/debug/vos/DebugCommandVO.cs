namespace debugconsole
{
  class DebugCommandVO
  {
    public string command { get; private set; }
    public string[] parameters { get; private set; }

    public DebugCommandVO(string cmd, string[] args)
    {
      command = cmd;
      parameters = args;
    }
  }
}
