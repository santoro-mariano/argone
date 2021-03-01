namespace Argone.Core.Options
{
    using System;
    using System.Drawing;

    public class WindowOptions
    {
        public Size InitialSize { get; set; } = Size.Empty;

        public string Title { get; set; } = "Argone Application";

        public Type RootWidgetType { get; set; }
    }
}