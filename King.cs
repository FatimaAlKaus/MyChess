using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class King : Figure
    {
        public event EventHandler Deth;
        public void deth()
        {
            Deth?.Invoke(this, null);
        }
        public King(int x, int y, Color color, EventHandler eventHandler) : base(x, y, color)
        {
            Deth = eventHandler;
            Texture = color == Color.Black ? Resource1.BlackKing : Resource1.WhiteKing;
        }
        public override List<Point> GetPoints(Field f)
        {
            List<Point> points = new List<Point>();
            Point point;
            point = new Point(X + 1, Y);
            if (f.Inside(point))
            {
                if (f.GetFigure(point) == null || f.GetFigure(point) != null && f.GetFigure(point).Color != this.Color)
                    points.Add(point);
            }
            point = new Point(X - 1, Y);
            if (f.Inside(point))
            {
                if (f.GetFigure(point) == null || f.GetFigure(point) != null && f.GetFigure(point).Color != this.Color)
                    points.Add(point);
            }
            point = new Point(X, Y + 1);
            if (f.Inside(point))
            {
                if (f.GetFigure(point) == null || f.GetFigure(point) != null && f.GetFigure(point).Color != this.Color)
                    points.Add(point);
            }
            point = new Point(X, Y - 1);
            if (f.Inside(point))
            {
                if (f.GetFigure(point) == null || f.GetFigure(point) != null && f.GetFigure(point).Color != this.Color)
                    points.Add(point);
            }
            point = new Point(X - 1, Y - 1);
            if (f.Inside(point))
            {
                if (f.GetFigure(point) == null || f.GetFigure(point) != null && f.GetFigure(point).Color != this.Color)
                    points.Add(point);
            }
            point = new Point(X - 1, Y + 1);
            if (f.Inside(point))
            {
                if (f.GetFigure(point) == null || f.GetFigure(point) != null && f.GetFigure(point).Color != this.Color)
                    points.Add(point);
            }
            point = new Point(X + 1, Y - 1);
            if (f.Inside(point))
            {
                if (f.GetFigure(point) == null || f.GetFigure(point) != null && f.GetFigure(point).Color != this.Color)
                    points.Add(point);
            }
            point = new Point(X + 1, Y + 1);
            if (f.Inside(point))
            {
                if (f.GetFigure(point) == null || f.GetFigure(point) != null && f.GetFigure(point).Color != this.Color)
                    points.Add(point);
            }
            return points;
        }
        
    }
}
