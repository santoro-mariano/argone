namespace Argone.Test
{
    using System.Drawing;
    using Argone.Core.Widgets;
    using Argone.Widgets;
    using Argone.Widgets.Extensions;
    using SkiaSharp;

    public class TestView: ContainerWidget
    {
        private readonly ViewWidget view;
        
        public TestView(IWidgetBuilder widgetBuilder)
        {
            var button = widgetBuilder.BuildButton(widgetBuilder.BuildText("Click me!"));
            button.Margin = new Edges<float>(20);
            // var dialog = widgetBuilder.BuildDialog(button);
            this.view = widgetBuilder.BuildView(button);
            this.view.BackgroundColor = ColorTranslator.FromHtml("#383838");
        }
        
        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            return this.view.Layout(bounds, dpiScale);
        }

        public override void Draw(SKCanvas canvas)
        {
            this.view.Draw(canvas);
        }
    }
}