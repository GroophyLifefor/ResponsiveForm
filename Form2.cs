using Responsive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponsiveForm.Test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            AllocConsole();
            Thread thread = new Thread(term);
            thread.Start();
        }


        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void Form2_Load(object sender, EventArgs e)
        {
            Sizing sizing = new Sizing(this);
            sizing.CreateNewConnection(richTextBox1,    menuStrip1,     Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button1,         richTextBox1,   Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button2,         button1,        Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button3,         button2,        Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button4,         richTextBox1,   Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button4,         button1,        Sizing.MarginSection.Left);
            sizing.CreateNewConnection(numericUpDown1,  richTextBox1,   Sizing.MarginSection.Top);
            sizing.CreateNewConnection(numericUpDown1,  button3,        Sizing.MarginSection.Left);
            sizing.CreateNewConnection(treeView1,       richTextBox1,   Sizing.MarginSection.Left);
            sizing.CreateNewConnection(treeView1,       Sizing.MarginSection.Right);
            sizing.CreateNewConnection(richTextBox1,    Sizing.MarginSection.Bottom);
            sizing.CreateNewConnection(button1,         Sizing.MarginSection.Bottom);
            sizing.CreateNewConnection(button2,         Sizing.MarginSection.Bottom);
            sizing.CreateNewConnection(button3,         Sizing.MarginSection.Bottom);
            sizing.CreateNewConnection(button4,         Sizing.MarginSection.Bottom);

            // MOVEFORM TO README
            MoveForm moveForm = new MoveForm(this, panel1);
            moveForm.LoadButtons(this, minBtn, maxBtn, closeBtn);

            Resizer resizer = new Resizer();
            resizer.LoadMouseHook(this);
            //resizer.LoadResizeLimits(this, new Resizer.ResizeLimits()
            //{
            //    minWidth = 600,
            //    minHeight = 500,
            //    maxWidth = 1200,
            //    maxHeight = 800
            //});
        }

        void term()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    var splitted = input.Split(' ');
                    if (splitted.Get(0) == "getloc")
                    {
                        if (splitted.Get(1) == "this")
                            richTextBox1.Text += $"this's Location: {this.Location}\r\n";
                        else 
                            richTextBox1.Text += $"{splitted.Get(1)}'s Location: {Controls.Find(splitted.Get(1), false).First().Location}\r\n";
                    }
                    if (splitted.Get(0) == "getsize")
                    {
                        if (splitted.Get(1) == "this")
                            richTextBox1.Text += $"this's Size: {this.Size}\r\n";
                        else
                            richTextBox1.Text += $"{splitted.Get(1)}'s Size: {Controls.Find(splitted.Get(1), false).First().Size}\r\n";
                    }
                    if (splitted.Get(0) == "setloc")
                    {
                        if (splitted.Get(1) == "this")
                            this.Location = new Point(Convert.ToInt32(splitted.Get(2)), Convert.ToInt32(splitted.Get(3)));
                        else
                            Controls.Find(splitted.Get(1), false).First().Location = new Point(Convert.ToInt32(splitted.Get(2)), Convert.ToInt32(splitted.Get(3)));
                    }
                    if (splitted.Get(0) == "setsize")
                    {
                        if (splitted.Get(1) == "this")
                            this.Size = new Size(Convert.ToInt32(splitted.Get(2)), Convert.ToInt32(splitted.Get(3)));
                        else
                            Controls.Find(splitted.Get(1), false).First().Size = new Size(Convert.ToInt32(splitted.Get(2)), Convert.ToInt32(splitted.Get(3)));
                    }
                }
                catch (Exception e)
                {
                    richTextBox1.Text += e.ToString() + "\r\n";
                }
            }
        }
    }

    public static class ext
    {
        public static string Get(this string[] value, int index) =>
            (value.Length > index)
                ? value[index]
                : "";
    }
}
