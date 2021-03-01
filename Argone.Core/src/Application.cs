namespace Argone.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Argone.Core.Options;
    using Argone.Core.Widgets;
    using GLFW;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Application : IHostedService
    {
        private readonly ApplicationOptions options;
        private readonly IServiceProvider serviceProvider;
        private readonly List<WindowContext> windowContexts = new();

        public Application(ApplicationOptions options, IServiceProvider serviceProvider)
        {
            this.options = options;
            this.serviceProvider = serviceProvider;
            this.AddGlfwHints();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Glfw.Init();
            Glfw.SetErrorCallback(this.OnGlfwError);
            this.CreateWindows();
            while (this.windowContexts.Any(x => !x.Window.IsClosing && !x.Window.IsClosed) || cancellationToken.IsCancellationRequested)
            {
                foreach (var window in this.windowContexts.Where(x => !x.Window.IsClosing && !x.Window.IsClosed).Select(x => x.Window))
                {
                    window.Render();
                }

                Glfw.WaitEvents();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var context in this.windowContexts)
            {
                context.Dispose();
            }

            Glfw.Terminate();
            return Task.CompletedTask;
        }

        private void CreateWindows()
        {
            foreach (var windowOptions in this.options.Windows)
            {
                var context = this.serviceProvider.GetRequiredService<WindowContext>();
                context.Initialize(windowOptions);
                this.windowContexts.Add(context);
                context.Window.Closed += this.OnWindowClosed;
            }
        }

        private void OnGlfwError(ErrorCode code, IntPtr message)
        {
            Console.WriteLine($"GLFW ERROR [{code}]: {Marshal.PtrToStringUTF8(message)}");
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            var window = (Window) sender;
            window.Closed -= this.OnWindowClosed;
            var context = this.windowContexts.Single(x => x.Window == window);
            this.windowContexts.Remove(context);
        }

        private void AddGlfwHints()
        {
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.WindowHint(Hint.OpenglForwardCompatible, true);
        }
    }
}