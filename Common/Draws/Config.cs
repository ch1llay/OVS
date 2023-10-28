using System.Drawing;

namespace Common.Draws
{
    public static class Config
    {
        public static Point DefaultLocation = new Point(80, 80);
        private static readonly Color VertexCircleColor = Color.Black;
        private static readonly Color VertexTextColor = Color.White;
        public static readonly Font VertexFont = new Font("Arial", 35);
        public static readonly Font EdgeFont = new Font("Arial", 20);
        public static readonly Brush DefaultBrush = new SolidBrush(VertexCircleColor);
        public static readonly Brush VertexTextBrush = new SolidBrush(VertexTextColor);

        public static readonly Pen VertexPen = new Pen(VertexCircleColor, 8);
        public static readonly int DefaultEdgePenSize = 4;
        public static readonly int HighlightingEdgePenSize = 8;
        public static Pen TextPen = new Pen(VertexCircleColor, 1);

        public static readonly int DefaultVertexRadius = 40;

        public static readonly StringFormat TextFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
    }
}