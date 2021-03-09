namespace Argone.Core.Widgets
{
    using System;

    public abstract class WidgetWithContent: Widget, IDisposable
    {
        private IWidget content;
        
        public virtual IWidget Content
        {
            get => this.content;
            set => this.SetContent(value);
        }

        private void SetContent(IWidget widget)
        {
            if (this.content != null)
            {
                this.content.Changed -= this.OnContentChanged;
            }

            this.content = widget;
            this.content.Changed += this.OnContentChanged;
            this.NotifyStructureChange();
        }

        public virtual void Dispose()
        {
            if (this.Content != null)
            {
                this.Content.Changed -= this.OnContentChanged;
            }
        }
        
        private void OnContentChanged(object sender, ChangedEventArgs eventArgs)
        {
            this.NotifyChange(sender, eventArgs);
        }
    }
}