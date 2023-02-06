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
#pragma warning disable CS0642 // Possible mistaken empty statement
#pragma warning disable CA1416 // Validate platform compatibility
using Responsive.Azyeb.Mermaid;
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

        [DllImport("Gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Set the border radius of the controls.
        /// 
        /// Credit to SOF@Meredith and SOF@Phi
        /// </summary>
        /// <param name="control">A Control object representing the control for which the border radius will be set.</param>
        /// <param name="ellipseWidth">An integer value representing the width of the ellipse used to create the rounded border. (default value is 20)</param>
        /// <param name="ellipseHeight">An integer value representing the height of the ellipse used to create the rounded border. (default value is 20)</param>
        /// <param name="useDeleteObject">A boolean value indicating whether the function should call the DeleteObject method after creating the rounded border. (default value is true)</param>
        public void LoadRoundedBorders(Control control, int ellipseWidth = 20, int ellipseHeight = 20, bool useDeleteObject = true)
        {
            MermaidBuilder.AddConnection("ResponsivePage", "ResponsivePage", $"LoadRoundedBorders {control.Name}, {ellipseWidth}, {ellipseHeight}");
            control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, ellipseWidth, ellipseHeight));
            control.SizeChanged += (_, __) =>
                {
                    if (useDeleteObject)
                    {
                        IntPtr handle = CreateRoundRectRgn(0, 0, control.Width, control.Height, ellipseWidth, ellipseHeight);
                        if (handle == IntPtr.Zero)
                            ; // error with CreateRoundRectRgn
                        control.Region = Region.FromHrgn(handle);
                        DeleteObject(handle);
                    }
                    else
                    {
                        control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, ellipseWidth, ellipseHeight));
                    }
                };
        }
    }
}