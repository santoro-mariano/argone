namespace Argone.Widgets
{
    using System.Drawing;
    using Argone.Core.Widgets;
    using Argone.Widgets.Extensions;
    using SkiaSharp;

    public class ModalWidget: ContainerWidget
    {
        public BoxWidget BoxWidget;

        public ModalWidget(IWidgetBuilder widgetBuilder)
        {
            this.BoxWidget = widgetBuilder.BuildBox(0, new Border(Color.Black, 1), 10);
        }
        
        public override RectangleF Layout(RectangleF bounds, PointF dpiScale)
        {
            return this.BoxWidget.Layout(bounds, dpiScale);
        }

        public override void Draw(SKCanvas canvas)
        {
            throw new System.NotImplementedException();
        }
    }
}