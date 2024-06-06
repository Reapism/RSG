using RSG.Core.Interfaces;
using RSG.Core.Interfaces.Result;
using System;
using System.ComponentModel;

namespace RSG.Core.Utilities
{

    public class ProgressChangedEventArgs : EventArgs
    {
        public ProgressChangedEventArgs(int progress, string message)
        {
            Progress = progress;
            Message = message;
        }

        public int Progress { get; }
        public string Message { get; }
    }
}
