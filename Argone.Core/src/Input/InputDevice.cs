namespace Argone.Core.Input
{
    using System;

    internal abstract class InputDevice
    {
        protected void InvokeEvent(MulticastDelegate eventDelegate)
        {
            var eventArgs = new InputEventArgs();
            this.InvokeEvent((Delegate)eventDelegate, eventArgs);
        }
        
        protected void InvokeEvent<TInnerEvent>(MulticastDelegate eventDelegate, TInnerEvent innerEvent) where TInnerEvent : EventArgs
        {
            var eventArgs = new InputEventArgs<TInnerEvent>(innerEvent);
            this.InvokeEvent((Delegate)eventDelegate, eventArgs);
        }

        private void InvokeEvent(Delegate eventDelegate, InputEventArgs eventArgs)
        {
            foreach (var eventHandler in eventDelegate.GetInvocationList())
            {
                eventHandler.Method.Invoke(eventHandler.Target, new object[] {this, eventArgs});
                if (eventArgs.Handled)
                {
                    break;
                }
            }
        }
    }
}