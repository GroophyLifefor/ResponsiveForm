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
#pragma warning disable CA1416
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Responsive.Azyeb
{
    public class ControlCustomBorder
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        
        public void LoadRoundedBorders(Control control, int ellipseWidth = 20, int ellipseHeight = 20)
        {
            control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, ellipseWidth, ellipseHeight));
            control.SizeChanged += (_, __) => 
                control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, ellipseWidth, ellipseHeight));
        }
    }
}