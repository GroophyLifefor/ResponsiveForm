using System.Drawing;
using System.Windows.Forms;

namespace ResponsiveNET6
{
    public delegate void DebugItemChanged(object sender, string name, object value);
    public delegate void Log(object sender, string log);
    public class Sizing
    {
        public event DebugItemChanged debugItemChanged;
        public event Log log;

        private void ItemChanged(string name, object value)
        {
            if (debugItemChanged != null)
            {
                debugItemChanged(this, name, value);
            }
        }

        private void Log(string logMessage)
        {
            if (log != null)
            {
                if (logQuery.Count > 0)
                {
                    foreach (var logm in logQuery)
                    {
                        log(this, logm);
                    }
                    logQuery.Clear();
                }
                log(this, logMessage);
            }
            else
            {
                logQuery.Add(logMessage);
            }
        }

        public enum MarginSection
        {
            Left,
            Top,
            Right,
            Bottom
        }

        public enum As
        {
            Location,
            Size
        }

        public Sizing(Control frm, int MenuBarHeight = 39)
        {
            MoveForm moveForm = MoveForm.GetEmptyMoveForm();
            if (MoveForm.isThisControlHasMoveForm(frm, out moveForm))
            {
                MenuBarHeight = moveForm.panel.Height;
            }
            menuBarHeight = MenuBarHeight;
            initWidth = frm.Width;
            initHeigth = frm.Height - menuBarHeight;

            foreach (Control cntl in frm.Controls)
            {
                sizeInits.Add(cntl, (cntl.Location, cntl.Size));
                isLocated.Add(cntl, false);
            }
            frm.SizeChanged += Frm_SizeChanged;
        }

        public Sizing(Control frm, Control moveFormPanel)
        {
            menuBarHeight = moveFormPanel.Height;
            initWidth = frm.Width;
            initHeigth = frm.Height - menuBarHeight;
            foreach (Control cntl in frm.Controls)
            {
                sizeInits.Add(cntl, (cntl.Location, cntl.Size));
                isLocated.Add(cntl, false);
            }
            frm.SizeChanged += Frm_SizeChanged;
        }

        public Sizing(Control frm, MoveForm moveForm)
        {
            menuBarHeight = moveForm.panel.Height;
            initWidth = frm.Width;
            initHeigth = frm.Height - menuBarHeight;
            foreach (Control cntl in frm.Controls)
            {
                sizeInits.Add(cntl, (cntl.Location, cntl.Size));
                isLocated.Add(cntl, false);
            }
            frm.SizeChanged += Frm_SizeChanged;
        }

        public void DEBUG_GetInitSize()
        {
            ItemChanged("initWidth", initWidth);
            ItemChanged("initHeigth", initHeigth);
        }

        public void CreateNewConnection(Control mainControl, Control connectTo, MarginSection section)
        {
            if (section == MarginSection.Left || section == MarginSection.Top)
                CreateNewConnection(mainControl, connectTo, section, As.Location);
            else
                CreateNewConnection(mainControl, connectTo, section, As.Size);
        }

