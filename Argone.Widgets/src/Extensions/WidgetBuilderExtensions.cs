namespace Argone.Widgets.Extensions
{
    using System.Drawing;
    using Argone.Core.Widgets;
    using Argone.Widgets.Input;

    public static class WidgetBuilderExtensions
    {
        public static TWidget BuildContainer<TWidget>(this IWidgetBuilder builder, IWidget content) where TWidget: ContainerWidget
        {
            var widget = builder.Build<TWidget>();
            widget.Content = content;
            return widget;
        }
        
        public static TextWidget BuildText(this IWidgetBuilder builder, string text)
        {
            var widget = builder.Build<TextWidget>();
            widget.Text = text;
            widget.FontColor = Color.FromArgb(36, 41, 46);
            return widget;
        }
        
        public static TextWidget BuildText(this IWidgetBuilder builder, string text, Color color)
        {
            var widget = builder.Build<TextWidget>();
            widget.Text = text;
            widget.FontColor = color;
            return widget;
        }

        public static ButtonWidget BuildButton(this IWidgetBuilder builder, IWidget content)
        {
            return builder.BuildContainer<ButtonWidget>(content);
        }

        public static ViewWidget BuildView(this IWidgetBuilder builder, IWidget content)
        {
            return builder.BuildContainer<ViewWidget>(content);
        }

        public static BoxWidget BuildBox(this IWidgetBuilder builder, float margin, Border border, float padding)
        {
            var widget = builder.Build<BoxWidget>();
            widget.Margin = new Edges<float>(margin);
            widget.Border = new Edges<Border>(border);
            widget.Padding = new Edges<float>(padding);
            return widget;
        }

        public static DialogWidget BuildDialog(this IWidgetBuilder builder, IWidget content)
        {
            return builder.BuildContainer<DialogWidget>(content);
        }
        
        public static MouseRegionWidget BuildMouseRegion(this IWidgetBuilder builder, IWidget content)
        {
            return builder.BuildContainer<MouseRegionWidget>(content);
        }
    }
}