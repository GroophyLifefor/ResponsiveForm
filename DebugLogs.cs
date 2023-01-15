using H.Hooks;
using Responsive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponsiveForm.Test
{
    public partial class DebugLogs : Form
    {
        bool visible = true;
        public DebugLogs()
        {
            InitializeComponent();
            this.Show();
            Program.logs = this;
            KeyboardHandle.Init();
            KeyboardHandle.addDownRule(new Action<KeyboardEventArgs>(args =>
            {
                if (args.Keys.IsCtrl && args.CurrentKey == Key.D)
                {
                    if (visible) {
                        this.Hide();
                        visible = false;
                    } else {
                        this.Show();
                        visible = true;
                    }
                }
            }));

            MoveForm moveForm = new MoveForm(this, Menubar);

            Resizer resizer = new Resizer();
            resizer.LoadMouseHook(this);
        }

        public static void AddOrUpdateItem(string key, string value)
        {
            int indexOf = -1;
            for (int i = 0;i < Program.logs.listViewTable.Items.Count;i++)
            {
                if (Program.logs.listViewTable.Items[i].Text == key) { indexOf = i; break; }
            }


            if (indexOf != -1)
                Program.logs.listViewTable.Items[indexOf].SubItems[1].Text = value;
            else
                Program.logs.listViewTable.Items.Add(new ListViewItem(new String[] { key, value }));
        }
    }
}
