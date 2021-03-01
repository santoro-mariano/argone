namespace Argone.Core.Extensions
{
    using System.Drawing;
    using SkiaSharp;
    
    public static class SKRectExtensions
    {
        public static RectangleF ToRectangle(this SKRect rect)
        {
            return new RectangleF(rect.Location.X, rect.Location.Y, rect.Size.Width, rect.Size.Height);
        }
    }
}