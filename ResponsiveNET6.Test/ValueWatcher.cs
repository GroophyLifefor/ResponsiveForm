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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ResponsiveNET6.Test
{
    public partial class ValueWatcher : Form
    {
        public ValueWatcher()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void ValueWatcher_Load(object sender, EventArgs e)
        {
            KeyboardHandle.Init();
            bool visible = true;
            KeyboardHandle.addDownRule(new Action<KeyboardEventArgs>(args =>
            {
                if (args.Keys.IsCtrl && args.CurrentKey == Key.D)
                {
                    if (visible)
                    {
                        this.Hide();
                        visible = false;
                    }
                    else
                    {
                        this.Show();
                        visible = true;
                    }
                }
            }));

            MoveForm moveForm = new MoveForm(this, panel1);
            moveForm.LoadButtons(button3, button2, button1, true);

            Responsive.Resizer resizer = new Responsive.Resizer();
            resizer.LoadMouseHook(this);
        }

        public void AddOrUpdateItem(string key, object value)
        {
            int indexOf = -1;
            for (int i = 0; i < listViewTable.Items.Count; i++)
            {
                if (listViewTable.Items[i].Text == key) { indexOf = i; break; }
            }


            if (indexOf != -1)
                listViewTable.Items[indexOf].SubItems[1].Text = value.ToString();
            else
                listViewTable.Items.Add(new ListViewItem(new String[] { key, value.ToString() }));
        }

        public void Log(string text)
        {
            richTextBox1.Text += text + "\r\n";
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
    }
}
