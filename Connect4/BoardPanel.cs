using System;
using System.Drawing;
using System.Windows.Forms;

namespace Connect4
{
    /// <summary>
    /// Кастомный Panel для отрисовки игрового поля 7×6
    /// </summary>
    public class BoardPanel : Panel
    {
        // Константы поля
        public const int Columns = 7;
        public const int Rows = 6;

        // Цвета
        private static readonly Color SlotEmpty = Color.White;
        private static readonly Color SlotRed = Color.FromArgb(220, 60, 60);
        private static readonly Color SlotYellow = Color.FromArgb(240, 200, 50);
        private static readonly Color SlotBorder = Color.FromArgb(20, 40, 90);
        private static readonly Color BoardBackground = Color.FromArgb(30, 60, 120);

        // Состояние поля: 0 = пусто, 1 = красный, 2 = жёлтый
        private readonly int[,] _cells = new int[Rows, Columns];

        // Отрисовка
        private int _cellSize;
        private int _padding;

        public BoardPanel()
        {
            this.DoubleBuffered = true; // Убирает мерцание при перерисовке
            this.ResizeRedraw = true;
        }

        /// <summary>
        /// Сбросить поле в начальное состояние
        /// </summary>
        public void Reset()
        {
            Array.Clear(_cells, 0, _cells.Length);
            this.Invalidate(); // Перерисовать
        }

        /// <summary>
        /// Установить фишку в колонку (возвращает строку, куда упала, или -1 если колонка полна)
        /// </summary>
        public int DropChip(int column, int player) // player: 1=красный, 2=жёлтый
        {
            if (column < 0 || column >= Columns) return -1;

            // Ищем нижнюю свободную ячейку в колонке
            for (int row = Rows - 1; row >= 0; row--)
            {
                if (_cells[row, column] == 0)
                {
                    _cells[row, column] = player;
                    this.Invalidate(); // Перерисовать
                    return row;
                }
            }
            return -1; // Колонка заполнена
        }

        /// <summary>
        /// Получить значение ячейки
        /// </summary>
        public int GetCell(int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                return 0;
            return _cells[row, column];
        }

        /// <summary>
        /// Определить, по какой колонке кликнули (по координатам мыши)
        /// </summary>
        public int GetColumnAtPoint(Point point)
        {
            if (_cellSize == 0) return -1;

            // Учитываем отступы
            int boardLeft = (this.Width - (Columns * _cellSize + (Columns - 1) * _padding)) / 2;

            for (int col = 0; col < Columns; col++)
            {
                int xStart = boardLeft + col * (_cellSize + _padding);
                if (point.X >= xStart && point.X <= xStart + _cellSize)
                    return col;
            }
            return -1;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CalculateLayout();
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawBoard(e.Graphics);
        }

        private void CalculateLayout()
        {
            // Вычисляем размер ячейки с учётом пропорций и отступов
            int availableWidth = this.Width - 40;  // отступы по бокам
            int availableHeight = this.Height - 40;

            int sizeByWidth = availableWidth / Columns;
            int sizeByHeight = availableHeight / Rows;

            _cellSize = Math.Min(sizeByWidth, sizeByHeight);
            _cellSize = Math.Max(40, Math.Min(_cellSize, 80)); // Ограничиваем размер: 40-80px
            _padding = _cellSize / 5; // Отступ между ячейками ~20%
        }

        private void DrawBoard(Graphics g)
        {
            // Фон доски
            using (var bgBrush = new SolidBrush(BoardBackground))
                g.FillRectangle(bgBrush, this.ClientRectangle);

            if (_cellSize == 0) return;

            // Центрируем поле
            int totalWidth = Columns * _cellSize + (Columns - 1) * _padding;
            int totalHeight = Rows * _cellSize + (Rows - 1) * _padding;
            int startX = (this.Width - totalWidth) / 2;
            int startY = (this.Height - totalHeight) / 2;

            // Рисуем ячейки
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    int x = startX + col * (_cellSize + _padding);
                    int y = startY + row * (_cellSize + _padding);

                    DrawSlot(g, x, y, _cellSize, _cells[row, col]);
                }
            }
        }

        private void DrawSlot(Graphics g, int x, int y, int size, int value)
        {
            // Тень/обводка слота
            using (var borderPen = new Pen(SlotBorder, size / 15))
            using (var slotBrush = new SolidBrush(value == 0 ? SlotEmpty :
                                                  value == 1 ? SlotRed : SlotYellow))
            {
                // Рисуем круглую "лунку"
                g.FillEllipse(slotBrush, x + 2, y + 2, size - 4, size - 4);
                g.DrawEllipse(borderPen, x + 2, y + 2, size - 4, size - 4);

                // Лёгкий градиент для объёма (опционально)
                if (value != 0)
                {
                    using (var highlightBrush = new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                        g.FillEllipse(highlightBrush, x + size / 4, y + size / 6, size / 3, size / 4);
                }
            }
        }
    }
}