using System;

namespace BuildCleanUp;

public class ProgressUpdatedEventArgs(string message) : EventArgs
{
    /// <summary>
    /// Contains current state information about the process
    /// </summary>
    public string Message { get; private set; } = message;
}