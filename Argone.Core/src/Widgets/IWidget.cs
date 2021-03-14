namespace Argone.Core.Widgets
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using SkiaSharp;

    public interface IWidget: IEnumerable<IWidget>
    {
        IWidget Parent { get; set; }
        
        bool IsVisible { get; }
        
        event EventHandler<ChangedEventArgs> Changed;

        RectangleF Layout(RectangleF bounds, PointF dpiScale);
        
        void Draw(SKCanvas canvas);
    }
}