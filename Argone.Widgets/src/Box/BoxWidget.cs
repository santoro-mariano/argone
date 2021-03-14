namespace Argone.Widgets
{
    using System.Drawing;
    using Argone.Core.Extensions;
    using Argone.Core.Widgets;
    using SkiaSharp;

    public class BoxWidget: ContainerWidget
    {
        private RectangleF borderBounds;
        private RectangleF contentBounds;
        
        private SKPaint leftBorderPaint;
        private SKPaint topBorderPaint;
        private SKPaint rightBorderPaint;
        private SKPaint bottomBorderPaint;
        private SKPaint backgroundPaint;
        
        private Color backgroundColor;
        private Edges<float> margin;
        private Edges<Border> border;
        private Edges<float> padding;
        private bool adjustSizeToChild;

        public Edges<float> Margin
        {
            get => this.margin;
            set
            {
                this.margin = value;
                this.NotifyStructureChange();
            }
        }

        public Edges<Border> Border
        {
            get => this.border;
            set
            {
                this.border = value;
                this.GenerateBorderPaints();
                this.NotifyStructureChange();
            }
        }
        
        public SizeF BorderRadius { get; set; }

        public Edges<float> Padding
        {
            get => this.padding;
            set
            {
                this.padding = value;
                this.NotifyStructureChange();
            }
        }

        public Color BackgroundColor
        {
            get => this.backgroundColor;
            set
            {
                this.backgroundColor = value;
                this.backgroundPaint = this.backgroundColor.ToSKPaint();
                this.NotifyGraphicChange();
            }
        }

        public bool AdjustSizeToChild
        {
            get => this.adjustSizeToChild;
            set
            {
                this.adjustSizeToChild = value;
                this.NotifyStructureChange();
            }
        }

        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            if (this.AdjustSizeToChild && this.Content != null)
            {
                this.ResizeToChild(bounds, dpiScale);
            }
            else
            {
                this.ResizeToBounds(bounds, dpiScale);
            }

            return new RectangleF(this.borderBounds.X - this.Margin.Left,
                                  this.borderBounds.Y - this.Margin.Top,
                                  this.borderBounds.Width + this.Margin.Left + this.Margin.Right,
                                  this.borderBounds.Height + this.Margin.Top + this.Margin.Bottom);
        }

        public override void Draw(SKCanvas canvas)
        {
            if (this.leftBorderPaint != null && this.Border.Left.Size > 0)
            {
                canvas.DrawRect(this.borderBounds.X, this.borderBounds.Y, this.Border.Left.Size, this.borderBounds.Height, this.leftBorderPaint);
            }
            
            if (this.topBorderPaint != null && this.Border.Top.Size > 0)
            {
                canvas.DrawRect(this.borderBounds.X + this.Border.Left.Size, this.borderBounds.Y, this.borderBounds.Width - this.Border.Left.Size  - this.Border.Right.Size, this.Border.Top.Size, this.topBorderPaint);
            }

            if (this.rightBorderPaint != null && this.Border.Right.Size > 0)
            {
                canvas.DrawRect(this.borderBounds.X + this.borderBounds.Width - this.Border.Right.Size, this.borderBounds.Y, this.Border.Right.Size, this.borderBounds.Height, this.rightBorderPaint);
            }

            if (this.bottomBorderPaint != null && this.Border.Bottom.Size > 0)
            {
                canvas.DrawRect(this.borderBounds.X + this.Border.Left.Size, this.borderBounds.Y + this.borderBounds.Height - this.Border.Bottom.Size, this.borderBounds.Width - this.Border.Left.Size  - this.Border.Right.Size, this.Border.Bottom.Size, this.bottomBorderPaint);
            }
            
            if (this.backgroundPaint != null)
            {
                canvas.DrawRect(this.contentBounds.X, this.contentBounds.Y, this.contentBounds.Width, this.contentBounds.Height, this.backgroundPaint);
            }
            
            this.Content?.Draw(canvas);
        }
        
        private void ResizeToChild(RectangleF bounds, PointF dpiScale)
        {
            var x = bounds.X + this.Margin.Left + this.border.Left.Size + this.padding.Left;
            var y = bounds.Y + this.Margin.Top + this.border.Top.Size + this.padding.Top;
            var width = bounds.Width - this.Margin.Left - this.Margin.Right - this.border.Left.Size - this.border.Right.Size - this.padding.Left - this.padding.Right;
            var height = bounds.Height - this.Margin.Top - this.Margin.Bottom - this.border.Top.Size - this.border.Bottom.Size - this.padding.Top - this.padding.Bottom;

            var childSize = this.Content.Layout(new RectangleF(x, y, width, height), dpiScale);
                
            this.contentBounds = new RectangleF(childSize.X - this.Padding.Left,
                childSize.Y - this.Padding.Top,
                childSize.Width + this.Padding.Left + this.Padding.Right,
                childSize.Height + this.Padding.Top + this.Padding.Bottom);
                
            this.borderBounds = new RectangleF(this.contentBounds.X - this.Border.Left.Size,
                this.contentBounds.Y - this.Border.Top.Size,
                this.contentBounds.Width + this.Border.Left.Size + this.Border.Left.Size,
                this.contentBounds.Height + this.Border.Top.Size + this.Border.Bottom.Size);
        }
        
        private void ResizeToBounds(RectangleF bounds, PointF dpiScale)
        {
            var x = bounds.X + this.Margin.Left;
            var y = bounds.Y + this.Margin.Top;
            var width = bounds.Width - x - this.Margin.Right;
            var height = bounds.Height - y - this.Margin.Bottom;

            this.borderBounds = new RectangleF(x, y, width, height);
            
            x += this.Border.Left.Size;
            y += this.Border.Top.Size;
            width -= this.Border.Left.Size - this.Border.Right.Size;
            height -= this.Border.Top.Size - this.Border.Bottom.Size;

            this.contentBounds = new RectangleF(x, y, width, height);

            if (this.Content != null)
            {
                x += this.Padding.Left;
                y += this.Padding.Top;
                width -= this.Padding.Left - this.Padding.Right;
                height -= this.Padding.Top - this.Padding.Bottom;
                this.Content.Layout(new RectangleF(x, y, width, height), dpiScale);
            }
        }
        
        private void GenerateBorderPaints()
        {
            this.leftBorderPaint = this.border.Left.Color.ToSKPaint();
            this.topBorderPaint = this.border.Top.Color.ToSKPaint();
            this.rightBorderPaint = this.border.Right.Color.ToSKPaint();
            this.bottomBorderPaint = this.border.Bottom.Color.ToSKPaint();
        }
    }
}