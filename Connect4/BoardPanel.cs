using System;
using System.Drawing;
using System.Windows.Forms;
using Connect4.Core;

namespace Connect4
{
    /// <summary>
    /// Кастомный элемент управления для визуализации игрового поля Connect Four.
    /// Наследуется от Panel и переопределяет процесс отрисовки (OnPaint) для 
    /// отображения сетки 7x6 с фишками игроков.
    /// </summary>
    public class BoardPanel : Panel
    {
        // Ссылка на объект доски из бизнес-логики (Connect4.Core).
        // Используется только для чтения состояния ячеек.
        private Board _board;

        // Вычисленные размеры для отрисовки
        private int _cellSize;  // Диаметр одной ячейки (фишки/лунки) в пикселях
        private int _padding;   // Расстояние между соседними ячейками

        // Палитра цветов для отрисовки
        // Индекс 0: Пустая ячейка (белый/фон лунки)
        // Индекс 1: Фишка игрока 1 (Красный)
        // Индекс 2: Фишка игрока 2 (Жёлтый)
        private readonly Color[] _colors =
        {
            Color.White,
            Color.FromArgb(220, 60, 60),
            Color.FromArgb(240, 200, 50)
        };

        // Цвет обводки лунок (тёмно-синий для контраста с фоном доски)
        private readonly Color _borderColor = Color.FromArgb(20, 40, 90);

        /// <summary>
        /// Инициализирует новый экземпляр BoardPanel.
        /// Включает двойную буферизацию для предотвращения мерцания при частой перерисовке
        /// и настройку автоматической перерисовки при изменении размера контрола.
        /// </summary>
        public BoardPanel()
        {
            // DoubleBuffered = true рисует сначала в память, потом выводит на экран.
            // Это критично для игр, чтобы избежать "мигания" при каждом ходе.
            this.DoubleBuffered = true;

            // ResizeRedraw = true заставляет контрол полностью перерисовываться 
            // при любом изменении его размеров (например, при растягивании окна).
            this.ResizeRedraw = true;
        }

        /// <summary>
        /// Обновляет ссылку на модель данных (Board) и инициирует перерисовку контрола.
        /// Этот метод следует вызывать при старте игры или после каждого хода игрока.
        /// </summary>
        /// <param name="board">Актуальное состояние игрового поля из Core.</param>
        public void Render(Board board)
        {
            _board = board;

            // Принудительно пересчитываем геометрию ячеек под текущий размер панели.
            // Это решает проблему "точки" при первом запуске, когда OnResize ещё не сработал.
            CalculateLayout();

            // Invalidate() помечает область контрола как требующую перерисовки.
            // Система Windows отправит сообщение WM_PAINT, и вызовется метод OnPaint().
            this.Invalidate();
        }

        /// <summary>
        /// Вызывается системой при изменении размеров панели.
        /// Пересчитывает размеры ячеек, чтобы поле всегда оставалось пропорциональным 
        /// и центрированным, независимо от размера окна приложения.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CalculateLayout();
            this.Invalidate();
        }

        /// <summary>
        /// Рассчитывает размер ячейки (_cellSize) и отступ (_padding) на основе 
        /// текущих размеров панели (Width/Height) и размерности доски (Rows/Cols).
        /// Алгоритм подбирает максимальный возможный размер фишки, который помещается 
        /// в видимую область с небольшими отступами по краям.
        /// </summary>
        private void CalculateLayout()
        {
            if (_board == null) return;

            // Доступное пространство минус небольшие поля по краям (по 20px с каждой стороны)
            int availableWidth = this.Width - 40;
            int availableHeight = this.Height - 40;

            // Максимально возможный размер ячейки по ширине и высоте
            int w = availableWidth / _board.Cols;
            int h = availableHeight / _board.Rows;

            // Выбираем меньшую сторону, чтобы ячейки были квадратными и влезали целиком.
            // Ограничиваем размер: минимум 30px (чтобы было видно), максимум 70px (чтобы не было гигантским).
            _cellSize = Math.Max(30, Math.Min(70, Math.Min(w, h)));

            // Отступ между ячейками составляет 20% от размера ячейки
            _padding = _cellSize / 5;
        }

