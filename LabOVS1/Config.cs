using System.Drawing;

namespace LabOVS1
{
    public static class Config
    {
        public static Point DefaultLocation = new Point(80, 80);
        private static readonly Color DefaultColor = Color.Black;
        public static readonly Font DefaultFont = new Font("Arial", 20);
        public static readonly Font EdgeFont = new Font("Arial", 15);
        public static readonly Brush DefaultBrush = new SolidBrush(DefaultColor);

        public static readonly Pen VertexPen = new Pen(DefaultColor, 6);
        public static readonly Pen EdgePen = new Pen(DefaultColor, 3);
        public static Pen TextPen = new Pen(DefaultColor, 1);

        public static readonly int DefaultVertexRadius = 35;

        public static readonly StringFormat TextFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
    }
}