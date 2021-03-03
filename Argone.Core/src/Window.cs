namespace Argone.Core
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using Argone.Core.Input;
    using Argone.Core.Widgets;
    using GLFW;
    using SkiaSharp;

    internal class Window: IDisposable
    {
        private static readonly object syncRoot = new();
        private readonly NativeWindow nativeWindow;
        private IWidget rootWidget;
        private GRContext skiaContext;

        public Window()
        {
            this.nativeWindow = new NativeWindow();
            Glfw.HideWindow(this.nativeWindow);
            Glfw.SwapInterval(1);
        }

        public SKSurface Surface { get; private set; }

        public bool IsClosing => this.nativeWindow.IsClosing;

        public bool IsClosed => this.nativeWindow.IsClosed;
        
        public event EventHandler Closed;

        public void Show(Size initialSize, string title)
        {
            this.nativeWindow.Size = initialSize;
            this.nativeWindow.Title = title;
            Glfw.ShowWindow(this.nativeWindow);
            this.SubscribeToWindowEvents();
        }
        
        public void InitializeRootWidget(IWidget widget)
        {
            this.rootWidget = widget;
            this.skiaContext = this.CreateSkiaContext();
            this.Surface = this.CreateSkiaSurface(this.skiaContext, this.nativeWindow.ClientSize);
            this.rootWidget.Layout(new RectangleF(PointF.Empty, this.nativeWindow.ClientSize), this.nativeWindow.ContentScale);
            this.rootWidget.Changed += this.OnWidgetChanged;
        }

        public void Render()
        {
            if (this.rootWidget == null)
            {
                return;
            }
            
            lock (Window.syncRoot)
            {
                this.nativeWindow.MakeCurrent();
                this.rootWidget.Draw(this.Surface.Canvas);
                this.Surface.Canvas.Flush();
                this.nativeWindow.SwapBuffers();
            }
        }

        public void Dispose()
        {
            this.UnsubscribeToWindowEvents();
            this.Surface?.Dispose();
            this.skiaContext?.Dispose();
            this.nativeWindow?.Dispose();
        }
        
        public void Close()
        {
            this.nativeWindow.Close();
        }
        
        private GRContext CreateSkiaContext()
        {
            var grGlInterface = GRGlInterface.Create(Glfw.GetProcAddress);
            return GRContext.CreateGl(grGlInterface);
        }
        
        private SKSurface CreateSkiaSurface(GRContext skiaContext, Size surfaceSize)
        {
            var frameBufferInfo = new GRGlFramebufferInfo((uint)new UIntPtr(0), SKColorType.Rgba8888.ToGlSizedFormat());
            var backendRenderTarget = new GRBackendRenderTarget(surfaceSize.Width,
                surfaceSize.Height,
                0, 
                8,
                frameBufferInfo);
            return SKSurface.Create(skiaContext, backendRenderTarget, GRSurfaceOrigin.BottomLeft, SKImageInfo.PlatformColorType);
        }
        
        private void SubscribeToWindowEvents()
        {
            this.nativeWindow.SizeChanged += this.OnWindowsSizeChanged;
            this.nativeWindow.Refreshed += this.OnWindowRefreshed;
            this.nativeWindow.Closing += this.OnWindowClosing;
            this.nativeWindow.Closed += this.OnWindowClosed;
        }
        
        private void UnsubscribeToWindowEvents()
        {
            this.nativeWindow.SizeChanged -= this.OnWindowsSizeChanged;
            this.nativeWindow.Refreshed -= this.OnWindowRefreshed;
            this.nativeWindow.Closing -= this.OnWindowClosing;
            this.nativeWindow.Closed -= this.OnWindowClosed;
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            this.Closed?.Invoke(this, e);
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            this.Dispose();
        }

        private void OnWindowRefreshed(object sender, EventArgs e)
        {
            this.Render();
        }

        private void OnWindowsSizeChanged(object sender, SizeChangeEventArgs e)
        {
            this.Surface?.Dispose();
            this.Surface = this.CreateSkiaSurface(this.skiaContext, this.nativeWindow.ClientSize);
            this.rootWidget.Layout(new RectangleF(PointF.Empty, this.nativeWindow.ClientSize), this.nativeWindow.ContentScale);
        }
        
        private void OnWidgetChanged(object sender, ChangedEventArgs eventArgs)
        {
            if (eventArgs.ChangeType == ChangeType.Structure)
            {
                this.rootWidget.Layout(new RectangleF(PointF.Empty, this.nativeWindow.ClientSize), this.nativeWindow.ContentScale);
            }
            
            Glfw.PostEmptyEvent();
        }
        
        internal class Keyboard: IKeyboard
        {
            private Window window;
        
            public Keyboard(Window window)
            {
                this.window = window;
            }

            public event EventHandler<KeyEventArgs> KeyPress
            {
                add => this.window.nativeWindow.KeyPress += value;
                remove => this.window.nativeWindow.KeyPress -= value;
            }
        
            public event EventHandler<KeyEventArgs> KeyRelease
            {
                add => this.window.nativeWindow.KeyRelease += value;
                remove => this.window.nativeWindow.KeyRelease -= value;
            }
            public event EventHandler<KeyEventArgs> KeyRepeat
            {
                add => this.window.nativeWindow.KeyRepeat += value;
                remove => this.window.nativeWindow.KeyRepeat -= value;
            }
        }
        
        internal class Mouse: IMouse
        {
            private Window window;

            public Mouse(Window window)
            {
                this.window = window;
            }

            public Point Position => this.window.nativeWindow.MousePosition;
        
            public event EventHandler<MouseButtonEventArgs> ButtonPress
            {
                add => this.window.nativeWindow.MouseButton += value;
                remove => this.window.nativeWindow.MouseButton -= value;
            }
        
            public event EventHandler Enter
            {
                add => this.window.nativeWindow.MouseEnter += value;
                remove => this.window.nativeWindow.MouseEnter -= value;
            }
        
            public event EventHandler Leave
            {
                add => this.window.nativeWindow.MouseLeave += value;
                remove => this.window.nativeWindow.MouseLeave -= value;
            }

            public event EventHandler<MouseMoveEventArgs> Moved
            {
                add => this.window.nativeWindow.MouseMoved += value;
                remove => this.window.nativeWindow.MouseMoved -= value;
            }
        
            public event EventHandler<MouseMoveEventArgs> Scroll
            {
                add => this.window.nativeWindow.MouseScroll += value;
                remove => this.window.nativeWindow.MouseScroll -= value;
            }
        
            public void SetCursor(CursorType cursorType)
            {
                Glfw.SetCursor(this.window.nativeWindow, Glfw.CreateStandardCursor(CursorType.Hand));
            }

            public void ResetCursor()
            {
                Glfw.SetCursor(this.window.nativeWindow, Cursor.None);
            }
        }
    }
}