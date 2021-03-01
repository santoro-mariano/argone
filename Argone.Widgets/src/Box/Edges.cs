namespace Argone.Widgets
{
    public struct Edges<T>
    {
        public Edges(T value)
        {
            this.Left = this.Right = this.Top = this.Bottom = value;
        }

        public Edges(T leftRight, T topBottom)
        {
            this.Left = this.Right = leftRight;
            this.Top = this.Bottom = topBottom;
        }
        
        public T Left { get; set; }
        
        public T Right { get; set; }
        
        public T Top { get; set; }
        
        public T Bottom { get; set; }
    }
}