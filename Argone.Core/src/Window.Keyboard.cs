namespace Argone.Core
{
    using System;
    using Argone.Core.Input;
    using GLFW;

    internal partial class Window
    {
        internal class Keyboard: InputDevice, IKeyboard, IDisposable
        {
            private Window window;
        
            public Keyboard(Window window)
            {
                this.window = window;
                this.SubscribeToEvents();
            }

            public event EventHandler<InputEventArgs<KeyEventArgs>> KeyPress;

            public event EventHandler<InputEventArgs<KeyEventArgs>> KeyRelease;

            public event EventHandler<InputEventArgs<KeyEventArgs>> KeyRepeat;

            public void Dispose()
            {
                this.UnsubscribeFromEvents();
            }

            private void SubscribeToEvents()
            {
                this.window.nativeWindow.KeyPress += this.OnKeyPress;
                this.window.nativeWindow.KeyRelease += this.OnKeyRelease;
                this.window.nativeWindow.KeyRepeat += this.OnKeyRepeat;
            }

            private void UnsubscribeFromEvents()
            {
                this.window.nativeWindow.KeyPress -= this.OnKeyPress;
                this.window.nativeWindow.KeyRelease -= this.OnKeyRelease;
                this.window.nativeWindow.KeyRepeat -= this.OnKeyRepeat;
            }
            
            private void OnKeyPress(object sender, KeyEventArgs e)
            {
                if (this.KeyPress == null)
                {
                    return;
                }
                
                this.InvokeEvent(this.KeyPress, e);
            }

            private void OnKeyRelease(object sender, KeyEventArgs e)
            {
                if (this.KeyRelease == null)
                {
                    return;
                }
                
                this.InvokeEvent(this.KeyRelease, e);
            }

            private void OnKeyRepeat(object sender, KeyEventArgs e)
            {
                if (this.KeyRepeat == null)
                {
                    return;
                }
                
                this.InvokeEvent(this.KeyRepeat, e);
            }
        }
    }
}