namespace Argone.Core.Extensions
{
    using System.Drawing;
    using SkiaSharp;

    public static class RectangleExtensions
    {
        public static SKRect ToSkRect(this RectangleF rectangle)
        {
            return new SKRect(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }
        
        public static SKRoundRect ToSkRoundRect(this RectangleF rectangle, float radius)
        {
            return new SKRoundRect(rectangle.ToSkRect(), radius);
        }
        
        public static SKRoundRect ToSkRoundRect(this RectangleF rectangle, float xRadius, float yRadius)
        {
            return new SKRoundRect(rectangle.ToSkRect(), xRadius, yRadius);
        }
    }
}