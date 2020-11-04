using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Castle : Figure
    {
        public Castle(int x, int y, Color color) : base(x, y, color)
        {
            Texture = color == Color.Black ? Resource1.BlackCastle : Resource1.WhiteCastle;
        }
        public override List<Point> GetPoints(Field f)
        {
            List<Point> points = new List<Point>();
            int i = X + 1;
            while (i < 8)
            {
                Figure figure = f.GetFigure(new Point(i, Y));
                if (figure != null)
                {
                    if (figure.Color != this.Color)
                        points.Add(new Point(i, Y));
                    break;
                }
                points.Add(new Point(i, Y));
                i++;
            }
            i = X - 1;
            while (i >= 0)
            {
                Figure figure = f.GetFigure(new Point(i, Y));
                if (figure != null)
                {
                    if (figure.Color != this.Color)
                        points.Add(new Point(i, Y));
                    break;
                }
                points.Add(new Point(i, Y));
                i--;
            }
            i = Y - 1;
            while (i >= 0)
            {
                Figure figure = f.GetFigure(new Point(X, i));
                if (figure != null)
                {
                    if (figure.Color != this.Color)
                        points.Add(new Point(X, i));
                    break;
                }

                points.Add(new Point(X, i));
                i--;
            }
            i = Y + 1;
            while (i < 8)
            {
                Figure figure = f.GetFigure(new Point(X, i));
                if (figure != null)
                {
                    if (figure.Color != this.Color)
                        points.Add(new Point(X, i));
                    break;
                }
                points.Add(new Point(X, i));
                i++;
            }
            return points;
        }
    }
}
