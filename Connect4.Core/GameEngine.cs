using System;

namespace Connect4.Core
{
    public class GameEngine
    {
        public event Action<int> TurnChanged;          // 1 или 2
        public event Action<GameResult> GameFinished;
        public event Action<int> TimeWarning;          // < 5 сек для визуального эффекта

        public Board Board { get; }
        public int CurrentPlayer { get; private set; } = 1;
        public bool IsGameOver { get; private set; }
        public int TimeLeft { get; private set; }
        public int TurnTimeLimit { get; set; } = 15;   // Условный лимит хода

        public GameEngine() => Board = new Board();

        public void StartNewGame()
        {
            Board.Reset();
            CurrentPlayer = 1;
            IsGameOver = false;
            ResetTimer();
            TurnChanged?.Invoke(CurrentPlayer);
        }

        public void ResetTimer() => TimeLeft = TurnTimeLimit;

        /// <summary>Вызывать из UI-таймера каждую секунду</summary>
        public void Tick()
        {
            if (IsGameOver || TimeLeft <= 0) return;
            TimeLeft--;
            if (TimeLeft == 5) TimeWarning?.Invoke(5);
            if (TimeLeft == 0) Finish(GameResult.Timeout);
        }

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

        private void Finish(GameResult result) { IsGameOver = true; GameFinished?.Invoke(result); }

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