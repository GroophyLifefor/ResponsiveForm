/*
   Copyright 2022-2023 Murat K.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using Responsive.Azyeb.Mermaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Responsive.Azyeb
{
    internal class ControlVisibilities
    {
        public ControlVisibilities(Control control)
        {
            frm = control;
            for (int i = 0; i < frm.Controls.Count; i++)
            {
                if (frm.Controls[i] is Panel)
                {
                    // Don't add panel to list
                    for (int p = 0; p < frm.Controls[i].Controls.Count; p++)
                    {
                        visibilities.Add(frm.Controls[i].Controls[p], frm.Controls[i].Controls[p].Visible);
                    }
                }
                else visibilities.Add(frm.Controls[i], frm.Controls[i].Visible);
            }
        }

        /// <summary>
        /// Hides all of controls out of panels(panel childControls too will be hidden).
        /// </summary>
        public void HideAllOfControls()
        {
            if (alreadyHidden) return;

            for (int i = 0; i < visibilities.Count; i++)
                visibilities[visibilities.ElementAt(i).Key] = visibilities.ElementAt(i).Key.Visible;

            for (int i = 0; i < visibilities.Count; i++)
            {
                if (visibilities.ElementAt(i).Key.InvokeRequired)
                {
                    visibilities.ElementAt(i).Key.Invoke(new MethodInvoker(() => visibilities.ElementAt(i).Key.Visible = false));
                }
                else visibilities.ElementAt(i).Key.Visible = false;
            }
            alreadyHidden = true;
            alreadyShown = false;
            frm.SuspendLayout();
            MermaidBuilder.AddConnection("ResponsivePage", "ResponsivePage", $"Hide controls");
        }

        /// <summary>
        /// Show all of controls which will be hidden.
        /// </summary>
        public void ShowAllOfControls()
        {
            if (alreadyShown) return;
            frm.ResumeLayout();
            for (int i = 0; i < visibilities.Count; i++)
            {
                if (visibilities.ElementAt(i).Key.InvokeRequired)
                {
                    visibilities.ElementAt(i).Key.Invoke(new MethodInvoker(() => visibilities.ElementAt(i).Key.Visible = visibilities[visibilities.ElementAt(i).Key]));
                }
                else visibilities.ElementAt(i).Key.Visible = visibilities[visibilities.ElementAt(i).Key];
            }
            alreadyHidden = false;
            alreadyShown = true;
            MermaidBuilder.AddConnection("ResponsivePage", "ResponsivePage", $"Show controls which will hidden by ResponsivePage");
        }

        public void PushStopwatchMillisecond(double ms)
            => query.Push(ms);

        public List<double> GetQuery() => query.GetValues();

        public Control frm { get; set; }
        private Dictionary<Control, bool> visibilities { get; set; } = new Dictionary<Control, bool>();
        private bool alreadyHidden { get; set; } = false;
        private bool alreadyShown { get; set; } = true;
        private Query<double> query { get; set; } = new Query<double>(50);
    }
}
