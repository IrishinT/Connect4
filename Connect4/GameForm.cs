using System;
using System.Drawing;
using System.Windows.Forms;

namespace Connect4
{
    public partial class GameForm : Form
    {
        private int _currentPlayer = 1; // 1 = красный, 2 = жёлтый
        private bool _gameOver = false;

        // Цвета для индикатора хода
        private static readonly Color Red = Color.FromArgb(220, 60, 60);
        private static readonly Color Yellow = Color.FromArgb(240, 200, 50);

        public GameForm()
        {
            InitializeComponent();
            ResetGame();
        }

        private void ResetGame()
        {
            boardPanel.Reset();
            _currentPlayer = 1;
            _gameOver = false;
            UpdateStatus("Ход: Красный", Red);
            UpdateTurnIndicator(1);
        }

        private void UpdateStatus(string message, Color? color = null)
        {
            lblStatus.Text = message;
            if (color.HasValue)
                lblStatus.ForeColor = color.Value;
        }

        private void UpdateTurnIndicator(int player)
        {
            lblTurn.Text = player == 1 ? "● Ход: Красный" : "● Ход: Жёлтый";
            lblTurn.ForeColor = player == 1 ? Red : Yellow;
        }

        // Обработчик клика по полю
        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (_gameOver) return;

            int column = boardPanel.GetColumnAtPoint(e.Location);
            if (column < 0) return;

            int row = boardPanel.DropChip(column, _currentPlayer);
            if (row < 0)
            {
                // Колонка заполнена — можно добавить визуальный фидбек
                return;
            }

            _currentPlayer = _currentPlayer == 1 ? 2 : 1;
            UpdateTurnIndicator(_currentPlayer);
            UpdateStatus("Игра идёт...");
        }

        // Перерисовка при изменении размера поля
        private void boardPanel_Resize(object sender, EventArgs e)
        {
            boardPanel.Invalidate(); // Перерисовать поле
        }

        // Кнопка "В меню"
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // MainForm покажется, если был скрыт через Hide()
        }

        // Кнопка "Новая игра"
        private void btnRestart_Click(object sender, EventArgs e)
        {
            if (_gameOver || MessageBox.Show("Начать новую игру?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetGame();
            }
        }

        // TODO: Заготовки для будущей логики
        private bool CheckWin(int lastRow, int lastCol)
        {
            // TODO: проверка 4 направлений от последнего хода
            return false;
        }

        private bool IsBoardFull()
        {
            // TODO: проверка, заполнено ли поле
            return false;
        }
    }
}