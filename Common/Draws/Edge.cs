using System;
using System.Drawing;

namespace Common.Draws
{
    public class Edge
    {
        public Pen CurrentPen;
        public readonly Vertex From;

        public bool IsDirected;

        private int _offset;
        private Random _rnd;

        private Brush _textBrush;
        public readonly Vertex To;
        public int Value;
        public Color Color;

        public Edge(Vertex from, Vertex to, int value, Pen pen)
        {
            Init();
            From = from;
            To = to;
            Value = value;
            CurrentPen = pen;
        }

        public Edge(Vertex from, Vertex to, int value, bool isDirected)
        {
            Init();
            From = from;
            To = to;
            Value = value;
            IsDirected = isDirected;
        }

        private void Init()
        {
            _textBrush = Config.DefaultBrush;
            IsDirected = true;
            _rnd = new Random();
            _offset = _rnd.Next(20, 150);
        }

        public void ChangeColorForHighlighting(Color color)
        {
            //CurrentPen.Dispose();
            Color = color;
            CurrentPen = new Pen(color, Config.HighlightingEdgePenSize);
            _textBrush = new SolidBrush(color);
            
        }
        
        public void ChangeColor(Pen pen)
        {
            //CurrentPen.Dispose();
            CurrentPen = pen;
            _textBrush = new SolidBrush(pen.Color);
        }

        public void ResetColor()
        {
            //CurrentPen.Dispose();
            Color = Tools.GetRandomColor(0);
            CurrentPen.Width = Config.DefaultEdgePenSize;
            CurrentPen = new Pen(Color, Config.DefaultEdgePenSize);
            _textBrush = Config.DefaultBrush;
        }

        private void DrawArrow(Graphics graphics, Point from, Point to)
        {
            // dx,dy = arrow line vector
            var dx = to.X - from.X;
            var dy = to.Y - from.Y;

            // increase this to get a larger arrow head
            const int arrowHeadBoxSize = 8;
            
            // normalizeищ
            var length = Math.Sqrt(dx * dx + dy * dy);
            var unitDx = dx / length;
            var unitDy = dy / length;

            

            try
            {
                var arrowPoint1 = new Point(
                    Convert.ToInt32(to.X - unitDx * arrowHeadBoxSize - unitDy * arrowHeadBoxSize),
                    Convert.ToInt32(to.Y - unitDy * arrowHeadBoxSize + unitDx * arrowHeadBoxSize));

                var arrowPoint2 = new Point(
                    Convert.ToInt32(to.X - unitDx * arrowHeadBoxSize + unitDy * arrowHeadBoxSize),
                    Convert.ToInt32(to.Y - unitDy * arrowHeadBoxSize - unitDx * arrowHeadBoxSize));

                var oldWidth = CurrentPen.Width;
                CurrentPen.Width = 2;
                graphics.DrawLine(CurrentPen, to, arrowPoint1);
                graphics.DrawLine(CurrentPen, to, arrowPoint2);
                CurrentPen.Width = oldWidth;
            }
            catch { }
        }

        private Rectangle GetRectForText()
        {
            var x = Math.Min(From.Center.X, To.Center.X);
            var y = Math.Min(From.Center.Y, To.Center.Y);
            var location = new Point(x, y);
            var sizeX = Math.Max(Math.Max(From.Center.X, To.Center.X) - x + _offset, _offset);
            var sizeY = Math.Max(Math.Max(From.Center.Y, To.Center.Y) - y + _offset, _offset);
            var size = new Size(sizeX, sizeY);

            return new Rectangle(location, size);
        }

        public void DrawAnimatedPackage(Graphics graphics, int i)
        {
            var from = new Point(From.Center.X, From.Center.Y);
            var to = new Point(To.Center.X, To.Center.Y);
            var lengthX = to.X - from.X;
            var lengthY = to.Y - from.Y;
            //graphics.DrawString("aaaa", Config.DefaultFont, TextBrush, from.X + lengthX/4, from.Y + lengthY/4);

            graphics.DrawRectangle(CurrentPen, from.X + lengthX / 40 * i, from.Y + lengthY / 40 * i, 25, 25);
            graphics.FillRectangle(_textBrush, from.X + lengthX / 40 * i, from.Y + lengthY / 40 * i, 25, 25);
        }

        private int Sign(int n)
        {
            return n >= 0 ? 1 : -1;
        }
        public void Draw(Graphics graphics)
        {
            var k = 0.90;
            var from = new Point(From.Center.X, From.Center.Y);
            var to = new Point(To.Center.X, To.Center.Y);
            var mid = new Point((from.X + to.X) / 2, (from.Y + to.Y) / 2);
            var mid1 = new Point((to.X + from.X) / 4, (to.Y + from.Y) / 4);
            var mid2 = new Point((int) ((from.X + to.X) / 1.5f), (int) ((from.Y + to.Y) / 1.5f));
            var lengthX = to.X - from.X;
            var lengthY = to.Y - from.Y;

            //Point[] points = { from, new Point(mid1.X + 20, mid1.Y + 20), new Point(mid2.X + 20, mid2.Y + 20), to };
            Point[] points = {from, new Point(mid.X + _offset, mid.Y + _offset), new Point(mid.X + _offset, mid.Y + _offset), to};
            ChangeColor(CurrentPen);
            graphics.DrawLine(CurrentPen, from, to);

            //graphics.DrawBezier(CurrentPen, points[0], points[1], points[2], points[3]);
            if (IsDirected)
            {
                DrawArrow(graphics, from, to);
            }
            //graphics.DrawString(Value.ToString(), Config.DefaultFont, TextBrush, GetRectForText(), Config.TextFromat);

            graphics.DrawString(Value.ToString(), Config.EdgeFont, _textBrush, to.X - lengthX / 4, to.Y - lengthY / 4);
        }
    }
}