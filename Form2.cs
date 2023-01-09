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
            //Sizing sizing = new Sizing(this);
            //sizing.CreateNewConnection(richTextBox1, menuStrip1, BetterSizing.MarginSection.Top);
            //sizing.CreateNewConnection(button1, richTextBox1, BetterSizing.MarginSection.Top);
            //sizing.CreateNewConnection(button2, button1, BetterSizing.MarginSection.Top);
            //sizing.CreateNewConnection(button3, button2, BetterSizing.MarginSection.Top);
            //sizing.CreateNewConnection(button4, richTextBox1, BetterSizing.MarginSection.Top);
            //sizing.CreateNewConnection(button4, button1, BetterSizing.MarginSection.Left);
            //sizing.CreateNewConnection(numericUpDown1, richTextBox1, BetterSizing.MarginSection.Top);
            //sizing.CreateNewConnection(numericUpDown1, button3, BetterSizing.MarginSection.Left);
            //sizing.CreateNewConnection(treeView1, richTextBox1, BetterSizing.MarginSection.Left);
            //sizing.CreateNewConnection(treeView1, BetterSizing.MarginSection.Right);
            //sizing.CreateNewConnection(richTextBox1, BetterSizing.MarginSection.Bottom);
            //sizing.CreateNewConnection(button1, BetterSizing.MarginSection.Bottom);
            //sizing.CreateNewConnection(button2, BetterSizing.MarginSection.Bottom);
            //sizing.CreateNewConnection(button3, BetterSizing.MarginSection.Bottom);
            //sizing.CreateNewConnection(button4, BetterSizing.MarginSection.Bottom);
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
