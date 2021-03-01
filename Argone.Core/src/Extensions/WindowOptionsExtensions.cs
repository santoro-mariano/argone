namespace Argone.Core.Extensions
{
    using Argone.Core.Options;
    
    public static class WindowOptionsExtensions
    {
        public static WindowOptions UseRootWidget<TWidget>(this WindowOptions options)
        {
            options.RootWidgetType = typeof(TWidget);
            return options;
        }
    }
}