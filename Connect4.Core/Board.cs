using System;

namespace Connect4.Core
{
    public class Board
    {
        public int Rows { get; } = 6;
        public int Cols { get; } = 7;
        public int[,] Cells { get; private set; }

        public Board() => Reset();

        public void Reset() => Cells = new int[Rows, Cols];

        public int Drop(int col, int player)
        {
            if (col < 0 || col >= Cols) return -1;
            for (int r = Rows - 1; r >= 0; r--)
                if (Cells[r, col] == 0) { Cells[r, col] = player; return r; }
            return -1; // Столбец полон
        }

        public bool IsFull()
        {
            for (int c = 0; c < Cols; c++)
                if (Cells[0, c] == 0) return false;
            return true;
        }

        public int Get(int r, int c) => (r >= 0 && r < Rows && c >= 0 && c < Cols) ? Cells[r, c] : 0;
    }
}