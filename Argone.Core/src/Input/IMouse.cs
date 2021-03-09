namespace Argone.Core.Input
{
    using System;
    using System.Drawing;
    using GLFW;

    public interface IMouse
    {
        Point Position { get; }

        event EventHandler<InputEventArgs<MouseButtonEventArgs>> ButtonPress;

        event EventHandler<InputEventArgs> Enter;

        event EventHandler<InputEventArgs> Leave;

        event EventHandler<InputEventArgs<MouseMoveEventArgs>> Moved;

        event EventHandler<InputEventArgs<MouseMoveEventArgs>> Scroll;
        
        void SetCursor(CursorType cursorType);

        void ResetCursor();
    }
}