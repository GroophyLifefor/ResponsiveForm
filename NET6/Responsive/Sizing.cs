using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Responsive
{
    public class Sizing
    {
        public enum MarginSection
        {
            Left,
            Top,
            Right,
            Bottom
        }

        public Sizing(Control frm)
        {
            initWidth = frm.Width;
            initHeigth = frm.Height;
            foreach (Control cntl in frm.Controls)
            {
                sizeInits.Add(cntl, (cntl.Location, cntl.Size));
                isLocated.Add(cntl, false);
            }
            frm.SizeChanged += Frm_SizeChanged;
        }

        public void CreateNewConnection(Control mainControl, Control connectTo, MarginSection section)
        {
            var item = (connectTo, new List<(MarginSection Section, int Distance)>(new (MarginSection, int)[]
            {
                (section,
                    section == MarginSection.Top
                        ? mainControl.Location.Y - (connectTo.Location.Y + connectTo.Size.Height)
                        : section == MarginSection.Right
                            ? connectTo.Location.X - (mainControl.Location.X + mainControl.Size.Width)
                            : section == MarginSection.Left
                                ? mainControl.Location.X - (connectTo.Location.X + connectTo.Size.Width)
                                : section == MarginSection.Bottom
                                    ? connectTo.Location.Y - mainControl.Location.Y
                                    : 0)
            }));

            if (Rules.ContainsKey(mainControl)) Rules[mainControl].Add(item);
            else Rules.Add(mainControl, new List<(Control connectTo, List<(MarginSection Section, int Distance)> values)>(new[] { item }));
        }

        public void FixedWidth(Control control) => FixedWidthControls.Add(control);
        public void FixedHeight(Control control) => FixedHeightControls.Add(control);

        public void CreateNewConnection(Control mainControl, MarginSection ConnectTo)
        {
            if (RulesToForm.ContainsKey(mainControl)) RulesToForm[mainControl].Add(ConnectTo);
            else RulesToForm.Add(mainControl, new List<MarginSection>(new MarginSection[] { ConnectTo }));
        }

        private void Frm_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Form)
            {
                // Handle throw
                if (((Form)sender).WindowState == FormWindowState.Minimized) return;
            }

            double px = (double)((Control)sender).Width / (double)initWidth;
            double py = (double)((Control)sender).Height / (double)initHeigth;

            foreach (Control cntl in ((Control)sender).Controls)
            {
                if (IgnoreControls.Any(x => x == cntl)) continue;

                int newWidth = (int)(sizeInits[cntl].size.Width * px);
                int newHeigth = (int)(sizeInits[cntl].size.Height * py);

                if (customSizing.ContainsKey(cntl))
                {
                    customSizing[cntl].Invoke((((Control)sender), new Size(newWidth, newHeigth)));
                }
                else
                {
                    if (FixedWidthControls.Any(x => x == cntl)) newWidth = cntl.Size.Width;
                    if (FixedHeightControls.Any(x => x == cntl)) newHeigth = cntl.Size.Height;
                    cntl.Size = new Size(newWidth, newHeigth);
                }
            }

            foreach (var rule in RulesToForm)
            {
                reSizePerLockRule(rule.Key, rule.Value, (Control)sender);
            }

            foreach (var rule in Rules)
            {
                reLocate(rule.Key);
            }

            foreach (var key in isLocated.Keys.ToList())
            {
                isLocated[key] = false;
            }
        }

        public void IgnoreControlWhenSizing(Control cntl) => IgnoreControls.Add(cntl);

        public void AddCustomSizing(Control cntl, Action<(Control owner, Size CalculatedSize)> action) => customSizing.Add(cntl, action);

        private void reLocate(Control control)
        {
            var _rule = Rules.Where(x => x.Key == control);
            if (_rule.Count() == 0) return;
            var rule = _rule.First();
            Point newLoc = control.Location;
            if (isLocated[control]) return;

            foreach (var value in rule.Value)
            {
                isLocated[control] = true;
                if (!isLocated[value.connectTo]) reLocate(value.connectTo);
                    reLocatePerMargin(value, control.Size, ref newLoc);
            }

            control.Location = newLoc;
        }

        private void reLocatePerMargin((Control connectTo, List<(MarginSection Section, int Distance)> values) value, Size size, ref Point newLoc)
        {
            foreach (var marginRule in value.values)
            {
                if (marginRule.Section == MarginSection.Left)
                    newLoc = new Point(value.connectTo.Location.X + value.connectTo.Size.Width + marginRule.Distance, newLoc.Y);
                if (marginRule.Section == MarginSection.Top)
                    newLoc = new Point(newLoc.X, value.connectTo.Location.Y + value.connectTo.Size.Height + marginRule.Distance);
                if (marginRule.Section == MarginSection.Right)
                    newLoc = new Point(value.connectTo.Location.X - size.Width - marginRule.Distance, newLoc.Y);
                if (marginRule.Section == MarginSection.Bottom)
                    newLoc = new Point(newLoc.X, value.connectTo.Location.Y - size.Height - marginRule.Distance);
            }
        }

        private void reSizePerLockRule(Control control, List<MarginSection> sections, Control mainForm)
        {
            foreach (var section in sections)
            {
                if (section == MarginSection.Left)
                { } // Left side already locked by defualt
                if (section == MarginSection.Top)
                { } // Top side already locked by defualt
                if (section == MarginSection.Right)
                    control.Size = new Size(
                        control.Size.Width + ((mainForm.Size.Width - (control.Location.X + control.Size.Width)) - (initWidth - (sizeInits[control].loc.X + sizeInits[control].size.Width))),
                        control.Size.Height
                        );
                if (section == MarginSection.Bottom)
                    control.Size = new Size(
                        control.Size.Width,
                        control.Size.Height + ((mainForm.Size.Height - (control.Location.Y + control.Size.Height)) - (initHeigth - (sizeInits[control].loc.Y + sizeInits[control].size.Height)))
                        );
            }
        }

        private int initWidth { get; set; }
        private int initHeigth { get; set; }
        private Dictionary<Control, (Point loc,Size size)> sizeInits { get; set; } = 
            new Dictionary<Control, (Point loc, Size size)>();

        private Dictionary<Control, List<(Control connectTo, List<(MarginSection Section, int Distance)> values)>> Rules = 
            new Dictionary<Control, List<(Control connectTo, List<(MarginSection Section, int Distance)> values)>>();

        private Dictionary<Control, List<MarginSection>> RulesToForm =
            new Dictionary<Control, List<MarginSection>>();

        private Dictionary<Control, bool> isLocated =
            new Dictionary<Control, bool>();

        private List<Control> IgnoreControls { get; set; } = 
            new List<Control>();
        private List<Control> FixedWidthControls { get; set; } =
           new List<Control>();
        private List<Control> FixedHeightControls { get; set; } =
           new List<Control>();

        private Dictionary<Control, Action<(Control owner, Size CalculatedSize)>> customSizing { get; set; } = 
            new Dictionary<Control, Action<(Control owner, Size CalculatedSize)>>();
    }
}
