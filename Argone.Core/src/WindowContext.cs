namespace Argone.Core
{
    using System;
    using Argone.Core.Options;
    using Argone.Core.Widgets;
    using Microsoft.Extensions.DependencyInjection;

    internal class WindowContext: IDisposable
    {
        private readonly IServiceScope serviceScope;
        
        public WindowContext(IServiceProvider serviceProvider)
        {
            this.serviceScope = serviceProvider.CreateScope();
            this.Window = this.ServiceProvider.GetRequiredService<Window>();
            this.Window.Closed += this.OnWindowClosed;
        }

        public IServiceProvider ServiceProvider => this.serviceScope.ServiceProvider;
        
        public Window Window { get; }
        
        public void Initialize(WindowOptions windowOptions)
        {
            this.Window.Show(windowOptions.InitialSize, windowOptions.Title);
            var rootWidget = (IWidget) this.ServiceProvider.GetRequiredService(windowOptions.RootWidgetType);
            this.Window.InitializeRootWidget(rootWidget);
        }

        public void Dispose()
        {
            this.Window?.Dispose();
            this.serviceScope?.Dispose();
        }
        
        private void OnWindowClosed(object? sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}