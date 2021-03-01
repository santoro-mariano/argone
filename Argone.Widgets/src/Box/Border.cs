namespace Argone.Widgets
{
    using System.Drawing;

    public struct Border
    {
        public Border(string hexColor, float size = 1):
            this(ColorTranslator.FromHtml(hexColor), size)
        {}
        
        public Border(float size = 1):
            this(Color.Black, size)
        {}
        
        public Border(Color color, float size = 1)
        {
            this.Color = color;
            this.Size = size;
        }
        
        public float Size { get; set; }
        
        public Color Color { get; set; }
    }
}