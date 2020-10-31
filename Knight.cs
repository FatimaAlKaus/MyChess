using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Knight : Figure
    {
        public Knight(int x, int y, Color color) : base(x, y, color)
        {
            Texture = color == Color.Black ? Resource1.BlackKnight : Resource1.WhiteKnight;
        }
        public override List<Point> GetPoints(Field field)
        {
            List<Point> points = new List<Point>();

            points.Add(new Point(X + 1, Y + 2));
            points.Add(new Point(X - 1, Y + 2));

            points.Add(new Point(X + 2, Y + 1));
            points.Add(new Point(X - 2, Y + 1));

            points.Add(new Point(X + 1, Y - 2));
            points.Add(new Point(X - 1, Y - 2));

            points.Add(new Point(X + 2, Y - 1));
            points.Add(new Point(X - 2, Y - 1));

            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (field.Inside(points[i]))
                    {
                        Figure f = field.GetFigure(points[i]);
                        if (f != null)
                        {
                            if (f.Color == this.Color)
                            {
                                points.Remove(points[i]);
                                i--;
                            }
                        }
                    }
                    else
                    {
                        points.Remove(points[i]);
                        i--;
                    }
                }
            }
            return points;
        }

    }
}
