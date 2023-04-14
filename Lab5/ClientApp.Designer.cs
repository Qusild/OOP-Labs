namespace OOPLab5
{
    partial class ClientApp
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
            this.components = new System.ComponentModel.Container();
            this.ExitButton = new System.Windows.Forms.Button();
            this.RankButton = new System.Windows.Forms.Button();
            this.DeterminantButton = new System.Windows.Forms.Button();
            this.TranspositionButton = new System.Windows.Forms.Button();
            this.SizeScroller = new System.Windows.Forms.NumericUpDown();
            this.LayoutBox = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.NumberTypePicker = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.SizeScroller)).BeginInit();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(641, 177);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(147, 29);
            this.ExitButton.TabIndex = 0;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // RankButton
            // 
            this.RankButton.Location = new System.Drawing.Point(641, 142);
            this.RankButton.Name = "RankButton";
            this.RankButton.Size = new System.Drawing.Size(147, 29);
            this.RankButton.TabIndex = 1;
            this.RankButton.Text = "Ранг матрицы";
            this.RankButton.UseVisualStyleBackColor = true;
            this.RankButton.Click += new System.EventHandler(this.RankButton_Click);
            // 
            // DeterminantButton
            // 
            this.DeterminantButton.Location = new System.Drawing.Point(641, 107);
            this.DeterminantButton.Name = "DeterminantButton";
            this.DeterminantButton.Size = new System.Drawing.Size(147, 29);
            this.DeterminantButton.TabIndex = 2;
            this.DeterminantButton.Text = "Определитель";
            this.DeterminantButton.UseVisualStyleBackColor = true;
            this.DeterminantButton.Click += new System.EventHandler(this.DeterminantButton_Click);
            // 
            // TranspositionButton
            // 
            this.TranspositionButton.Location = new System.Drawing.Point(641, 72);
            this.TranspositionButton.Name = "TranspositionButton";
            this.TranspositionButton.Size = new System.Drawing.Size(147, 29);
            this.TranspositionButton.TabIndex = 3;
            this.TranspositionButton.Text = "Транспонировать";
            this.TranspositionButton.UseVisualStyleBackColor = true;
            this.TranspositionButton.Click += new System.EventHandler(this.TranspositionButton_Click);
            // 
            // SizeScroller
            // 
            this.SizeScroller.Location = new System.Drawing.Point(641, 5);
            this.SizeScroller.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SizeScroller.Name = "SizeScroller";
            this.SizeScroller.Size = new System.Drawing.Size(147, 27);
            this.SizeScroller.TabIndex = 4;
            this.SizeScroller.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SizeScroller.ValueChanged += new System.EventHandler(this.SizeScroller_ValueChanged);
            // 
            // LayoutBox
            // 
            this.LayoutBox.Location = new System.Drawing.Point(12, 12);
            this.LayoutBox.Name = "LayoutBox";
            this.LayoutBox.Size = new System.Drawing.Size(250, 125);
            this.LayoutBox.TabIndex = 5;
            // 
            // NumberTypePicker
            // 
            this.NumberTypePicker.FormattingEnabled = true;
            this.NumberTypePicker.Items.AddRange(new object[] {
            "Вещественные",
            "Рациональные",
            "Комплексные"});
            this.NumberTypePicker.Location = new System.Drawing.Point(641, 38);
            this.NumberTypePicker.Name = "NumberTypePicker";
            this.NumberTypePicker.Size = new System.Drawing.Size(147, 28);
            this.NumberTypePicker.TabIndex = 6;
            this.NumberTypePicker.Text = "Рациональные";
            this.NumberTypePicker.SelectedIndexChanged += new System.EventHandler(this.NumberTypePicker_SelectedIndexChanged);
            // 
            // ClientApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.NumberTypePicker);
            this.Controls.Add(this.LayoutBox);
            this.Controls.Add(this.SizeScroller);
            this.Controls.Add(this.TranspositionButton);
            this.Controls.Add(this.DeterminantButton);
            this.Controls.Add(this.RankButton);
            this.Controls.Add(this.ExitButton);
            this.Name = "ClientApp";
            this.Text = "ClientApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientApp_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.SizeScroller)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button ExitButton;
        private Button RankButton;
        private Button DeterminantButton;
        private Button TranspositionButton;
        private NumericUpDown SizeScroller;
        private FlowLayoutPanel LayoutBox;
        private ToolTip toolTip1;
        private ComboBox NumberTypePicker;
    }
}