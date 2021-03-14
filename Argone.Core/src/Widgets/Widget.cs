namespace Argone.Core.Widgets
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using SkiaSharp;

    public abstract class Widget: IWidget
    {
        private bool isVisible;
        
        public IWidget Parent { get; set; }

        public bool IsVisible
        {
            get => this.isVisible;
            set
            {
                this.isVisible = value;
                this.NotifyStructureChange();
            }
        }
        
        public event EventHandler<ChangedEventArgs> Changed;
        
        public abstract RectangleF Layout(RectangleF bounds, PointF dpiScale);

        public abstract void Draw(SKCanvas canvas);

        protected void NotifyStructureChange()
        {
            this.NotifyChange(this, new ChangedEventArgs(ChangeType.Structure));
        }
        
        protected void NotifyGraphicChange()
        {
            this.NotifyChange(this, new ChangedEventArgs(ChangeType.Graphic));
        }

        protected void NotifyChange(object sender, ChangedEventArgs eventArgs)
        {
            this.Changed?.Invoke(sender, eventArgs);
        }

        public virtual IEnumerator<IWidget> GetEnumerator()
        {
            if (!this.IsVisible)
            {
                yield return this;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}