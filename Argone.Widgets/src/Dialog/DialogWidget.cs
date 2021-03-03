namespace Argone.Widgets
{
    using System.Drawing;
    using Argone.Core.Extensions;
    using Argone.Core.Widgets;
    using SkiaSharp;

    public class DialogWidget: WidgetWithContent
    {
        private Color backgroundColor = Color.FromArgb(125, Color.Black);
        private SKPaint backgroundPaint = Color.FromArgb(125, Color.Black).ToSKPaint();
        private SKRect backgroundRect;
        
        public Color BackgroundColor
        {
            get => this.backgroundColor;
            set
            {
                this.backgroundColor = value;
                this.backgroundPaint = value.ToSKPaint();
                this.NotifyGraphicChange();
            }
        }
        
        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            this.backgroundRect = bounds.ToSkRect();
            this.Content.Layout(bounds, dpiScale);
            return bounds;
        }

        public override void Draw(SKCanvas canvas)
        {
            this.Content.Draw(canvas);
            canvas.DrawRect(this.backgroundRect, this.backgroundPaint);
        }
    }
}