namespace Argone.Widgets
{
    using System.Drawing;
    using Argone.Core.Extensions;
    using Argone.Core.Widgets;
    using SkiaSharp;

    public class ViewWidget: ContainerWidget
    {
        private Color backgroundColor = Color.White;
        private SKColor backgroundSkiaColor = SKColors.White;

        public Color BackgroundColor
        {
            get => this.backgroundColor;
            set
            {
                this.backgroundColor = value;
                this.backgroundSkiaColor = this.backgroundColor.ToSKColor();
                this.NotifyGraphicChange();
            }
        }
        
        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            this.Content?.Layout(bounds, dpiScale);
            return bounds;
        }

        public override void Draw(SKCanvas canvas)
        {
            canvas.Clear(this.backgroundSkiaColor);
            this.Content?.Draw(canvas);
        }
    }
}