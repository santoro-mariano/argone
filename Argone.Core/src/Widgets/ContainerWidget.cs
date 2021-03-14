namespace Argone.Core.Widgets
{
    using System;

    public abstract class ContainerWidget: Widget, IDisposable
    {
        private IWidget content;

        public virtual IWidget Content
        {
            get => this.content;
            set => this.SetContent(value);
        }

        private void SetContent(IWidget contentWidget)
        {
            if (contentWidget.Parent != null)
            {
                var exceptionMessage =
                    $"The widget {contentWidget.GetType().Name} can't be added to {this.GetType().Name} because it's a child of {contentWidget.Parent.GetType().Name}.";
                throw new InvalidOperationException(exceptionMessage);
            }
            
            if (this.content != null)
            {
                this.content.Changed -= this.OnContentChanged;
                this.content.Parent = null;
            }
            
            this.content = contentWidget;
            this.content.Parent = this;
            this.content.Changed += this.OnContentChanged;
        }

        private void OnContentChanged(object sender, ChangedEventArgs e)
        {
            this.NotifyChange(sender, e);
        }

        public virtual void Dispose()
        {
            if (this.content != null)
            {
                this.content.Changed -= this.OnContentChanged;
            }
        }
    }
}