        /// <summary>
        /// Основной метод отрисовки. Вызывается автоматически при необходимости обновить изображение.
        /// Очищает фон и последовательно рисует все лунки игрового поля.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Если доска еще не инициализирована, ничего не рисуем
            if (_board == null || _cellSize <= 0) return;

            // 1. Очистка фона доски (темно-синий цвет)
            e.Graphics.Clear(Color.FromArgb(30, 60, 120));

            // Включаем сглаживание для красивых круглых фишек без "лесенок"
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // 2. Расчет общих размеров игрового поля в пикселях
            int totalW = _board.Cols * _cellSize + (_board.Cols - 1) * _padding;
            int totalH = _board.Rows * _cellSize + (_board.Rows - 1) * _padding;

            // 3. Центрирование поля внутри панели
            int startX = (this.Width - totalW) / 2;
            int startY = (this.Height - totalH) / 2;

            // 4. Двойной цикл отрисовки каждой ячейки
            for (int r = 0; r < _board.Rows; r++)
            {
                for (int c = 0; c < _board.Cols; c++)
                {
                    // Вычисляем координаты верхнего левого угла конкретной лунки
                    int x = startX + c * (_cellSize + _padding);
                    int y = startY + r * (_cellSize + _padding);

                    // Получаем состояние ячейки (0 - пусто, 1 - красный, 2 - желтый)
                    int playerValue = _board.Get(r, c);

                    // Рисуем конкретную лунку
                    DrawSlot(e.Graphics, x, y, playerValue);
                }
            }
        }

        /// <summary>
        /// Рисует одну лунку (ячейку) на переданной поверхности Graphics.
        /// Если ячейка занята, рисует фишку соответствующего цвета с бликом.
        /// Если пуста — рисует только контур ("дырку" в доске).
        /// </summary>
        /// <param name="g">Поверхность для рисования.</param>
        /// <param name="x">Координата X верхнего левого угла.</param>
        /// <param name="y">Координата Y верхнего левого угла.</param>
        /// <param name="player">ID игрока (0, 1 или 2).</param>
        private void DrawSlot(Graphics g, int x, int y, int player)
        {
            int size = _cellSize;

            // Создаем кисть для заполнения (цвет зависит от игрока)
            using var brush = new SolidBrush(_colors[player]);

            // Создаем перо для обводки (толщина зависит от размера ячейки)
            using var pen = new Pen(_borderColor, Math.Max(1, size / 15));

            // Рисуем заполненный круг (саму лунку или фишку)
            // x+2, y+2 и size-4 создают небольшой внутренний отступ, чтобы обводка не обрезалась
            g.FillEllipse(brush, x + 2, y + 2, size - 4, size - 4);

            // Рисуем контур круга
            g.DrawEllipse(pen, x + 2, y + 2, size - 4, size - 4);

            // Если ячейка занята (игрок 1 или 2), добавляем декоративный блик для объема
            if (player != 0)
            {
                // Полупрозрачный белый эллипс в верхней левой части фишки
                using var highlight = new SolidBrush(Color.FromArgb(60, 255, 255, 255));
                g.FillEllipse(highlight, x + size / 4, y + size / 6, size / 3, size / 4);
            }
        }

        /// <summary>
        /// Определяет индекс колонки (0-6), по которой кликнул пользователь.
        /// Использует обратный расчет координат на основе текущей геометрии поля.
        /// </summary>
        /// <param name="p">Координаты мыши относительно левого верхнего угла панели.</param>
        /// <returns>Индекс колонки (0-6) или -1, если клик был вне игрового поля.</returns>
        public int GetColumnAtPoint(Point p)
        {
            if (_board == null || _cellSize == 0) return -1;

            // Повторяем расчет центрирования, чтобы знать, где начинается первая колонка
            int totalW = _board.Cols * _cellSize + (_board.Cols - 1) * _padding;
            int startX = (this.Width - totalW) / 2;

            // Проходим по всем колонкам и проверяем, попадает ли X курсора в диапазон колонки
            for (int c = 0; c < _board.Cols; c++)
            {
                int xStart = startX + c * (_cellSize + _padding);

                // Если курсор находится между началом и концом колонки
                if (p.X >= xStart && p.X <= xStart + _cellSize)
                {
                    return c;
                }
            }

            // Клик был в промежутке между колонками или за пределами поля
            return -1;
        }
    }
}