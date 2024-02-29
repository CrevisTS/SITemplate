﻿using System;

namespace SITemplate.Core.Events
{
    public class ProgressMessageEventArgs : EventArgs
    {
        public string Message { get; }
        public int ProgressValue { get; }
        public ProgressMessageEventArgs(string message, int progressValue)
        {
            Message = message;
            ProgressValue = progressValue;
        }
    }
}
