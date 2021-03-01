namespace Argone.Core.Widgets
{
    using System;
    using System.Drawing;
    using SkiaSharp;

    public interface IWidget
    {
        event EventHandler<ChangedEventArgs> Changed;

        RectangleF Layout(RectangleF bounds, PointF dpiScale);
        
        void Draw(SKCanvas canvas);
    }
}