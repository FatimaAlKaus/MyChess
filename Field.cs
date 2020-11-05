using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ChessLib;
namespace Library
{
    public class Field
    {
        Figure selectedFigure;
        public event Action Refresh;
        public ChessEventHandler ChessEventHandler { get; set; }
        public int CellSize { get; private set; } = 50;
        private List<Figure> figures = new List<Figure>();
        int _x;
        int _y;
        public int X { get => _x; set { _x = value / CellSize * CellSize; } }
        public int Y { get => _y; set { _y = value / CellSize * CellSize; } }
        public bool Inside(Point point)
        {
            return !(point.X < 0 || point.Y < 0 || point.X > 7 || point.Y > 7);
        }
        private void VictoryNotify(object sender, EventArgs e)
        {
            King king = sender as King;
            string message = king.Color == Color.Black ? "белый" : "черный";
            MessageBox.Show($"Победил {message} игрок!");
            Reset();
        }
        public void Move(object sender, ChessEventArgs e)
        {
            GetFigure(new Point(e.OldX, e.OldY))?.Move(e.NewX, e.NewY, this);
            Refresh?.Invoke();
        }
        public Field()
        {
            Reset();
        }
        public void GlobalSubscribe()
        {
            foreach (var figure in figures)
            {
                figure.TurnNotify += ChessEventHandler;
            }
        }
        public void Reset()
        {
            figures.Clear();
            figures.Add(new King(3, 0, Color.Black, VictoryNotify));
            figures.Add(new Queen(4, 0, Color.Black));
            figures.Add(new Bishop(2, 0, Color.Black));
            figures.Add(new Bishop(5, 0, Color.Black));
            figures.Add(new Castle(0, 0, Color.Black));
            figures.Add(new Castle(7, 0, Color.Black));
            figures.Add(new Knight(1, 0, Color.Black));
            figures.Add(new Knight(6, 0, Color.Black));
            for (int i = 0; i < 8; i++)
                figures.Add(new Pawn(i, 1, Color.Black, RebornPawn));

            figures.Add(new King(3, 7, Color.White, VictoryNotify));
            figures.Add(new Queen(4, 7, Color.White));
            figures.Add(new Bishop(2, 7, Color.White));
            figures.Add(new Bishop(5, 7, Color.White));
            figures.Add(new Castle(0, 7, Color.White));
            figures.Add(new Castle(7, 7, Color.White));
            figures.Add(new Knight(1, 7, Color.White));
            figures.Add(new Knight(6, 7, Color.White));
            for (int i = 0; i < 8; i++)
                figures.Add(new Pawn(i, 6, Color.White, RebornPawn));

            GlobalSubscribe();
        }
        public Figure GetFigure(Point point)
        {
            foreach (var figure in figures)

                if (figure.X == point.X && figure.Y == point.Y)
                    return figure;
            return null;

        }
        private Figure GetFigure()
        {
            var x = X / CellSize;
            var y = Y / CellSize;
            foreach (var figure in figures)

                if (figure.X == x && figure.Y == y)
                    return figure;
            return null;
        }
        public void Remove(Figure f)
        {

            figures.Remove(f);
            if (f is King)
                (f as King).deth();

        }
        public void Put()
        {
            if (selectedFigure != null)
            {
                Point point = new Point(X / CellSize, Y / CellSize);

                if (selectedFigure.GetPoints(this).Contains(point))
                {
                    selectedFigure.Move(point.X, point.Y, this);
                }
            }
        }
        public void Select()
        {
            selectedFigure = selectedFigure == null ? GetFigure() : null;
        }
        private void RebornPawn(object sender, EventArgs e)
        {
            Pawn pawn = sender as Pawn;
            Queen queen = new Queen(pawn.X, pawn.Y, pawn.Color);
            figures.Remove(pawn);
            figures.Add(queen);

        }
        public void Show(Graphics g)
        {
            SolidBrush blackBrush = new SolidBrush(Color.Gray);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            Pen pen = new Pen(Color.Green, 2);
            Pen redPen = new Pen(Color.Red, 2);
            //Прорисовка клеток
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if ((i + j) % 2 == 0)
                        g.FillRectangle(blackBrush, new Rectangle(j * CellSize, i * CellSize, CellSize, CellSize));
                    else
                        g.FillRectangle(whiteBrush, new Rectangle(j * CellSize, i * CellSize, CellSize, CellSize));

            //Прорисовка фигур
            foreach (var figure in figures)
            {
                figure.Show(g, this);
            }

            //Прорисовка выделенной области
            if (X / CellSize < 8 && Y / CellSize < 8) g.DrawRectangle(pen, X, Y, CellSize, CellSize);

            //Прорисовка всех позиций фигуры
            //Figure f = GetFigure(new Point(_x / CellSize, _y / CellSize));
            if (selectedFigure != null)
            {
                var points = selectedFigure.GetPoints(this);
                foreach (var point in points)
                {
                    g.DrawRectangle(redPen, point.X * CellSize, point.Y * CellSize, CellSize, CellSize);
                }
            }

        }

    }
}
