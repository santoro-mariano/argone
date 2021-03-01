namespace Argone.Widgets
{
    using System.Drawing;
    using Argone.Core.Extensions;
    using Argone.Core.Widgets;
    using SkiaSharp;

    public class TextWidget: Widget
    {
        private RectangleF textBounds;
        private PointF dpiScale;
        
        private string text;
        private string fontFamily;
        private float fontSize;
        private Color fontColor;
        private SKPaint paint;

        public TextWidget()
        {
            this.fontColor = Color.Black;
            this.fontSize = 12;
            this.RefreshPaint();
        }

        public string Text
        {
            get => this.text;
            set
            {
                this.text = value;
                this.NotifyStructureChange();
            }

        }

        public string FontFamily
        {
            get => this.fontFamily;
            set
            {
                this.fontFamily = value;
                this.RefreshPaint();
                this.NotifyStructureChange();
            }
        }

        public float FontSize
        {
            get => this.fontSize;
            set
            {
                this.fontSize = value;
                this.RefreshPaint();
                this.NotifyStructureChange();
            }
        }

        public Color FontColor
        {
            get => this.fontColor;
            set
            {
                this.fontColor = value;
                this.RefreshPaint();
                this.NotifyGraphicChange();
            }
        }

        private void RefreshPaint()
        {
            this.paint = this.fontColor.ToSKPaint();
            this.paint.TextSize = this.fontSize * 96 / 72 * this.dpiScale.Y;
            this.paint.IsAntialias = true;
        }

        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            this.textBounds = bounds;
            this.dpiScale = dpiScale;
            this.RefreshPaint();
            var txtBounds = SKRect.Empty;
            this.paint.MeasureText(this.text, ref txtBounds);
            return new RectangleF(bounds.Left + txtBounds.Left, bounds.Top, txtBounds.Width, txtBounds.Height);
        }

        public override void Draw(SKCanvas canvas)
        {
            var txtBounds = SKRect.Empty;
            this.paint.MeasureText(this.text, ref txtBounds);
            canvas.DrawText(this.text, this.textBounds.X, this.textBounds.Y + txtBounds.Height, this.paint);
        }
    }
}