using Responsive.Azyeb;
using Responsive.Azyeb.Mermaid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponsiveNET6.Test
{
    public partial class SampleApp : Form
    {
        public SampleApp()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void SampleApp_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            textBox1.Text = this.Width.ToString();
            MoveForm moveForm = new MoveForm(this, menuBar);
            Resizer resizer = new Resizer();
            resizer.LoadMouseHook(this);

            ResponsivePage responsivePage = new ResponsivePage(this);
            responsivePage.ControlCustomBorder.LoadRoundedBorders(this);

            int Adistance = A.Location.X;
            int ABdistance = B.Location.X - (A.Location.X + A.Size.Width);
            int initWidth = this.Width;

            this.SizeChanged += (_, __) =>
            {
                textBox1.Text = this.Width.ToString();
                float px = (float)this.Width / (float)initWidth;
                int newABDistance = (int)(ABdistance * px);
                int newADistance = (int)(Adistance * px);
                
                A.Location = new Point(
                    newADistance,
                    A.Location.Y
                    );

                B.Location = new Point(
                    A.Location.X + A.Size.Width + newABDistance,
                    B.Location.Y
                    );

                Graphics g = this.CreateGraphics();
                Pen pen = new Pen(new SolidBrush(Color.Yellow), 5);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLine(pen,
                    new Point(
                        A.Location.X + A.Size.Width,
                        A.Location.Y + A.Size.Height / 2
                        ),
                    new Point(
                        B.Location.X,
                        A.Location.Y + A.Size.Height / 2
                        ));

                label1.Text = $"Distance: {ABdistance}, New Distance: {(B.Location.X - (A.Location.X + A.Size.Width)).ToString()}";
            };
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Width = int.Parse(textBox1.Text);
            }
        }
    }
}
