namespace Argone.Widgets
{
    using System;
    using System.Drawing;
    using Argone.Core.Input;
    using Argone.Core.Widgets;
    using Argone.Widgets.Extensions;
    using GLFW;
    using SkiaSharp;

    public class ButtonWidget: WidgetWithContent
    {
        private readonly IMouse mouse;
        private readonly BoxWidget boxWidget;
        private RectangleF dimensions;

        public ButtonWidget(IWidgetBuilder widgetBuilder, IMouse mouse)
        {
            this.mouse = mouse;
            var border = new Border(Color.FromArgb(217, 219, 219));
            this.boxWidget = widgetBuilder.BuildBox(0, border, 10);
            this.boxWidget.BackgroundColor = Color.FromArgb(250, 251, 252);
            this.boxWidget.AdjustSizeToChild = true;
            this.mouse.Enter += this.OnMouseEvent;
            this.mouse.Leave += this.OnMouseEvent;
            this.mouse.Moved += this.OnMouseEvent;
            this.mouse.ButtonPress += this.OnMouseButtonPress;
        }

        public override IWidget Content
        {
            get => this.boxWidget.Content;
            set => this.boxWidget.Content = value;
        }

        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            this.dimensions = this.boxWidget.Layout(bounds, dpiScale);
            return this.dimensions;
        }

        public override void Draw(SKCanvas canvas)
        {
            this.boxWidget.Draw(canvas);
        }
        
        private void OnMouseEvent(object sender, EventArgs e)
        {
            if (this.dimensions.Contains(this.mouse.Position))
            {
                this.mouse.SetCursor(CursorType.Hand);
                this.boxWidget.BackgroundColor = Color.FromArgb(237, 240, 243);
            }
            else
            {
                this.mouse.ResetCursor();
                this.boxWidget.BackgroundColor = Color.FromArgb(250, 251, 252);
            }
        }
        
        private void OnMouseButtonPress(object sender, InputEventArgs<MouseButtonEventArgs> e)
        {
            if (e.InnerEventArgs.Button == MouseButton.Left &&
                (e.InnerEventArgs.Action == InputState.Press || e.InnerEventArgs.Action == InputState.Repeat) &&
                this.dimensions.Contains(this.mouse.Position))
            {
                Console.WriteLine("Button pressed!");
            }
        }
    }
}