using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ResponsiveNET6
{
    public class MoveForm
    {
        public static bool isThisControlHasMoveForm(Control form, out MoveForm panel)
        {
            var has = isMoveFormed.Where(x => x.form == form);
            if (has.Count() > 0)
            {
                panel = has.First().moveForm;
                return true;
            }

            panel = null;
            return false;
        }

        public static MoveForm GetEmptyMoveForm() => new MoveForm();
        private MoveForm() { }

        public MoveForm(Control form, Control Panel)
        {
            frm = form;
            panel = Panel;
            panel.MouseDown += MouseDown;
            panel.MouseMove += MouseMove;
            panel.MouseUp += MouseUp;
            isMoveFormed.Add((form, this));
        }

        public bool LoadButtons(Control mainForm, Control MinimalizeBtn, Control SizingChangeBtn, Control CloseBtn, bool JustHideFormWhenClose = false) =>
            LoadButtons(MinimalizeBtn, SizingChangeBtn, CloseBtn, JustHideFormWhenClose);

        public bool LoadButtons(Control MinimalizeBtn, Control SizingChangeBtn, Control CloseBtn, bool JustHideFormWhenClose = false)
        {
            if (frm is not Form) return false;
            buttons = (true, MinimalizeBtn, SizingChangeBtn, CloseBtn, JustHideFormWhenClose);
            var _frm = frm as Form;
            if (!(MinimalizeBtn is null)) MinimalizeBtn.Click += (s, e) => _frm.WindowState = FormWindowState.Minimized;
            if (!(SizingChangeBtn is null)) SizingChangeBtn.Click += (s, e) =>
            {
                if (smoothResize)
                {
                    if (_frm.WindowState == FormWindowState.Maximized)
                    {
                        var (maxLoc, maxSize) = (_frm.Location, _frm.Size);
                        _frm.WindowState = FormWindowState.Normal;
                        var (loc, size) = (_frm.Location, _frm.Size);
                        _frm.Location = maxLoc;
                        _frm.Size = new Size(maxSize.Width - 1, maxSize.Height - 1);
                        int times = 10;
                        double[] X = GetGradiantNormalized(maxLoc.X, loc.X, times);
                        double[] Y = GetGradiantNormalized(maxLoc.Y, loc.Y, times);
                        double[] Width = GetGradiantNormalized(maxSize.Width, size.Width, times);
                        double[] Height = GetGradiantNormalized(maxSize.Height, size.Height, times);
                        for (int i = 0; i < times; i++)
                        {
                            _frm.Location = new Point((int)X[i], (int)Y[i]);
                            _frm.Size = new Size((int)Width[i], (int)Height[i]);
                        }
                    }
                    else if (_frm.WindowState == FormWindowState.Normal)
                    {
                        var (loc, size) = (_frm.Location, _frm.Size);
                        _frm.WindowState = FormWindowState.Maximized;
                        var (maxLoc, maxSize) = (_frm.Location, _frm.Size);
                        _frm.WindowState = FormWindowState.Normal;
                        _frm.Location = maxLoc;
                        _frm.Size = new Size(maxSize.Width - 1, maxSize.Height - 1);
                        int times = 10;
                        double[] X = GetGradiantSlowToFast(loc.X, maxLoc.X, times);
                        double[] Y = GetGradiantSlowToFast(loc.Y, maxLoc.Y, times);
                        double[] Width = GetGradiantSlowToFast(size.Width, maxSize.Width, times);
                        double[] Height = GetGradiantSlowToFast(size.Height, maxSize.Height, times);
                        for (int i = 0; i < times; i++)
                        {
                            _frm.Location = new Point((int)X[i], (int)Y[i]);
                            _frm.Size = new Size((int)Width[i], (int)Height[i]);
                        }
                        _frm.WindowState = FormWindowState.Maximized;
                    }
                    return;
                }

                if (_frm.WindowState == FormWindowState.Maximized) _frm.WindowState = FormWindowState.Normal;
                else if (_frm.WindowState == FormWindowState.Normal) _frm.WindowState = FormWindowState.Maximized;
            };
            if (!(CloseBtn is null)) CloseBtn.Click += (s, e) =>
            {
                if (JustHideFormWhenClose) _frm.Hide();
                else Environment.Exit(0);
            };
            return true;
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
            double[] doubles = new double[times + 1];
            Func<double, double> formula = new Func<double, double>(X =>
            {
                // Y = Sqrt(B^2 - ((B^2 * X^2) / A^2))
                // Sqrt => 49 => 7 (Rooting)
                return Math.Sqrt(Math.Pow(B, 2) - ((Math.Pow(B, 2) * Math.Pow(X, 2)) / Math.Pow(A, 2)));
            });
            for (int i = 0; i < times + 1; i++)
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

        public bool AutoColorBrightnessButtons(float brightness = 0.3f)
        {
            if (!buttons.isButtonsLoaded) return false;
            ColorizeWhenHover(buttons.minBtn, brightness);
            ColorizeWhenHover(buttons.maxBtn, brightness);
            ColorizeWhenHover(buttons.closeBtn, brightness);
            return true;
        }

        public static void ColorizePanelWhenHover(Panel control, float correctionFactor = 0.3f)
        {
            var controlDefualt = control.BackColor;
            var controlHoverColor = ChangeColorBrightness(control.BackColor, correctionFactor);
            for (int i = 0;i < control.Controls.Count;i++)
            {
                control.Controls[i].MouseHover += (_, __) =>
                {
                    control.BackColor = controlHoverColor;
                };
                control.Controls[i].MouseLeave += (_, __) =>
                {
                    control.BackColor = controlDefualt;
                };
            }
            control.MouseHover += (_, __) =>
            {
                control.BackColor = controlHoverColor;
            };
            control.MouseLeave += (_, __) =>
            {
                control.BackColor = controlDefualt;
            };
        }

        public static void ColorizeWhenHover(Control control, float correctionFactor = 0.3f)
        {
            var controlDefualt = control.BackColor;
            var controlHoverColor = ChangeColorBrightness(control.BackColor, correctionFactor);
            control.MouseHover += (_, __) =>
            {
                control.BackColor = controlHoverColor;
            };
            control.MouseLeave += (_, __) =>
            {
                control.BackColor = controlDefualt;
            };
        }

        private static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

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
        public Control panel;
        public (bool isButtonsLoaded, Control minBtn, Control maxBtn, Control closeBtn, bool JustHideFormWhenClose) buttons { get; set; }
        = (false, null, null, null, false);
        private static List<(Control form, MoveForm moveForm)> isMoveFormed = new List<(Control, MoveForm)>();
    }
}