namespace Connect4
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlTop = new Panel();
            btnBack = new Button();
            lblTurn = new Label();
            btnRestart = new Button();
            boardPanel = new BoardPanel();
            pnlStatus = new Panel();
            lblTimer = new Label();
            pnlTop.SuspendLayout();
            pnlStatus.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.White;
            pnlTop.Controls.Add(btnBack);
            pnlTop.Controls.Add(lblTurn);
            pnlTop.Controls.Add(btnRestart);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.MinimumSize = new Size(0, 60);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(10);
            pnlTop.Size = new Size(900, 60);
            pnlTop.TabIndex = 0;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Left;
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10F);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(10, 15);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(120, 30);
            btnBack.TabIndex = 0;
            btnBack.Text = "← В меню";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // lblTurn
            // 
            lblTurn.Anchor = AnchorStyles.None;
            lblTurn.AutoSize = true;
            lblTurn.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTurn.ForeColor = Color.FromArgb(45, 85, 165);
            lblTurn.Location = new Point(320, 18);
            lblTurn.Name = "lblTurn";
            lblTurn.Size = new Size(202, 32);
            lblTurn.TabIndex = 1;
            lblTurn.Text = "● Ход: Красный";
            lblTurn.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnRestart
            // 
            btnRestart.Anchor = AnchorStyles.Right;
            btnRestart.BackColor = Color.FromArgb(45, 125, 85);
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.Font = new Font("Segoe UI", 10F);
            btnRestart.ForeColor = Color.White;
            btnRestart.Location = new Point(770, 15);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(120, 30);
            btnRestart.TabIndex = 2;
            btnRestart.Text = "🔄 Новая игра";
            btnRestart.UseVisualStyleBackColor = false;
            btnRestart.Click += btnRestart_Click;
            // 
            // boardPanel
            // 
            boardPanel.BackColor = Color.FromArgb(30, 60, 120);
            boardPanel.Dock = DockStyle.Fill;
            boardPanel.Location = new Point(0, 60);
            boardPanel.Name = "boardPanel";
            boardPanel.Size = new Size(900, 340);
            boardPanel.TabIndex = 1;
            boardPanel.MouseClick += boardPanel_MouseClick;
            // 
            // pnlStatus
            // 
            pnlStatus.BackColor = Color.FromArgb(245, 245, 245);
            pnlStatus.Controls.Add(lblTimer);
            pnlStatus.Dock = DockStyle.Bottom;
            pnlStatus.Location = new Point(0, 400);
            pnlStatus.MinimumSize = new Size(0, 50);
            pnlStatus.Name = "pnlStatus";
            pnlStatus.Padding = new Padding(10);
            pnlStatus.Size = new Size(900, 50);
            pnlStatus.TabIndex = 2;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            lblTimer.ForeColor = Color.FromArgb(45, 85, 165);
            lblTimer.Location = new Point(340, 12);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(120, 32);
            lblTimer.TabIndex = 0;
            lblTimer.Text = "⏱ 15с";
            lblTimer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 450);
            Controls.Add(boardPanel);
            Controls.Add(pnlStatus);
            Controls.Add(pnlTop);
            MinimumSize = new Size(700, 450);
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connect Four — Игра";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlStatus.ResumeLayout(false);
            pnlStatus.PerformLayout();
            ResumeLayout(false);
        }

        private Panel pnlTop;
        private Button btnBack;
        private Label lblTurn;
        private Button btnRestart;
        private BoardPanel boardPanel;
        private Panel pnlStatus;
        private Label lblTimer;
        private System.Windows.Forms.Timer _uiTimer;

        #endregion
    }
}