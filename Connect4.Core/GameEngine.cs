using System;

namespace Connect4.Core
{
    /// <summary>
    /// Основной класс управления игровой сессией.
    /// Отвечает за очередность ходов, проверку условий победы, таймер и обработку результатов.
    /// </summary>
    public class GameEngine
    {
        // Событие, возникающее при смене активного игрока.
        public event Action<int> TurnChanged;          // 1 или 2

        // Событие, возникающее при завершении игры (победа, ничья или таймаут).
        public event Action<GameResult> GameFinished;

        // Событие-предупреждение, когда до конца времени хода остается мало секунд.
        public event Action<int> TimeWarning;          // < 5 сек для визуального эффекта

        // Текущее состояние игрового поля.
        public Board Board { get; }

        // ID текущего игрока (1 - Красный, 2 - Желтый).
        public int CurrentPlayer { get; private set; } = 1;

        // Флаг завершения игры.
        public bool IsGameOver { get; private set; }

        // Оставшееся время на текущий ход в секундах.
        public int TimeLeft { get; private set; }

        // Лимит времени на один ход (по умолчанию 15 секунд).
        public int TurnTimeLimit { get; set; } = 15;   // Условный лимит хода

        /// <summary>
        /// Создает новый экземпляр движка с пустой доской.
        /// </summary>
        public GameEngine() => Board = new Board();

        /// <summary>
        /// Инициализирует новую игру: сбрасывает доску, таймер и устанавливает первого игрока.
        /// </summary>
        public void StartNewGame()
        {
            Board.Reset();
            CurrentPlayer = 1;
            IsGameOver = false;
            ResetTimer();
            TurnChanged?.Invoke(CurrentPlayer);
        }

        /// <summary>
        /// Сбрасывает таймер хода к начальному значению.
        /// </summary>
        public void ResetTimer() => TimeLeft = TurnTimeLimit;

        /// <summary>
        /// Обновляет таймер (должен вызываться каждую секунду из UI).
        /// При истечении времени фиксирует поражение текущего игрока.
        /// </summary>
        public void Tick()
        {
            if (IsGameOver || TimeLeft <= 0) return;
            TimeLeft--;
            if (TimeLeft == 5) TimeWarning?.Invoke(5);
            if (TimeLeft == 0) Finish(GameResult.Timeout);
        }

        /// <summary>
        /// Пытается совершить ход в указанный столбец.
        /// </summary>
        /// <param name="col">Индекс столбца для хода.</param>
        /// <returns>Результат попытки хода (успех, ошибка, победа и т.д.).</returns>
        public GameResult TryDrop(int col)
        {
            if (IsGameOver) return GameResult.None;

            int row = Board.Drop(col, CurrentPlayer);
            if (row == -1) return GameResult.InvalidMove;

            if (CheckWin(row, col, CurrentPlayer))
            {
                Finish(CurrentPlayer == 1 ? GameResult.RedWins : GameResult.YellowWins);
                return GameResult.RedWins;
            }

            if (Board.IsFull()) { Finish(GameResult.Draw); return GameResult.Draw; }

            // Передача хода
            CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
            ResetTimer();
            TurnChanged?.Invoke(CurrentPlayer);
            return GameResult.None;
        }

        /// <summary>
        /// Метод завершения игры
        /// </summary>
        /// <param name="result">Результат игры</param>
        private void Finish(GameResult result) { IsGameOver = true; GameFinished?.Invoke(result); }

        /// <summary>
        /// Проверяет наличие 4 фишек в ряд от последней поставленной точки.
        /// </summary>
        private bool CheckWin(int row, int col, int player)
        {
            int[] dr = { 0, 1, 1, 1 };
            int[] dc = { 1, 0, 1, -1 };

            for (int i = 0; i < 4; i++)
            {
                int count = 1;
                for (int s = 1; s < 4; s++) if (Board.Get(row + dr[i] * s, col + dc[i] * s) == player) count++; else break;
                for (int s = 1; s < 4; s++) if (Board.Get(row - dr[i] * s, col - dc[i] * s) == player) count++; else break;
                if (count >= 4) return true;
            }
            return false;
        }
    }
}