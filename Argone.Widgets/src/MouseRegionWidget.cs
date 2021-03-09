namespace Argone.Widgets
{
    using System;
    using System.Drawing;
    using Argone.Core.Input;
    using Argone.Core.Widgets;
    using GLFW;
    using SkiaSharp;

    public class MouseRegionWidget: WidgetWithContent
    {
        private readonly IMouse mouse;
        private RectangleF dimensions;
        private bool isMouseHover;

        public MouseRegionWidget(IMouse mouse)
        {
            this.mouse = mouse;
            this.SubscribeToEvents();
        }

        public event EventHandler Enter;

        public event EventHandler Hover;

        public event EventHandler Exit;

        public event EventHandler<MouseButtonEventArgs> ButtonPress;

        public event EventHandler<MouseMoveEventArgs> Scroll;

        public bool Opaque { get; set; } = true;
        
        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            this.dimensions = this.Content.Layout(bounds, dpiScale);
            return this.dimensions;
        }

        public override void Draw(SKCanvas canvas)
        {
            this.Content.Draw(canvas);
        }

        public override void Dispose()
        {
            this.UnsubscribeFromEvents();
            base.Dispose();
        }
        
        private void SubscribeToEvents()
        {
            this.mouse.Enter += this.OnMouseEvent;
            this.mouse.Moved += this.OnMouseEvent;
            this.mouse.Leave += this.OnMouseEvent;
            this.mouse.ButtonPress += this.OnMouseButtonPress;
            this.mouse.Scroll += this.OnMouseScroll;
        }

        private void UnsubscribeFromEvents()
        {
            this.mouse.Enter -= this.OnMouseEvent;
            this.mouse.Moved -= this.OnMouseEvent;
            this.mouse.Leave -= this.OnMouseEvent;
            this.mouse.ButtonPress -= this.OnMouseButtonPress;
            this.mouse.Scroll -= this.OnMouseScroll;
        }
        
        private void OnMouseEvent(object? sender, InputEventArgs args)
        {
            if (this.dimensions.Contains(this.mouse.Position))
            {
                if (!this.isMouseHover)
                {
                    this.isMouseHover = true;
                    this.Enter?.Invoke(this, args);
                }
                else
                {
                    this.Hover?.Invoke(this, args);
                }

                args.Handled = this.Opaque;
            }
            else if(this.isMouseHover)
            {
                this.isMouseHover = false;
                this.Exit?.Invoke(this, args);
            }
        }
        
        private void OnMouseButtonPress(object? sender, InputEventArgs<MouseButtonEventArgs> args)
        {
            if (this.dimensions.Contains(this.mouse.Position))
            {
                this.ButtonPress?.Invoke(this, args.InnerEventArgs);
                args.Handled = this.Opaque;
            }
        }
        
        private void OnMouseScroll(object? sender, InputEventArgs<MouseMoveEventArgs> args)
        {
            if (this.dimensions.Contains(this.mouse.Position))
            {
                this.Scroll?.Invoke(this, args.InnerEventArgs);
                args.Handled = this.Opaque;
            }
        }
    }
}