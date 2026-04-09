namespace Connect4
{
    partial class TitleForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnStart = new Button();
            btnAbout = new Button();
            pnlFooter = new Panel();
            lblAuthor = new Label();
            pnlContent = new Panel();
            tblLayout = new TableLayoutPanel();

            pnlFooter.SuspendLayout();
            tblLayout.SuspendLayout();
            pnlContent.SuspendLayout();
            SuspendLayout();

            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitle.ForeColor = Color.FromArgb(45, 85, 165);
            lblTitle.Location = new Point(100, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(273, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Connect Four";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.None;
            btnStart.BackColor = Color.FromArgb(45, 125, 85);
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(100, 100);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(300, 60);
            btnStart.TabIndex = 2;
            btnStart.Text = "▶ Начать игру";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            btnStart.MouseEnter += btnStart_MouseEnter;
            btnStart.MouseLeave += btnStart_MouseLeave;

            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.None;
            btnAbout.BackColor = Color.FromArgb(108, 117, 125);
            btnAbout.FlatAppearance.BorderSize = 0;
            btnAbout.FlatStyle = FlatStyle.Flat;
            btnAbout.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnAbout.ForeColor = Color.White;
            btnAbout.Location = new Point(100, 170);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(300, 50);
            btnAbout.TabIndex = 3;
            btnAbout.Text = "ℹ Об игре";
            btnAbout.UseVisualStyleBackColor = false;
            btnAbout.Click += btnAbout_Click;
            btnAbout.MouseEnter += btnAbout_MouseEnter;
            btnAbout.MouseLeave += btnAbout_MouseLeave;

            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(245, 245, 245);
            pnlFooter.Controls.Add(lblAuthor);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 400);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(800, 50);
            pnlFooter.TabIndex = 4;
            pnlFooter.MinimumSize = new Size(0, 50); // Не сжимать футер

            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblAuthor.ForeColor = Color.Gray;
            lblAuthor.Location = new Point(12, 18);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(180, 20);
            lblAuthor.TabIndex = 0;
            lblAuthor.Text = "Разработчик: Иришин Тимофей";

            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(tblLayout);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(10);
            pnlContent.Size = new Size(800, 400);
            pnlContent.TabIndex = 5;

            // 
            // tblLayout - центрирующий контейнер
            // 
            tblLayout.ColumnCount = 3;
            tblLayout.RowCount = 4;
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));  // Левый отступ (растягивается)
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));        // Центр (контент)
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));  // Правый отступ (растягивается)
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 0F));   // Пустой для выравнивания
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));        // Верхний отступ
            tblLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));            // Заголовок
            tblLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));            // Кнопка 1
            tblLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));            // Кнопка 2
            tblLayout.Dock = DockStyle.Top;
            tblLayout.Location = new Point(10, 40); // Отступ сверху внутри pnlContent
            tblLayout.Name = "tblLayout";
            tblLayout.Size = new Size(780, 300);
            tblLayout.TabIndex = 0;

            // Размещение элементов в центральной колонке (индекс 1)
            tblLayout.Controls.Add(lblTitle, 1, 1);
            tblLayout.Controls.Add(btnStart, 1, 2);
            tblLayout.Controls.Add(btnAbout, 1, 3);

            // 
            // Connect4MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlContent);
            Controls.Add(pnlFooter);
            MinimumSize = new Size(600, 450); // 🔒 Защита от слишком маленького окна
            Name = "Connect4MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connect Four";

            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            tblLayout.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlContent;
        private TableLayoutPanel tblLayout;
        private Label lblTitle;
        private Button btnStart;
        private Button btnAbout;
        private Panel pnlFooter;
        private Label lblAuthor;
    }
}
