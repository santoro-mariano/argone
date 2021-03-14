namespace Argone.Widgets
{
    using System;
    using System.Drawing;
    using Argone.Core.Input;
    using Argone.Core.Widgets;
    using Argone.Widgets.Extensions;
    using Argone.Widgets.Input;
    using GLFW;
    using SkiaSharp;

    public class ButtonWidget: ContainerWidget
    {
        private readonly MouseRegionWidget mouseRegionWidget;
        private readonly IMouse mouse;
        private readonly BoxWidget boxWidget;

        public ButtonWidget(IWidgetBuilder widgetBuilder, IMouse mouse)
        {
            this.mouse = mouse;
            var border = new Border(Color.FromArgb(217, 219, 219));
            this.boxWidget = widgetBuilder.BuildBox(0, border, 10);
            this.boxWidget.BackgroundColor = Color.FromArgb(250, 251, 252);
            this.boxWidget.AdjustSizeToChild = true;
            this.mouseRegionWidget = widgetBuilder.BuildMouseRegion(this.boxWidget);
            this.mouseRegionWidget.Opaque = true;
            this.SubscribeToEvents();
        }

        public override IWidget Content
        {
            get => this.boxWidget.Content;
            set => this.boxWidget.Content = value;
        }

        public Edges<float> Margin
        {
            get => this.boxWidget.Margin;
            set => this.boxWidget.Margin = value;
        }

        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            return this.mouseRegionWidget.Layout(bounds, dpiScale);
        }

        public override void Draw(SKCanvas canvas)
        {
            this.mouseRegionWidget.Draw(canvas);
        }

        public override void Dispose()
        {
            this.UnsubscribeFromEvents();
            base.Dispose();
        }

        private void SubscribeToEvents()
        {
            this.mouseRegionWidget.Enter += this.OnMouseEnter;
            this.mouseRegionWidget.Exit += this.OnMouseExit;
            this.mouseRegionWidget.ButtonPress += this.OnMouseButtonPress;
        }
        
        private void UnsubscribeFromEvents()
        {
            this.mouseRegionWidget.Enter -= this.OnMouseEnter;
            this.mouseRegionWidget.Exit -= this.OnMouseExit;
            this.mouseRegionWidget.ButtonPress -= this.OnMouseButtonPress;
        }

        private void OnMouseEnter(object? sender, EventArgs e)
        {
            this.mouse.SetCursor(CursorType.Hand);
            this.boxWidget.BackgroundColor = Color.FromArgb(237, 240, 243);
        }
        
        private void OnMouseExit(object? sender, EventArgs e)
        {
            this.mouse.ResetCursor();
            this.boxWidget.BackgroundColor = Color.FromArgb(250, 251, 252);
        }
        
        private void OnMouseButtonPress(object? sender, MouseButtonEventArgs args)
        {
            if (args.Button == MouseButton.Left &&
                (args.Action == InputState.Press || args.Action == InputState.Repeat))
            {
                Console.WriteLine("Button pressed!");
            }
        }
    }
}