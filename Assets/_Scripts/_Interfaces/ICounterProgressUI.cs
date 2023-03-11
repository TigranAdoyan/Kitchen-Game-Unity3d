using System;

public interface ICounterProgressUI
{
    public event EventHandler<OnProgressEventArgs> OnProgressEvent;
    public class OnProgressEventArgs : EventArgs
    {
        public float value;
        public float timeout;
        public bool status;
        public bool auto;
    }
}
