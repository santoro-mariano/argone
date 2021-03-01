namespace Argone.Widgets
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using Argone.Core.Widgets;
    using SkiaSharp;

    public class ListWidget: Widget, IDisposable
    {
        private readonly List<IWidget> children = new();
        private ListOrientation orientation;

        public ListOrientation Orientation
        {
            get => this.orientation;
            set
            {
                this.orientation = value;
                this.NotifyStructureChange();
            }
        }

        public void AddChild(IWidget widget)
        {
            this.children.Add(widget);
            widget.Changed += this.OnChildChange;
            this.NotifyStructureChange();
        }

        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            foreach (var child in this.children)
            {
                var childSize = child.Layout(bounds, dpiScale);
                if (this.Orientation == ListOrientation.Horizontal)
                {
                    bounds.X += childSize.Width;
                    bounds.Width = Math.Max(bounds.Width, childSize.X + childSize.Width);
                    bounds.Height = Math.Max(bounds.Height, childSize.Height);
                }
                else
                {
                    bounds.Y += childSize.Height;
                    bounds.Width = Math.Max(bounds.Width, childSize.Width);
                    bounds.Height = Math.Max(bounds.Height, childSize.Y + childSize.Height);
                }
            }

            return bounds;
        }

        public override void Draw(SKCanvas canvas)
        {
            foreach (var child in this.children)
            {
                child.Draw(canvas);
            }
        }

        public void Dispose()
        {
            foreach (var child in this.children)
            {
                child.Changed -= this.OnChildChange;
            }
        }
        
        private void OnChildChange(object sender, ChangedEventArgs eventArgs)
        {
            this.NotifyChange(sender, eventArgs);
        }
    }
}