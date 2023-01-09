using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Responsive;

namespace ResponsiveForm.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Responsive.Sizing sizing = new Sizing(this);

            sizing.CreateNewConnection(button2, button1, Sizing.MarginSection.Left);
            sizing.CreateNewConnection(button3, button1, Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button4, button1, Sizing.MarginSection.Top);
            
            sizing.CreateNewConnection(button4, button3, Sizing.MarginSection.Left);
            sizing.CreateNewConnection(button5, button3, Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button6, button3, Sizing.MarginSection.Top);
            
            sizing.CreateNewConnection(button6, button5, Sizing.MarginSection.Left);
            sizing.CreateNewConnection(button8, button7, Sizing.MarginSection.Left);
            sizing.CreateNewConnection(button7, button5, Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button8, button5, Sizing.MarginSection.Top);
            
            sizing.IgnoreControlWhenSizing(Menubar);
            sizing.IgnoreControlWhenSizing(miniToolStrip);

            Resizer resizer = new Resizer();
            //resizer.LoadResizeLimits(this, new Resizer.ResizeLimits()
            //{
            //    minWidth = 400,
            //    minHeight = 400,
            //    maxWidth = 800,
            //    maxHeight = 800
            //});

            MoveForm moveForm = new MoveForm(this, Menubar);
            moveForm.LoadButtons(this, MinimalizeBtn, SizingChangeBtn, CloseBtn);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Blue)),
                new Point(button2.Location.X + button2.Size.Width, button2.Location.Y),
                new Point(this.Size.Width, button2.Location.Y)
                );

            label1.Text = "Distance between button2's end and Form's max width: " + (this.Size.Width - (button2.Location.X + button2.Size.Width)).ToString();
        }
    }
}