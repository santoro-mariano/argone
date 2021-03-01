namespace Argone.Core.Input
{
    using System;
    using System.Drawing;
    using GLFW;

    public interface IMouse
    {
        Point Position { get; }

        event EventHandler<MouseButtonEventArgs> ButtonPress;

        event EventHandler Enter;

        event EventHandler Leave;

        event EventHandler<MouseMoveEventArgs> Moved;

        event EventHandler<MouseMoveEventArgs> Scroll;
        
        void SetCursor(CursorType cursorType);

        void ResetCursor();
    }
}