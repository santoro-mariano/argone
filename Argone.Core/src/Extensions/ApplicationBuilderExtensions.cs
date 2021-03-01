namespace Argone.Core.Extensions
{
    using System.Collections.Generic;
    using System.Drawing;
    using Argone.Core.Hosting;
    using Argone.Core.Options;
    using Argone.Core.Widgets;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ApplicationBuilderExtensions
    {
        public static WindowOptions AddWindow(this IApplicationBuilder applicationBuilder, Size initialSize, string title)
        {
            var windowList = new List<WindowOptions>(applicationBuilder.Options.Windows);
            var newWindow = new WindowOptions {InitialSize = initialSize, Title = title};
            windowList.Add(newWindow);
            applicationBuilder.Options.Windows = windowList;
            return newWindow;
        }
        
        public static IApplicationBuilder AddWidget<TWidget>(this IApplicationBuilder applicationBuilder)
            where TWidget : class, IWidget
        {
            applicationBuilder.Services.TryAddScoped<TWidget>();
            return applicationBuilder;
        }
    }
}