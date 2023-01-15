namespace ResponsiveForm.Test
{
    partial class MusicPlayer
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
            this.Menubar = new System.Windows.Forms.Panel();
            this.minBtn = new System.Windows.Forms.Button();
            this.maxBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Songs = new System.Windows.Forms.TreeView();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.time = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Menubar.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // Menubar
            // 
            this.Menubar.Controls.Add(this.minBtn);
            this.Menubar.Controls.Add(this.maxBtn);
            this.Menubar.Controls.Add(this.closeBtn);
            this.Menubar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Menubar.Location = new System.Drawing.Point(0, 0);
            this.Menubar.Name = "Menubar";
            this.Menubar.Size = new System.Drawing.Size(602, 30);
            this.Menubar.TabIndex = 0;
            // 
            // minBtn
            // 
            this.minBtn.BackColor = System.Drawing.Color.SeaGreen;
            this.minBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.minBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minBtn.Location = new System.Drawing.Point(377, 0);
            this.minBtn.Name = "minBtn";
            this.minBtn.Size = new System.Drawing.Size(75, 30);
            this.minBtn.TabIndex = 5;
            this.minBtn.UseVisualStyleBackColor = false;
            // 
            // maxBtn
            // 
            this.maxBtn.BackColor = System.Drawing.Color.Moccasin;
            this.maxBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.maxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maxBtn.Location = new System.Drawing.Point(452, 0);
            this.maxBtn.Name = "maxBtn";
            this.maxBtn.Size = new System.Drawing.Size(75, 30);
            this.maxBtn.TabIndex = 4;
            this.maxBtn.UseVisualStyleBackColor = false;
            // 
            // closeBtn
            // 
            this.closeBtn.BackColor = System.Drawing.Color.IndianRed;
            this.closeBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Location = new System.Drawing.Point(527, 0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(75, 30);
            this.closeBtn.TabIndex = 3;
            this.closeBtn.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Songs);
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 379);
            this.panel1.TabIndex = 1;
            // 
            // Songs
            // 
            this.Songs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.Songs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Songs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(108)))));
            this.Songs.Location = new System.Drawing.Point(0, 0);
            this.Songs.Name = "Songs";
            this.Songs.Size = new System.Drawing.Size(192, 379);
            this.Songs.TabIndex = 2;
            this.Songs.DoubleClick += new System.EventHandler(this.Songs_DoubleClick);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(198, 350);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(392, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(108)))));
            this.time.Location = new System.Drawing.Point(206, 380);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(13, 13);
            this.time.TabIndex = 3;
            this.time.Text = "--";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(206, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // MusicPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(602, 409);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.time);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Menubar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MusicPlayer";
            this.Text = "MusicPlayer";
            this.Load += new System.EventHandler(this.MusicPlayer_Load);
            this.Menubar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button minBtn;
        private System.Windows.Forms.Button maxBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel Menubar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView Songs;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Label label1;
    }
}