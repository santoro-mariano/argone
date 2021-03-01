namespace Argone.Core.Input
{
    using System;
    using GLFW;

    public interface IKeyboard
    {
        event EventHandler<KeyEventArgs> KeyPress;

        event EventHandler<KeyEventArgs> KeyRelease;

        event EventHandler<KeyEventArgs> KeyRepeat;
    }
}