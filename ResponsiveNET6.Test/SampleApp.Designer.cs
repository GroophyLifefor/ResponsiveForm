namespace ResponsiveNET6.Test
{
    partial class SampleApp
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
            this.menuBar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.minBtn = new System.Windows.Forms.Button();
            this.maxBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.A = new System.Windows.Forms.Button();
            this.B = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(136)))));
            this.menuBar.Controls.Add(this.label1);
            this.menuBar.Controls.Add(this.minBtn);
            this.menuBar.Controls.Add(this.maxBtn);
            this.menuBar.Controls.Add(this.closeBtn);
            this.menuBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(1200, 36);
            this.menuBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "label1";
            // 
            // minBtn
            // 
            this.minBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.minBtn.FlatAppearance.BorderSize = 0;
            this.minBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minBtn.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minBtn.ForeColor = System.Drawing.Color.Black;
            this.minBtn.Location = new System.Drawing.Point(1050, 0);
            this.minBtn.Name = "minBtn";
            this.minBtn.Size = new System.Drawing.Size(50, 36);
            this.minBtn.TabIndex = 8;
            this.minBtn.Text = " ─";
            this.minBtn.UseVisualStyleBackColor = true;
            // 
            // maxBtn
            // 
            this.maxBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.maxBtn.FlatAppearance.BorderSize = 0;
            this.maxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maxBtn.ForeColor = System.Drawing.Color.Black;
            this.maxBtn.Location = new System.Drawing.Point(1100, 0);
            this.maxBtn.Name = "maxBtn";
            this.maxBtn.Size = new System.Drawing.Size(50, 36);
            this.maxBtn.TabIndex = 7;
            this.maxBtn.Text = "▢";
            this.maxBtn.UseVisualStyleBackColor = false;
            // 
            // closeBtn
            // 
            this.closeBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.ForeColor = System.Drawing.Color.Black;
            this.closeBtn.Location = new System.Drawing.Point(1150, 0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(50, 36);
            this.closeBtn.TabIndex = 6;
            this.closeBtn.Text = "✕";
            this.closeBtn.UseVisualStyleBackColor = false;
            // 
            // A
            // 
            this.A.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.A.FlatAppearance.BorderSize = 0;
            this.A.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.A.Location = new System.Drawing.Point(130, 120);
            this.A.Name = "A";
            this.A.Size = new System.Drawing.Size(220, 100);
            this.A.TabIndex = 1;
            this.A.Text = "A";
            this.A.UseVisualStyleBackColor = false;
            // 
            // B
            // 
            this.B.BackColor = System.Drawing.Color.Olive;
            this.B.FlatAppearance.BorderSize = 0;
            this.B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B.Location = new System.Drawing.Point(500, 120);
            this.B.Name = "B";
            this.B.Size = new System.Drawing.Size(220, 194);
            this.B.TabIndex = 2;
            this.B.Text = "B";
            this.B.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // SampleApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(203)))), ((int)(((byte)(212)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.B);
            this.Controls.Add(this.A);
            this.Controls.Add(this.menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SampleApp";
            this.Text = "SampleApp";
            this.Load += new System.EventHandler(this.SampleApp_Load);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel menuBar;
        private Button minBtn;
        private Button maxBtn;
        private Button closeBtn;
        private Button A;
        private Button B;
        private Label label1;
        private TextBox textBox1;
    }
}