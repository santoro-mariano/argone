namespace Argone.Core.Widgets
{
    using System;
    
    public class ChangedEventArgs: EventArgs
    {
        public ChangedEventArgs(ChangeType changeType)
        {
            this.ChangeType = changeType;
        }
        
        public ChangeType ChangeType { get; set; }
    }
}