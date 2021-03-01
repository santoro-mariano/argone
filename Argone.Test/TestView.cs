namespace Argone.Test
{
    using System.Drawing;
    using Argone.Core.Widgets;
    using Argone.Widgets;
    using Argone.Widgets.Extensions;
    using SkiaSharp;

    public class TestView: WidgetWithContent
    {
        private readonly ViewWidget view;
        
        public TestView(IWidgetBuilder widgetBuilder)
        {
            var button = widgetBuilder.BuildButton(widgetBuilder.BuildText("Click me!"));
            // var dialog = widgetBuilder.BuildDialog(button);
            this.view = widgetBuilder.BuildView(button);
            //ColorTranslator.FromHtml("#FFFFFF");
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