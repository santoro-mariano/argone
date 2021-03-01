namespace Argone.Core.Widgets
{
    public interface IWidgetBuilder
    {
        TWidget Build<TWidget>() where TWidget : IWidget;
    }
}