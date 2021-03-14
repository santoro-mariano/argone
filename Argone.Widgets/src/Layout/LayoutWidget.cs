namespace Argone.Widgets.Layout
{
    using System;
    using System.Collections.Generic;
    using Argone.Core.Widgets;

    public abstract class LayoutWidget: Widget, IDisposable
    {
        protected readonly List<IWidget> children = new();

        public void AddChild(IWidget widget)
        {
            if (widget.Parent != null)
            {
                var exceptionMessage =
                    $"The widget {widget.GetType().Name} can't be added to {this.GetType().Name} because it's a child of {widget.Parent.GetType().Name}.";
                throw new InvalidOperationException(exceptionMessage);
            }
            this.children.Add(widget);
            widget.Parent = this;
            widget.Changed += this.OnChildChanged;
            this.NotifyStructureChange();
        }

        public void RemoveChild(IWidget widget)
        {
            widget.Changed -= this.OnChildChanged;
            widget.Parent = null;
            this.NotifyStructureChange();
        }

        public virtual void Dispose()
        {
            foreach (var child in this.children)
            {
                child.Changed -= this.OnChildChanged;
            }
        }

        public override IEnumerator<IWidget> GetEnumerator()
        {
            if (!this.IsVisible)
            {
                yield break;
            }

            yield return this;
            if (this.children == null)
            {
                yield break;
            }

            foreach (var child in this.children)
            {
                yield return child;
            }
        }

        private void OnChildChanged(object sender, ChangedEventArgs eventArgs)
        {
            this.NotifyChange(sender, eventArgs);
        }
    }
}