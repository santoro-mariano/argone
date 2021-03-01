namespace Argone.Core.Extensions
{
    using System.Drawing;
    using SkiaSharp;

    public static class ColorExtensions
    {
        public static SKColor ToSKColor(this Color color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }

        public static SKPaint ToSKPaint(this Color color)
        {
            return new SKPaint {Color = color.ToSKColor()};
        }
    }
}