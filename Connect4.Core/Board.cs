using System;

namespace Connect4.Core
{
    /// <summary>
    /// Представляет игровое поле размером 7x6 для игры "Четыре в ряд".
    /// Управляет состоянием ячеек и логикой размещения фишек.
    /// </summary>
    public class Board
    {
        // Количество строк на поле (стандарт 6).
        public int Rows { get; } = 6;

        // Количество столбцов на поле (стандарт 7).
        public int Cols { get; } = 7;

        // Двумерный массив, хранящий состояние каждой ячейки.
        // 0 - пусто, 1 - игрок 1 (Красный), 2 - игрок 2 (Желтый).
        public int[,] Cells { get; private set; }

        /// <summary>
        /// Инициализирует новое пустое игровое поле.
        /// </summary>
        public Board() => Reset();

        /// <summary>
        /// Сбрасывает состояние поля, очищая все ячейки.
        /// </summary>
        public void Reset() => Cells = new int[Rows, Cols];

        /// <summary>
        /// Размещает фишку игрока в указанном столбце. Фишка падает до нижней свободной ячейки.
        /// </summary>
        /// <param name="col">Индекс столбца (от 0 до 6).</param>
        /// <param name="player">ID игрока (1 или 2).</param>
        /// <returns>Индекс строки, куда упала фишка, или -1, если столбец заполнен.</returns>
        public int Drop(int col, int player)
        {
            if (col < 0 || col >= Cols) return -1;
            for (int r = Rows - 1; r >= 0; r--)
                if (Cells[r, col] == 0) { Cells[r, col] = player; return r; }
            return -1; // Столбец полон
        }

        /// <summary>
        /// Проверяет, заполнено ли всё игровое поле фишками.
        /// </summary>
        /// <returns>True, если свободных ячеек не осталось; иначе False.</returns>
        public bool IsFull()
        {
            for (int c = 0; c < Cols; c++)
                if (Cells[0, c] == 0) return false;
            return true;
        }

        /// <summary>
        /// Возвращает содержимое конкретной ячейки.
        /// </summary>
        /// <param name="r">Индекс строки.</param>
        /// <param name="c">Индекс столбца.</param>
        /// <returns>ID игрока в ячейке или 0, если ячейка пуста/выход за границы.</returns>
        public int Get(int r, int c) => (r >= 0 && r < Rows && c >= 0 && c < Cols) ? Cells[r, c] : 0;
    }
}