using Connect4.Core;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Connect4
{
    public partial class GameForm : Form
    {
        private readonly GameEngine _game;
        private readonly Color _red = Color.FromArgb(220, 60, 60);
        private readonly Color _yellow = Color.FromArgb(240, 200, 50);

        public GameForm()
        {
            InitializeComponent();
            _game = new GameEngine { TurnTimeLimit = 15 };
            _game.TurnChanged += OnTurnChanged;
            _game.GameFinished += OnGameFinished;
            _game.TimeWarning += OnTimeWarning;

            _uiTimer = new System.Windows.Forms.Timer { Interval = 1000, Enabled = true };
            _uiTimer.Tick += (s, e) => { _game.Tick(); UpdateTimerLabel(); };

            _game.StartNewGame();
            boardPanel.Render(_game.Board);
        }

        private void UpdateTimerLabel()
        {
            lblTimer.Text = $"⏱ {_game.TimeLeft}с";
            lblTimer.ForeColor = _game.TimeLeft <= 5 ? Color.FromArgb(180, 50, 50) : Color.FromArgb(45, 85, 165);
        }

        private void OnTurnChanged(int player)
        {
            lblTurn.Text = player == 1 ? "● Ход: Красный" : "● Ход: Жёлтый";
            lblTurn.ForeColor = player == 1 ? _red : _yellow;
            UpdateTimerLabel();
        }

        private void OnTimeWarning(int sec)
            => MessageBox.Show($"Осталось {sec} секунд!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private void OnGameFinished(GameResult result)
        {
            _uiTimer.Stop();
            string msg = result switch
            {
                GameResult.RedWins => "🏆 Победа Красных!",
                GameResult.YellowWins => "🏆 Победа Жёлтых!",
                GameResult.Draw => "🤝 Ничья!",
                GameResult.Timeout => "⏰ Время вышло! Победа соперника.",
                _ => "Игра окончена."
            };
            MessageBox.Show(msg, "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (_game.IsGameOver) return;
            int col = boardPanel.GetColumnAtPoint(e.Location);
            if (col < 0) return;

            var res = _game.TryDrop(col);
            if (res != GameResult.InvalidMove)
                boardPanel.Render(_game.Board);
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            _game.StartNewGame();
            boardPanel.Render(_game.Board);
            _uiTimer.Start();
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
    }
}