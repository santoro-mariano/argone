namespace Argone.Test
{
    using System.Drawing;
    using System.Threading.Tasks;
    using Argone.Core.Extensions;
    using Argone.Widgets.Extensions;
    using Microsoft.Extensions.Hosting;

    class Program
    {
        private static Task Main(string[] args)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureArgone(builder =>
                {
                    builder.AddWidgets();
                    builder.AddWidget<TestView>();
                    
                    builder.AddWindow(new Size(800, 600), "Argone Test").UseRootWidget<TestView>();
                })
                .RunConsoleAsync();
        }
    }
}