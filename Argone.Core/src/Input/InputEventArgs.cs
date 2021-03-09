namespace Argone.Core.Input
{
    using System;

    public class InputEventArgs: EventArgs
    {
        public bool Handled { get; set; }
    }
    
    public class InputEventArgs<TEventArgs> : InputEventArgs where TEventArgs: EventArgs
    {
        public InputEventArgs(TEventArgs innerEventArgs)
        {
            this.InnerEventArgs = innerEventArgs;
        }
        
        public TEventArgs InnerEventArgs { get; }
    }
}