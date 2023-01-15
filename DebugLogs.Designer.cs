namespace ResponsiveForm.Test
{
    partial class DebugLogs
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
            this.label1 = new System.Windows.Forms.Label();
            this.listViewTable = new System.Windows.Forms.ListView();
            this.Name_ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.Menubar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Menubar
            // 
            this.Menubar.Controls.Add(this.label1);
            this.Menubar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Menubar.Location = new System.Drawing.Point(0, 0);
            this.Menubar.Name = "Menubar";
            this.Menubar.Size = new System.Drawing.Size(385, 31);
            this.Menubar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(108)))));
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Press Ctrl + D for Hide";
            // 
            // listViewTable
            // 
            this.listViewTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.listViewTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Name_,
            this.Value});
            this.listViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(108)))));
            this.listViewTable.HideSelection = false;
            this.listViewTable.Location = new System.Drawing.Point(0, 31);
            this.listViewTable.Name = "listViewTable";
            this.listViewTable.Size = new System.Drawing.Size(385, 419);
            this.listViewTable.TabIndex = 1;
            this.listViewTable.UseCompatibleStateImageBehavior = false;
            this.listViewTable.View = System.Windows.Forms.View.Details;
            // 
            // Name
            // 
            this.Name_.Text = "Name";
            this.Name_.Width = 182;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 198;
            // 
            // logBox
            // 
            this.logBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logBox.Location = new System.Drawing.Point(0, 303);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(385, 147);
            this.logBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.logBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(108)))));
            this.logBox.TabIndex = 2;
            this.logBox.Text = "";
            // 
            // DebugLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(42)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(385, 450);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.listViewTable);
            this.Controls.Add(this.Menubar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DebugLogs";
            this.Text = "DebugLogs";
            this.Menubar.ResumeLayout(false);
            this.Menubar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Menubar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewTable;
        private System.Windows.Forms.ColumnHeader Name_;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.RichTextBox logBox;
    }
}