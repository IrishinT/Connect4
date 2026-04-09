using System;
using System.Drawing;
using System.Windows.Forms;
using Connect4.Core;

namespace Connect4
{
    public class BoardPanel : Panel
    {
        private Board _board;
        private int _cellSize;
        private int _padding;
        private readonly Color[] _colors = { Color.White, Color.FromArgb(220, 60, 60), Color.FromArgb(240, 200, 50) };
        private readonly Color _borderColor = Color.FromArgb(20, 40, 90);

        public BoardPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        public void Render(Board board)
        {
            _board = board;
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CalculateLayout();
            Invalidate();
        }

        private void CalculateLayout()
        {
            if (_board == null) return;
            int w = (Width - 40) / _board.Cols;
            int h = (Height - 40) / _board.Rows;
            _cellSize = Math.Max(30, Math.Min(70, Math.Min(w, h)));
            _padding = _cellSize / 5;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_board == null) return;

            e.Graphics.Clear(Color.FromArgb(30, 60, 120));

            int totalW = _board.Cols * _cellSize + (_board.Cols - 1) * _padding;
            int totalH = _board.Rows * _cellSize + (_board.Rows - 1) * _padding;
            int startX = (Width - totalW) / 2;
            int startY = (Height - totalH) / 2;

            for (int r = 0; r < _board.Rows; r++)
                for (int c = 0; c < _board.Cols; c++)
                    DrawSlot(e.Graphics, startX + c * (_cellSize + _padding), startY + r * (_cellSize + _padding), _board.Get(r, c));
        }

        private void DrawSlot(Graphics g, int x, int y, int player)
        {
            int size = _cellSize;
            using var brush = new SolidBrush(_colors[player]);
            using var pen = new Pen(_borderColor, size / 15);
            g.FillEllipse(brush, x + 2, y + 2, size - 4, size - 4);
            g.DrawEllipse(pen, x + 2, y + 2, size - 4, size - 4);
            if (player != 0)
                g.FillEllipse(new SolidBrush(Color.FromArgb(60, 255, 255, 255)), x + size / 4, y + size / 6, size / 3, size / 4);
        }

        public int GetColumnAtPoint(Point p)
        {
            if (_board == null || _cellSize == 0) return -1;
            int totalW = _board.Cols * _cellSize + (_board.Cols - 1) * _padding;
            int startX = (Width - totalW) / 2;
            for (int c = 0; c < _board.Cols; c++)
            {
                int xStart = startX + c * (_cellSize + _padding);
                if (p.X >= xStart && p.X <= xStart + _cellSize) return c;
            }
            return -1;
        }
    }
}