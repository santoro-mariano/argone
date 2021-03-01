namespace Argone.Widgets
{
    using System.Drawing;
    using Argone.Core.Widgets;
    using SkiaSharp;

    public class ImageWidget: Widget
    {
        private IImageSource source;

        public IImageSource Source
        {
            get => this.source;
            set
            {
                this.source = value;
                this.NotifyGraphicChange();
            }
        }
        
        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(SKCanvas canvas)
        {
            throw new System.NotImplementedException();
        }
    }
}