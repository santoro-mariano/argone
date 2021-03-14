namespace Argone.Widgets.Extensions
{
    using Argone.Core.Extensions;
    using Argone.Core.Hosting;
    using Argone.Widgets.Input;
    using Argone.Widgets.Layout;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddWidgets(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.AddWidget<BoxWidget>()
                                     .AddWidget<ImageWidget>()
                                     .AddWidget<ListLayoutWidget>()
                                     .AddWidget<ButtonWidget>()
                                     .AddWidget<ScrollBarWidget>()
                                     .AddWidget<TextWidget>()
                                     .AddWidget<ViewWidget>()
                                     .AddWidget<DialogWidget>()
                                     .AddWidget<MouseRegionWidget>();
        }
    }
}