        public void CreateNewConnection(Control mainControl, Control connectTo, MarginSection section, As _as)
        {
            var item = (connectTo, new List<(MarginSection Section, As _as, int Distance)>(new (MarginSection, As, int)[]
            {
                (section, _as,
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

            Log($"New connection from '{mainControl.Name}' to '{connectTo.Name}' as section '{section}'");

            if (Rules.ContainsKey(mainControl)) Rules[mainControl].Add(item);
            else Rules.Add(mainControl, new List<(Control connectTo, List<(MarginSection Section, As _as, int Distance)> values)>(new[] { item }));
        }

        public void FixedWidth(Control control)
        {
            FixedWidthControls.Add(control);
            Log($"Sizing does not effect {control.Name}'s Width value.");

        }
        public void FixedHeight(Control control)
        {
            FixedHeightControls.Add(control);
            Log($"Sizing does not effect {control.Name}'s Height value.");
        }

        public void CreateNewConnection(Control mainControl, MarginSection ConnectTo, As _as)
        {
            Log($"New connection from '{mainControl.Name}' to 'Form' as section '{ConnectTo}'");

            if (RulesToForm.ContainsKey(mainControl)) RulesToForm[mainControl].Add((ConnectTo, _as));
            else RulesToForm.Add(mainControl, new List<(MarginSection, As)>(new (MarginSection, As)[] {(ConnectTo, _as)}));
        }

        private void Frm_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Form)
            {
                // Handle throw
                if (((Form)sender).WindowState == FormWindowState.Minimized) return;
            }

            double px = (double)((Control)sender).Width / (double)initWidth;
            double py = (double)(((Control)sender).Height - menuBarHeight) / (double)initHeigth;

            ItemChanged("PX", px);
            ItemChanged("PY", py);

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
                ItemChanged($"{cntl.Name}'s newSize", cntl.Size.ToString());
            }

            foreach (var rule in RulesToForm)
            {
                int distance = -1;
                reSizePerLockRule(rule.Key, rule.Value, (Control)sender, ref distance);
                Log($"Sizing '{rule.Key.Name}' as Form's ({string.Join(',', rule.Value)}) section. Distance: {distance}");
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
            Size newSize = control.Size;
            if (isLocated[control]) return;

            foreach (var value in rule.Value)
            {
                isLocated[control] = true;
                if (!isLocated[value.connectTo])
                {
                    reLocate(value.connectTo);
                }
                reLocatePerMargin(value, control, ref newLoc, ref newSize);
                ItemChanged($"{control.Name}'s newLocation", control.Location.ToString());
            }

            control.Location = newLoc;
            control.Size = newSize;
        }

        private void reLocatePerMargin((Control connectTo, List<(MarginSection Section, As _as, int Distance)> values) value, Control control, ref Point newLoc, ref Size newSize)
        {
            foreach (var marginRule in value.values)
            {
                if (marginRule.Section == MarginSection.Left)
                {
                    if (marginRule._as == As.Size)
                        throw new Exception("Margin rule which left, 'as' value can not be Size, try Location.");
                    newLoc = new Point(value.connectTo.Location.X + value.connectTo.Size.Width + marginRule.Distance, newLoc.Y);
                }
                if (marginRule.Section == MarginSection.Top)
                {
                    if (marginRule._as == As.Size)
                        throw new Exception("Margin rule which top, 'as' value can not be Size, try Location.");
                    newLoc = new Point(newLoc.X, value.connectTo.Location.Y + value.connectTo.Size.Height + marginRule.Distance);
                }
                if (marginRule.Section == MarginSection.Right)
                {
                    if (marginRule._as == As.Location)  newLoc = new Point(value.connectTo.Location.X - control.Width - marginRule.Distance, newLoc.Y);
                    else if (marginRule._as == As.Size) newSize = new Size(value.connectTo.Location.X - (marginRule.Distance + control.Location.X), value.connectTo.Size.Height);
                }
                if (marginRule.Section == MarginSection.Bottom)
                {
                    if (marginRule._as == As.Location)  newLoc = new Point(newLoc.X, value.connectTo.Location.Y - control.Height - marginRule.Distance);
                    else if (marginRule._as == As.Size) newSize = new Size(value.connectTo.Size.Width, value.connectTo.Location.Y - (marginRule.Distance + control.Location.Y));
                }
            }
        }

        private void reSizePerLockRule(Control control, List<(MarginSection margin, As _as)> sections, Control mainForm, ref int distance)
        {
            foreach (var section in sections)
            {
                if (section.margin == MarginSection.Left)
                {
                    distance = sizeInits[control].loc.X;
                    control.Location = new Point(
                        distance,
                        control.Location.Y
                        );
                }
                if (section.margin == MarginSection.Top)
                {
                    distance = sizeInits[control].loc.Y;
                    control.Location = new Point(
                        control.Location.X,
                        distance
                        );
                }
                if (section.margin == MarginSection.Right)
                {
                    distance = initWidth - (sizeInits[control].loc.X + sizeInits[control].size.Width);
                    if (section._as == As.Location)
                        control.Location = new Point(
                            mainForm.Width - (distance + control.Size.Width),
                            control.Location.Y
                            );
                    else //Size
                        control.Size = new Size(
                        mainForm.Width - (distance + control.Location.X),
                        control.Size.Height
                        );
                }
                if (section.margin == MarginSection.Bottom)
                {
                    distance = (initHeigth + menuBarHeight) - (sizeInits[control].loc.Y + sizeInits[control].size.Height);
                    if (section._as == As.Location)
                        control.Location = new Point(
                        control.Location.X,
                        mainForm.Height - (distance + control.Size.Height)
                        );
                    else //Size
                        control.Size = new Size(
                        control.Size.Width,
                        mainForm.Height - (distance + control.Location.Y)
                        );
                }
                ItemChanged($"{control.Name}'s newLocation", control.Location.ToString());
            }
        }

        private int initWidth { get; set; }
        private int initHeigth { get; set; }
        private int menuBarHeight { get; set; }
        private Dictionary<Control, (Point loc, Size size)> sizeInits { get; set; } =
            new Dictionary<Control, (Point loc, Size size)>();

        private Dictionary<Control, List<(Control connectTo, List<(MarginSection Section, As _as, int Distance)> values)>> Rules =
            new Dictionary<Control, List<(Control connectTo, List<(MarginSection Section, As _as, int Distance)> values)>>();

        private Dictionary<Control, List<(MarginSection, As)>> RulesToForm =
            new Dictionary<Control, List<(MarginSection, As)>>();

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

        private List<string> logQuery = new List<string>();
    }
}
