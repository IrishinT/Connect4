using System;
using System.Drawing;
using System.Windows.Forms;

namespace Connect4
{
    public partial class TitleForm : Form
    {
        private readonly Color _btnStartHover = Color.FromArgb(35, 105, 70);
        private readonly Color _btnStartNormal = Color.FromArgb(45, 125, 85);
        private readonly Color _btnAboutHover = Color.FromArgb(90, 100, 110);
        private readonly Color _btnAboutNormal = Color.FromArgb(108, 117, 125);

        public TitleForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var gameForm = new GameForm();
            this.Hide();
            gameForm.FormClosed += (s, args) => this.Close();
            gameForm.Show();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Connect Four — классическая стратегия для двух игроков.\n\n" +
                "Цель: выстроить 4 фишки своего цвета в ряд.\n" +
                "Ход: клик по столбцу → фишка падает вниз.\n" +
                "Победа: 4 в ряд по горизонтали, вертикали или диагонали.",
                "Об игре",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void btnStart_MouseEnter(object sender, EventArgs e) => btnStart.BackColor = _btnStartHover;
        private void btnStart_MouseLeave(object sender, EventArgs e) => btnStart.BackColor = _btnStartNormal;
        private void btnAbout_MouseEnter(object sender, EventArgs e) => btnAbout.BackColor = _btnAboutHover;
        private void btnAbout_MouseLeave(object sender, EventArgs e) => btnAbout.BackColor = _btnAboutNormal;
    }
}