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
using System.Windows.Forms;

namespace Responsive.Azyeb
{
    public class ResponsivePage
    {
        #region Delegates
        public delegate void DebuggerItemChanged(object sender, string name, object value);
        #endregion
        #region Static methots
        public static Header CreateNewStaticHeader(Control form, ResponsivePage responsivePage) => new Header(responsivePage, form);
        public static Header CreateNewStaticHeader(Control form, ResponsivePage responsivePage, out Header header)
        {
            header = new Header(responsivePage, form);
            return header;
        }
        #endregion

        #region Public methots
        public Header CreateNewHeader(Control form, ResponsivePage responsivePage)
        {
            MermaidBuilder.AddConnection("ResponsivePage", "Header", "New Header");
            return new Header(responsivePage, form);
        }
        public Header CreateNewHeader(Control form, ResponsivePage responsivePage, out Header header)
        {
            header = new Header(responsivePage, form);
            MermaidBuilder.AddConnection("ResponsivePage", "Header", "New Header");
            return header;
        }
        public Scaler CreateNewScaler(Control form, ResponsivePage responsivePage)
        {
            MermaidBuilder.AddConnection("ResponsivePage", "Scaler", "New Scaler");
            return new Scaler(responsivePage, form);
        }
        public Scaler CreateNewScaler(Control form, ResponsivePage responsivePage, out Scaler scaler)
        {
            scaler = new Scaler(responsivePage, form);
            MermaidBuilder.AddConnection("ResponsivePage", "Scaler", "New Scaler");
            return scaler;
        }

        #endregion

        public ResponsivePage(Control form)
        {
            frm = form;
            this.ControlVisibilities = new ControlVisibilities(frm);
        }

        #region Varriables
        Control frm { get; set; }
        internal ControlVisibilities ControlVisibilities { get; set; }
        public ControlCustomBorder ControlCustomBorder { get; set; } = new ControlCustomBorder();

        #endregion
    }
}
