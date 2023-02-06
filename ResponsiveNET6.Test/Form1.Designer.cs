namespace ResponsiveNET6.Test
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuBar = new System.Windows.Forms.Panel();
            this.createAPublicNotePanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.moveableMenuBar = new System.Windows.Forms.Panel();
            this.minBtn = new System.Windows.Forms.Button();
            this.maxBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.leftMenuBar = new System.Windows.Forms.Panel();
            this.fintoryPanel = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fintoryImage = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.sideBar = new System.Windows.Forms.Panel();
            this.helpPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.scintilla = new ScintillaNET.Scintilla();
            this.preview = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.hiddenBlock = new System.Windows.Forms.Panel();
            this.menuBar.SuspendLayout();
            this.createAPublicNotePanel.SuspendLayout();
            this.moveableMenuBar.SuspendLayout();
            this.leftMenuBar.SuspendLayout();
            this.fintoryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fintoryImage)).BeginInit();
            this.sideBar.SuspendLayout();
            this.helpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.BackColor = System.Drawing.Color.White;
            this.menuBar.Controls.Add(this.createAPublicNotePanel);
            this.menuBar.Controls.Add(this.moveableMenuBar);
            this.menuBar.Controls.Add(this.leftMenuBar);
            this.menuBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Size = new System.Drawing.Size(991, 73);
            this.menuBar.TabIndex = 3;
            // 
            // createAPublicNotePanel
            // 
            this.createAPublicNotePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(126)))), ((int)(((byte)(255)))));
            this.createAPublicNotePanel.Controls.Add(this.label4);
            this.createAPublicNotePanel.Controls.Add(this.label3);
            this.createAPublicNotePanel.Location = new System.Drawing.Point(843, 40);
            this.createAPublicNotePanel.Name = "createAPublicNotePanel";
            this.createAPublicNotePanel.Size = new System.Drawing.Size(134, 28);
            this.createAPublicNotePanel.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "✖ Create a public note";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "✖ Create a public note";
            // 
            // moveableMenuBar
            // 
            this.moveableMenuBar.Controls.Add(this.minBtn);
            this.moveableMenuBar.Controls.Add(this.maxBtn);
            this.moveableMenuBar.Controls.Add(this.closeBtn);
            this.moveableMenuBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.moveableMenuBar.Location = new System.Drawing.Point(200, 0);
            this.moveableMenuBar.Name = "moveableMenuBar";
            this.moveableMenuBar.Size = new System.Drawing.Size(791, 38);
            this.moveableMenuBar.TabIndex = 4;
            // 
            // minBtn
            // 
            this.minBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.minBtn.FlatAppearance.BorderSize = 0;
            this.minBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minBtn.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minBtn.ForeColor = System.Drawing.Color.Black;
            this.minBtn.Location = new System.Drawing.Point(641, 0);
            this.minBtn.Name = "minBtn";
            this.minBtn.Size = new System.Drawing.Size(50, 38);
            this.minBtn.TabIndex = 5;
            this.minBtn.Text = " ─";
            this.minBtn.UseVisualStyleBackColor = true;
            // 
            // maxBtn
            // 
            this.maxBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.maxBtn.FlatAppearance.BorderSize = 0;
            this.maxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maxBtn.ForeColor = System.Drawing.Color.Black;
            this.maxBtn.Location = new System.Drawing.Point(691, 0);
            this.maxBtn.Name = "maxBtn";
            this.maxBtn.Size = new System.Drawing.Size(50, 38);
            this.maxBtn.TabIndex = 4;
            this.maxBtn.Text = "▢";
            this.maxBtn.UseVisualStyleBackColor = false;
            // 
            // closeBtn
            // 
            this.closeBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.ForeColor = System.Drawing.Color.Black;
            this.closeBtn.Location = new System.Drawing.Point(741, 0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(50, 38);
            this.closeBtn.TabIndex = 0;
            this.closeBtn.Text = "✕";
            this.closeBtn.UseVisualStyleBackColor = false;
            // 
            // leftMenuBar
            // 
            this.leftMenuBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.leftMenuBar.Controls.Add(this.fintoryPanel);
            this.leftMenuBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftMenuBar.Location = new System.Drawing.Point(0, 0);
            this.leftMenuBar.Name = "leftMenuBar";
            this.leftMenuBar.Size = new System.Drawing.Size(200, 73);
            this.leftMenuBar.TabIndex = 3;
            // 
            // fintoryPanel
            // 
            this.fintoryPanel.BackColor = System.Drawing.Color.White;
            this.fintoryPanel.Controls.Add(this.pictureBox2);
            this.fintoryPanel.Controls.Add(this.pictureBox1);
            this.fintoryPanel.Controls.Add(this.label2);
            this.fintoryPanel.Controls.Add(this.label1);
            this.fintoryPanel.Controls.Add(this.fintoryImage);
            this.fintoryPanel.Location = new System.Drawing.Point(12, 18);
            this.fintoryPanel.Name = "fintoryPanel";
            this.fintoryPanel.Size = new System.Drawing.Size(170, 48);
            this.fintoryPanel.TabIndex = 6;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(146, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 19);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(146, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(21, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(65, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Workspace";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Markdig UI";
            // 
            // fintoryImage
            // 
            this.fintoryImage.Image = ((System.Drawing.Image)(resources.GetObject("fintoryImage.Image")));
            this.fintoryImage.Location = new System.Drawing.Point(7, 3);
            this.fintoryImage.Name = "fintoryImage";
            this.fintoryImage.Size = new System.Drawing.Size(48, 44);
            this.fintoryImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fintoryImage.TabIndex = 0;
            this.fintoryImage.TabStop = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(19, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 19);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "AutoRefresh";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // sideBar
            // 
            this.sideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.sideBar.Controls.Add(this.checkBox1);
            this.sideBar.Controls.Add(this.helpPanel);
            this.sideBar.Location = new System.Drawing.Point(0, 73);
            this.sideBar.Name = "sideBar";
            this.sideBar.Size = new System.Drawing.Size(200, 608);
            this.sideBar.TabIndex = 5;
            // 
            // helpPanel
            // 
            this.helpPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(126)))), ((int)(((byte)(255)))));
            this.helpPanel.Controls.Add(this.label5);
            this.helpPanel.Location = new System.Drawing.Point(19, 36);
            this.helpPanel.Name = "helpPanel";
            this.helpPanel.Size = new System.Drawing.Size(163, 45);
            this.helpPanel.TabIndex = 8;
            this.helpPanel.Click += new System.EventHandler(this.helpPanel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(5, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "🕮       Show mermaid";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // scintilla
            // 
            this.scintilla.AutoCMaxHeight = 9;
            this.scintilla.BiDirectionality = ScintillaNET.BiDirectionalDisplayType.Disabled;
            this.scintilla.CaretLineBackColor = System.Drawing.Color.White;
            this.scintilla.CaretLineVisible = true;
            this.scintilla.Lexer = ScintillaNET.Lexer.Markdown;
            this.scintilla.LexerName = "markdown";
            this.scintilla.Location = new System.Drawing.Point(206, 79);
            this.scintilla.Name = "scintilla";
            this.scintilla.ScrollWidth = 1575;
            this.scintilla.Size = new System.Drawing.Size(376, 595);
            this.scintilla.TabIndents = true;
            this.scintilla.TabIndex = 6;
            this.scintilla.Text = resources.GetString("scintilla.Text");
            this.scintilla.UseRightToLeftReadingLayout = false;
            this.scintilla.WrapMode = ScintillaNET.WrapMode.None;
            this.scintilla.TextChanged += new System.EventHandler(this.scintilla_TextChanged);
            // 
            // preview
            // 
            this.preview.AllowExternalDrop = true;
            this.preview.CreationProperties = null;
            this.preview.DefaultBackgroundColor = System.Drawing.Color.White;
            this.preview.Location = new System.Drawing.Point(588, 79);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(391, 595);
            this.preview.TabIndex = 7;
            this.preview.ZoomFactor = 1D;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusBar.Location = new System.Drawing.Point(0, 684);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(991, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 8;
            this.statusBar.Text = "statusBar";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(57, 17);
            this.toolStripStatusLabel1.Text = "Welcome";
            // 
            // hiddenBlock
            // 
            this.hiddenBlock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hiddenBlock.Location = new System.Drawing.Point(0, 674);
            this.hiddenBlock.Name = "hiddenBlock";
            this.hiddenBlock.Size = new System.Drawing.Size(991, 10);
            this.hiddenBlock.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(991, 706);
            this.Controls.Add(this.hiddenBlock);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.scintilla);
            this.Controls.Add(this.sideBar);
            this.Controls.Add(this.menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuBar.ResumeLayout(false);
            this.createAPublicNotePanel.ResumeLayout(false);
            this.createAPublicNotePanel.PerformLayout();
            this.moveableMenuBar.ResumeLayout(false);
            this.leftMenuBar.ResumeLayout(false);
            this.fintoryPanel.ResumeLayout(false);
            this.fintoryPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fintoryImage)).EndInit();
            this.sideBar.ResumeLayout(false);
            this.sideBar.PerformLayout();
            this.helpPanel.ResumeLayout(false);
            this.helpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Panel menuBar;
        private Button closeBtn;
        private CheckBox checkBox1;
        private Panel leftMenuBar;
        private Panel sideBar;
        private Panel fintoryPanel;
        private Panel moveableMenuBar;
        private Button minBtn;
        private Button maxBtn;
        private PictureBox fintoryImage;
        private Label label1;
        private Label label2;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel createAPublicNotePanel;
        private Label label3;
        private ScintillaNET.Scintilla scintilla;
        private Microsoft.Web.WebView2.WinForms.WebView2 preview;
        private Panel helpPanel;
        private Label label4;
        private Label label5;
        private StatusStrip statusBar;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Panel hiddenBlock;
    }
}