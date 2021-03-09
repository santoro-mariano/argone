namespace Argone.Core.Input
{
    using System;
    using GLFW;

    public interface IKeyboard
    {
        event EventHandler<InputEventArgs<KeyEventArgs>> KeyPress;

        event EventHandler<InputEventArgs<KeyEventArgs>> KeyRelease;

        event EventHandler<InputEventArgs<KeyEventArgs>> KeyRepeat;
    }
}