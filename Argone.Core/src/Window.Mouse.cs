namespace Argone.Core
{
    using System;
    using System.Drawing;
    using Argone.Core.Input;
    using GLFW;

    internal partial class Window
    {
        internal class Mouse: InputDevice, IMouse, IDisposable
        {
            private Window window;

            public Mouse(Window window)
            {
                this.window = window;
                this.SubscribeToEvents();
            }

            public Point Position => this.window.nativeWindow.MousePosition;

            public event EventHandler<InputEventArgs<MouseButtonEventArgs>> ButtonPress;

            public event EventHandler<InputEventArgs> Enter;

            public event EventHandler<InputEventArgs> Leave;

            public event EventHandler<InputEventArgs<MouseMoveEventArgs>> Moved;

            public event EventHandler<InputEventArgs<MouseMoveEventArgs>> Scroll;
        
            public void SetCursor(CursorType cursorType)
            {
                Glfw.SetCursor(this.window.nativeWindow, Glfw.CreateStandardCursor(CursorType.Hand));
            }

            public void ResetCursor()
            {
                Glfw.SetCursor(this.window.nativeWindow, Cursor.None);
            }
            
            public void Dispose()
            {
                this.UnsubscribeFromEvents();
            }
            
            private void SubscribeToEvents()
            {
                this.window.nativeWindow.MouseButton += this.OnMouseButton;
                this.window.nativeWindow.MouseEnter += this.OnMouseEnter;
                this.window.nativeWindow.MouseLeave += this.OnMouseLeave;
                this.window.nativeWindow.MouseMoved += this.OnMouseMoved;
                this.window.nativeWindow.MouseScroll += this.OnMouseScroll;
            }
            
            private void UnsubscribeFromEvents()
            {
                this.window.nativeWindow.MouseButton -= this.OnMouseButton;
                this.window.nativeWindow.MouseEnter -= this.OnMouseEnter;
                this.window.nativeWindow.MouseLeave -= this.OnMouseLeave;
                this.window.nativeWindow.MouseMoved -= this.OnMouseMoved;
                this.window.nativeWindow.MouseScroll -= this.OnMouseScroll;
            }
            
            private void OnMouseButton(object sender, MouseButtonEventArgs e)
            {
                if (this.ButtonPress == null)
                {
                    return;
                }

                this.InvokeEvent(this.ButtonPress, e);
            }
            
            private void OnMouseEnter(object sender, EventArgs e)
            {
                if (this.Enter == null)
                {
                    return;
                }
                
                this.InvokeEvent(this.Enter);
            }
            
            private void OnMouseLeave(object sender, EventArgs e)
            {
                if (this.Leave == null)
                {
                    return;
                }
                
                this.InvokeEvent(this.Leave);
            }
            
            private void OnMouseMoved(object sender, MouseMoveEventArgs e)
            {
                if (this.Moved == null)
                {
                    return;
                }
                
                this.InvokeEvent(this.Moved, e);
            }
            
            private void OnMouseScroll(object sender, MouseMoveEventArgs e)
            {
                if (this.Scroll == null)
                {
                    return;
                }
                
                this.InvokeEvent(this.Scroll, e);
            }
        }
    }
}