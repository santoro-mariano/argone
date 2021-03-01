namespace Argone.Widgets.Extensions
{
    using Argone.Core.Extensions;
    using Argone.Core.Hosting;
    
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddWidgets(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.AddWidget<BoxWidget>()
                                     .AddWidget<ImageWidget>()
                                     .AddWidget<ListWidget>()
                                     .AddWidget<ButtonWidget>()
                                     .AddWidget<ScrollBarWidget>()
                                     .AddWidget<TextWidget>()
                                     .AddWidget<ViewWidget>()
                                     .AddWidget<DialogWidget>();
        }
    }
}