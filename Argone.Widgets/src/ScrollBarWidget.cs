namespace Argone.Widgets
{
    using System;
    using System.Drawing;
    using Argone.Core.Extensions;
    using Argone.Core.Input;
    using Argone.Core.Widgets;
    using GLFW;
    using SkiaSharp;

    public class ScrollBarWidget: ContainerWidget
    {
        private readonly IMouse mouse;

        private RectangleF dimensions;
        private int width = 20;
        private SKRect backgroundRect;
        private Color barColor = Color.Gray;
        private SKPaint barPaint = Color.Gray.ToSKPaint();
        private Color backgroundColor = Color.FromArgb(125, Color.Gray);
        private SKPaint backgroundPaint = Color.FromArgb(125, Color.Gray).ToSKPaint();

        public ScrollBarWidget(IMouse mouse)
        {
            this.mouse = mouse;
            this.mouse.Moved += this.OnMouseMoved;
            this.mouse.Leave += this.OnMouseMoved;
            this.mouse.Enter += this.OnMouseMoved;
        }

        public int Width
        {
            get => this.width;
            set
            {
                this.width = value;
                this.NotifyStructureChange();
            }
        }

        public Color BarColor
        {
            get => this.barColor;
            set
            {
                this.barColor = value;
                this.barPaint = value.ToSKPaint();
                this.NotifyGraphicChange();
            }
        }

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
            this.backgroundRect = new SKRect(bounds.Width - this.Width, bounds.Top, bounds.Right, bounds.Bottom);
            this.dimensions = this.backgroundRect.ToRectangle();
            this.Content.Layout(new RectangleF(bounds.X, bounds.Y, bounds.Width - this.Width, bounds.Height), dpiScale);
            return bounds;
        }

        public override void Draw(SKCanvas canvas)
        {
            canvas.DrawRect(this.backgroundRect, this.backgroundPaint);
            var borderRadius = this.Width - 4 / 2;
            canvas.DrawRoundRect(SKRect.Inflate(this.backgroundRect, -2, -2), new SKSize(borderRadius, borderRadius), this.barPaint);
            this.Content?.Draw(canvas);
        }
        
        private void OnMouseMoved(object sender, EventArgs e)
        {
            if (this.dimensions.Contains(this.mouse.Position))
            {
                this.BarColor = Color.FromArgb(255, Color.Gray);
                this.mouse.SetCursor(CursorType.Hand);
            }
            else
            {
                this.BarColor = Color.FromArgb(125, Color.Gray);
                this.mouse.ResetCursor();
            }
        }
    }
}