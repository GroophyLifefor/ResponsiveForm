using System.Drawing;
using System.Windows.Forms;

namespace Responsive
{
    public class MoveForm
    {
        public MoveForm(Control form, Control panel)
        {
            frm = form;
            panel.MouseDown += MouseDown;
            panel.MouseMove += MouseMove;
            panel.MouseUp += MouseUp;
        }

        public void LoadButtons(Form MainForm, Control MinimalizeBtn, Control SizingChangeBtn, Control CloseBtn, bool JustHideFormWhenClose = false)
        {
            MinimalizeBtn.Click += (s, e) => MainForm.WindowState = FormWindowState.Minimized;
            SizingChangeBtn.Click += (s, e) =>
            {
                if (MainForm.WindowState == FormWindowState.Maximized) MainForm.WindowState = FormWindowState.Normal;
                else if (MainForm.WindowState == FormWindowState.Normal) MainForm.WindowState = FormWindowState.Maximized;
            };
            CloseBtn.Click += (s, e) =>
            {
                if (JustHideFormWhenClose) MainForm.Hide();
                else MainForm.Close();
            };
        }

        private bool arrastando = false;
        private Point pontoinicial = new Point(0, 0);
        private Control frm;

        private void MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            arrastando = true;
            pontoinicial = new Point(e.X, e.Y);
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastando)
            {
                Point p = frm.PointToScreen(e.Location);
                frm.Location = new Point(
                    p.X - this.pontoinicial.X,
                    p.Y - this.pontoinicial.Y);
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            if (arrastando) arrastando = false;
        }
    }
}