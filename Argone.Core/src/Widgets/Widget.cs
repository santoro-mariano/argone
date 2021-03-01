namespace Argone.Core.Widgets
{
    using System;
    using System.Drawing;
    using SkiaSharp;

    public abstract class Widget: IWidget
    {
        public event EventHandler<ChangedEventArgs> Changed;
        
        public abstract RectangleF Layout(RectangleF bounds, PointF dpiScale);

        public abstract void Draw(SKCanvas canvas);

        protected void NotifyStructureChange()
        {
            this.NotifyChange(this, new ChangedEventArgs(ChangeType.Structure));
        }
        
        protected void NotifyGraphicChange()
        {
            this.NotifyChange(this, new ChangedEventArgs(ChangeType.Graphic));
        }

        protected void NotifyChange(object sender, ChangedEventArgs eventArgs)
        {
            this.Changed?.Invoke(sender, eventArgs);
        }
    }
}