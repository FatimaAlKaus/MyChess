using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Pawn : Figure
    {
        bool firstTurn = true;
        public event EventHandler PawnNotify;
        public Pawn(int x, int y, Color color, EventHandler eventHandler) : base(x, y, color)
        {
            PawnNotify = eventHandler;
            Texture = color == Color.Black ? Resource1.BlackPawn : Resource1.WhitePawn;
        }
        public override List<Point> GetPoints(Field f)
        {
            List<Point> points = new List<Point>();
            int k = Color == Color.Black ? 1 : -1;



            if (firstTurn && f.GetFigure(new Point(X, Y + k * 2)) == null && f.GetFigure(new Point(X, Y + k * 1)) == null)
            {
                points.Add(new Point(X, Y + k * 2));
            }


            if (f.Inside(new Point(X, Y + k * 1)) && f.GetFigure(new Point(X, Y + k * 1)) == null) points.Add(new Point(X, Y + k * 1));

            Point point1 = new Point(X - 1, Y + k * 1);
            if (f.GetFigure(point1) != null)
            {
                if (f.GetFigure(point1).Color != Color)
                {
                    points.Add(point1);
                }
            }
            Point point2 = new Point(X + 1, Y + k * 1);
            if (f.GetFigure(point2) != null)
            {
                if (f.GetFigure(point2).Color != Color)
                {
                    points.Add(point2);
                }
            }

            return points;
        }
        public override void Move(int x, int y, Field f)
        {
            base.Move(x, y, f);
            firstTurn = false;

            if (Color == Color.Black && Y == 7 || Color == Color.White && Y == 0)
            {
                PawnNotify?.Invoke(this, null);
            }
        }
    }
}
