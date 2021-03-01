namespace Argone.Core.Widgets
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class WidgetBuilder: IWidgetBuilder
    {
        private readonly IServiceProvider serviceProvider;

        public WidgetBuilder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        
        public TWidget Build<TWidget>() where TWidget : IWidget
        {
            return this.serviceProvider.GetRequiredService<TWidget>();
        }
    }
}