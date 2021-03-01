namespace Argone.Core.Hosting
{
    using Argone.Core.Input;
    using Argone.Core.Options;
    using Argone.Core.Widgets;
    using Microsoft.Extensions.DependencyInjection;
    
    public class ApplicationBuilder: IApplicationBuilder
    {
        public ApplicationBuilder(IServiceCollection services)
        {
            this.Services = services;
            this.Options = new ApplicationOptions();
            services.AddSingleton(this.Options);
            services.AddTransient<WindowContext>();
            services.AddScoped<Window>();
            services.AddScoped<Window.Keyboard>();
            services.AddScoped<IKeyboard>(svc => svc.GetRequiredService<Window.Keyboard>());
            services.AddScoped<Window.Mouse>();
            services.AddScoped<IMouse>(svc => svc.GetRequiredService<Window.Mouse>());
            services.AddScoped<IWidgetBuilder, WidgetBuilder>();
            services.AddHostedService<Application>();
        }

        public ApplicationOptions Options { get; }
        
        public IServiceCollection Services { get; }
    }
}