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

            MoveForm moveForm = new MoveForm(this, menuBar);
            moveForm.LoadButtons(minBtn, maxBtn, closeBtn);
            moveForm.AutoColorBrightnessButtons();

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
            sizing.IgnoreControlWhenSizing(menuBar);
            sizing.CreateNewConnection(checkBox1, Sizing.MarginSection.Right);
            sizing.CreateNewConnection(checkBox1, Sizing.MarginSection.Bottom);

            resizer = new Resizer();

            resizer.LoadMouseHook(this);
            resizer.LoadRoundedBorders();

            resizer.DebugItemChanged += (s, name, value) =>
            {
                watcher.AddOrUpdateItem(name, value.ToString());
            };
            resizer.GenerateResizeLimitsByMoveForm(this, moveForm);


        }
        Resizer resizer;


        private void Form1_SizeChanged(object? sender, EventArgs e)
        {
            watcher.AddOrUpdateItem("Width", Width.ToString());
            watcher.AddOrUpdateItem("Height", Height.ToString());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                resizer.EnableAutoRefresh();
            else
                resizer.DisableAutoRefresh();
        }
    }
}