using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace ResponsiveNET6.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ValueWatcher watcher;
        Size initSize;
        private void Form1_Load(object sender, EventArgs e)
        {
            watcher = new ValueWatcher();
            watcher.Show();
            initSize = this.Size;

            this.SizeChanged += Form1_SizeChanged;

            MoveForm moveForm = new MoveForm(this, panel2);
            moveForm.LoadButtons(button3, button2, button4);

            ResponsiveNET6.Sizing sizing = new Sizing(this);
            sizing.debugItemChanged += (s, name, value) =>
            {
                watcher.AddOrUpdateItem(name, value.ToString());
            };
            sizing.log += (s, log) =>
            {
                watcher.Log(log);
            };

            sizing.DEBUG_GetInitSize();
            sizing.IgnoreControlWhenSizing(panel2);
            sizing.CreateNewConnection(panel1, Sizing.MarginSection.Bottom);
            sizing.CreateNewConnection(label1, panel1, Sizing.MarginSection.Left);
            sizing.CreateNewConnection(button1, label1, Sizing.MarginSection.Top);
            sizing.CreateNewConnection(button1, panel1, Sizing.MarginSection.Left);

            Resizer resizer = new Resizer();

            resizer.LoadMouseHook(this);
            //resizer.DisableAutoRefresh();
            resizer.LoadRoundedBorders();

            resizer.debugItemChanged += (s, name, value) =>
            {
                watcher.AddOrUpdateItem(name, value.ToString());
            };
        }

        private void Form1_SizeChanged(object? sender, EventArgs e)
        {
            watcher.AddOrUpdateItem("Width", Width.ToString());
            watcher.AddOrUpdateItem("Height", Height.ToString());
        }
    }
}