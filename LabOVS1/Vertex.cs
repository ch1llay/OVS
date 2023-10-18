using System.Drawing;

namespace LabOVS1
{
    public class Vertex
    {
        private Point _center;
        public Point Location;

        private Pen _currentPen;
        public int Index;
        public int Radius;

        public Vertex(int value)
        {
            Init();
            Index = value;
        }

        public Vertex(Point center)
        {
            Init();
            Center = center;
        }

        public Vertex(Point center, int value)
        {
            Init();
            Center = center;
            Index = value;
        }

        public Point Center
        {
            get => _center;
            set
            {
                _center.X = value.X;
                _center.Y = value.Y;
                Location.X = value.X - Radius;
                Location.Y = value.Y - Radius;
            }
        }

        private void Init()
        {
            _center = new Point(0, 0);
            Location = new Point(0, 0);
            Radius = Config.DefaultVertexRadius;
            _currentPen = Config.VertexPen;
            Center = Config.DefaultLocation;
            Index = 0;
        }

        public void ChangeColor(Color color)
        {
            //CurrentPen.Dispose();
            _currentPen = new Pen(color, 10);
        }
        

        public void ResetColor()
        {
            //CurrentPen.Dispose();
            _currentPen = Config.VertexPen;
        }

        public void Draw(Graphics graphics)
        {
            var rect = new Rectangle(Location, new Size(Radius * 2, Radius * 2));
            graphics.DrawEllipse(_currentPen, rect);
            graphics.DrawString(Index.ToString(), Config.DefaultFont, Config.DefaultBrush, rect, Config.TextFormat);
        }

        public override string ToString()
        {
            return Index.ToString();
        }
    }
}