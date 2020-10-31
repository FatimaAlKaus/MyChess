using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Bishop : Figure
    {
        public Bishop(int x, int y, Color color) : base(x, y, color)
        {
            Texture = color == Color.Black ? Resource1.BlackBishop : Resource1.WhiteBishop;
        }
        public override List<Point> GetPoints(Field f)
        {
            List<Point> points = new List<Point>();
            int i = 1;
            while (f.Inside(new Point(X + i, Y + i)))
            {
                if (f.GetFigure(new Point(X + i, Y + i)) != null)
                {
                    if (f.GetFigure(new Point(X + i, Y + i)).Color != this.Color)
                        points.Add(new Point(X + i, Y + i));
                    break;
                }
                points.Add(new Point(X + i, Y + i));
                i++;
            }
            i = 1;
            while (f.Inside(new Point(X + i, Y - i)))
            {
                if (f.GetFigure(new Point(X + i, Y - i)) != null)
                {
                    if (f.GetFigure(new Point(X + i, Y - i)).Color != this.Color)
                        points.Add(new Point(X + i, Y - i));
                    break;
                }
                points.Add(new Point(X + i, Y - i));
                i++;
            }
            i = 1;
            while (f.Inside(new Point(X - i, Y + i)))
            {
                if (f.GetFigure(new Point(X - i, Y + i)) != null)
                {
                    if (f.GetFigure(new Point(X - i, Y + i)).Color != this.Color)
                        points.Add(new Point(X - i, Y + i));
                    break;
                }
                points.Add(new Point(X - i, Y + i));
                i++;
            }
            i = 1;
            while (f.Inside(new Point(X - i, Y - i)))
            {
                if (f.GetFigure(new Point(X - i, Y - i)) != null)
                {
                    if (f.GetFigure(new Point(X - i, Y - i)).Color != this.Color)
                        points.Add(new Point(X - i, Y - i));
                    break;
                }
                points.Add(new Point(X - i, Y - i));
                i++;
            }

            return points;
          
        }
    }
}
