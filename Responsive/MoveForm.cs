using System;
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
            if (!(MinimalizeBtn is null)) MinimalizeBtn.Click += (s, e) => MainForm.WindowState = FormWindowState.Minimized;
            if (!(SizingChangeBtn is null)) SizingChangeBtn.Click += (s, e) =>
            {
                if (smoothResize)
                {
                    if (MainForm.WindowState == FormWindowState.Maximized)
                    {
                        var (maxLoc, maxSize) = (MainForm.Location, MainForm.Size);
                        MainForm.WindowState = FormWindowState.Normal;
                        var (loc, size) = (MainForm.Location, MainForm.Size);
                        MainForm.Location = maxLoc;
                        MainForm.Size = new Size(maxSize.Width - 1, maxSize.Height - 1);
                        int times = 10;
                        double[] X = GetGradiantNormalized(maxLoc.X, loc.X, times);
                        double[] Y = GetGradiantNormalized(maxLoc.Y, loc.Y, times);
                        double[] Width = GetGradiantNormalized(maxSize.Width, size.Width, times);
                        double[] Height = GetGradiantNormalized(maxSize.Height, size.Height, times);
                        for (int i = 0;i < times; i++)
                        {
                            MainForm.Location = new Point((int)X[i], (int)Y[i]);
                            MainForm.Size = new Size((int)Width[i], (int)Height[i]);
                        }
                    }
                    else if (MainForm.WindowState == FormWindowState.Normal)
                    {
                        var (loc, size) = (MainForm.Location, MainForm.Size);
                        MainForm.WindowState = FormWindowState.Maximized;
                        var (maxLoc, maxSize) = (MainForm.Location, MainForm.Size);
                        MainForm.WindowState = FormWindowState.Normal;
                        MainForm.Location = maxLoc;
                        MainForm.Size = new Size(maxSize.Width - 1, maxSize.Height - 1);
                        int times = 10;
                        double[] X = GetGradiantSlowToFast(loc.X, maxLoc.X, times);
                        double[] Y = GetGradiantSlowToFast(loc.Y, maxLoc.Y, times);
                        double[] Width = GetGradiantSlowToFast(size.Width, maxSize.Width, times);
                        double[] Height = GetGradiantSlowToFast(size.Height, maxSize.Height, times);
                        for (int i = 0; i < times; i++)
                        {
                            MainForm.Location = new Point((int)X[i], (int)Y[i]);
                            MainForm.Size = new Size((int)Width[i], (int)Height[i]);
                        }
                        MainForm.WindowState = FormWindowState.Maximized;
                    }
                    return;
                }

                if (MainForm.WindowState == FormWindowState.Maximized) MainForm.WindowState = FormWindowState.Normal;
                else if (MainForm.WindowState == FormWindowState.Normal) MainForm.WindowState = FormWindowState.Maximized;
            };
            if (!(CloseBtn is null)) CloseBtn.Click += (s, e) =>
            {
                if (JustHideFormWhenClose) MainForm.Hide();
                else Environment.Exit(0);
            };
        }

        //Thanks to my math teacher(Aybik hocam <3)
        #region math
        private double[] GetGradiantNormalized(double start, double end, int times)
        {
            double range = end - start;
            double per = range / times;
            double[] doubles = new double[times];
            doubles[0] = start + per;
            for (int i = 1; i < times; i++)
                doubles[i] = doubles[i - 1] + per;
            return doubles;
        }

        private double[] GetGradiantSlowToFast(double start, double end, int times)
        {
            double A = times;
            double B = end - start;
            double[] doubles = new double[times+1];
            Func<double, double> formula = new Func<double, double>(X =>
            {
                // Y = Sqrt(B^2 - ((B^2 * X^2) / A^2))
                // Sqrt => 49 => 7 (Rooting)
                return Math.Sqrt(Math.Pow(B, 2) - ((Math.Pow(B, 2) * Math.Pow(X, 2)) / Math.Pow(A, 2)));
            });
            for (int i = 0; i < times+1; i++)
            {
                doubles[i] = formula(i) + start;
            }
            return doubles;
        }

        private double[] GetGradiantFastToSlow(double start, double end, int times)
        {
            double A = end - start;
            double B = times;
            double[] doubles = new double[times + 1];
            Func<double, double> formula = new Func<double, double>(X =>
            {
                // Y = Sqrt(A^2 - ((A^2 * Y^2) / B^2))
                // Sqrt => 49 => 7 (Rooting)
                return Math.Sqrt(Math.Pow(B, 2) - ((Math.Pow(B, 2) * Math.Pow(X, 2)) / Math.Pow(A, 2)));
                //return Math.Sqrt(Math.Pow(A, 2) - ((Math.Pow(A, 2) * Math.Pow(Y, 2)) / Math.Pow(B, 2)));
            });
            for (int i = 0; i < times + 1; i++)
            {
                doubles[i] = formula(i);
            }
            return doubles;
        }
#endregion

        // Private because we don't recomment while I do better one.
        private void DoSmootherResize() => smoothResize = true; 

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

        private bool arrastando = false;
        private Point pontoinicial = new Point(0, 0);
        private Control frm;
        private bool smoothResize = false;
    }
}