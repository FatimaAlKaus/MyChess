using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public abstract class Figure
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Color Color { get; private set; }
        public Bitmap Texture { get; protected set; }
        public Figure(int x, int y, Color color)
        {
            Color = color;
            X = x;
            Y = y;
        }
        public virtual void Show(Graphics g, Field field)
        {
            g.DrawImage(Texture, new Rectangle(X * field.CellSize, Y * field.CellSize, field.CellSize, field.CellSize));

        }

       

        public virtual void Move(int x, int y, Field f)
        {
            Figure figure = f.GetFigure(new Point(x, y));
            if (figure != null && figure.Color != Color)
            {
                f.Remove(figure);
            }
            X = x;
            Y = y;
        }
        public abstract List<Point> GetPoints(Field f);
    }
}
