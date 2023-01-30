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
using System.Drawing;
using System.Windows.Forms;

namespace Responsive.Azyeb
{
    /// <summary>
    /// The `Header` class is responsible for controlling the actions of moving, minimizing, maximizing, and closing a form in WinForms.
    /// </summary>
    public class Header
    {
        #region Public methots
        public Header(ResponsivePage responsivePage, Control form)
        {
            this.ResponsivePage = responsivePage;
            frm = form;
        }

        /// <summary>
        /// A special mouse handle system allows you to move your application with a left click of the mouse.
        ///
        /// I recommend to use LoadButtons before AddMoveHandler.
        /// </summary>
        /// <param name="frm">Application as which big possible 'Form'</param>
        /// <param name="control">MenuBar item as which big possible a top dock panel</param>
        /// <param name="addChildControls">when adding delegates will add child controls too</param>
        /// <returns></returns>
        public Header AddMoveHandler(Control frm, Control control, bool addChildControls = false)
        {
            headerPanels.Add(control);
            if (control == _buttons.MinimalizeBtn || control == _buttons.MaximizeBtn || control == _buttons.CloseBtn) return this;
            
            // Inline varriables for movement
            bool arrastando = false;
            Point pontoinicial = new Point(0, 0);

            control.MouseDown += (s, e) =>
            {
                // if is left start to moving by defualt location 
                if (e.Button != MouseButtons.Left) return;
                arrastando = true;
                pontoinicial = new Point(e.X, e.Y);
            };
            control.MouseMove += (s, e) =>
            {
                // if is left click movement arrastando will be true
                if (arrastando)
                {
                    // relocate
                    Point p = frm.PointToScreen(e.Location);
                    frm.Location = new Point(
                        p.X - pontoinicial.X,
                        p.Y - pontoinicial.Y);
                    
                }
            };
            control.MouseUp += (s, e) =>
            {
                // reset varriable
                if (arrastando) arrastando = false;
            };
            if (addChildControls)
            {
                foreach (Control cntl in control.Controls)
                {
                    AddMoveHandler(frm, cntl, addChildControls);
                }
            }
            return this;
        }

        /// <summary>
        /// We have specially prepared the code for the tasks of the 3 buttons 
        /// you have to do is put the controls in the parameters. These controls 
        /// don't have to be buttons, but the controls must be.
        /// 
        /// Example of you don't want minimalize button in your form, leave null in parameter
        /// LoadButtons(this, null, maxBtn, closeBtn);
        /// </summary>
        /// <param name="frm">Application as which big possible 'Form'.</param>
        /// <param name="MinimalizeBtn">Control which minimalize when clicked.</param>
        /// <param name="MaximizeBtn">Control which normalize/maximize when clicked.</param>
        /// <param name="CloseBtn">Control which close or hide when clicked.</param>
        /// <param name="JustHideFormWhenClose">Close control just will hide form, not close **application**.</param>
        /// <returns></returns>
        public Header LoadButtons(Form frm, Control? MinimalizeBtn, Control? MaximizeBtn, Control? CloseBtn, bool JustHideFormWhenClose = false)
        {
            _buttons = (MinimalizeBtn, MaximizeBtn, CloseBtn);

            if (MinimalizeBtn is not null) MinimalizeBtn.Click += (s, e) =>
            {
                if (isSmootherWindowState)
                {
                    #region ChangeWindowState_Minimalize
                    Task.Run(() =>
                    {
                        var firstLoc = frm.Location;

                        double[] locationX = WindowStateConfiguration.GetGradiant?.Invoke(
                            frm.Location.X, 
                            (Screen.PrimaryScreen.Bounds.Width / 2) - (frm.Size.Width / 2), 
                            WindowStateConfiguration.Minimalize.times, 
                            WindowStateConfiguration.Minimalize.formula);

                        double[] locationY = WindowStateConfiguration.GetGradiant?.Invoke(
                            frm.Location.Y, 
                            Screen.PrimaryScreen.Bounds.Height,                                  
                            WindowStateConfiguration.Minimalize.times,
                            WindowStateConfiguration.Minimalize.formula);
                        ResponsivePage.ControlVisibilities.HideAllOfControls();
                        for (int i = 0; i < locationX.Length; i++)
                        {
                            frm.Location = new Point((int)locationX[i], (int)locationY[i]);

                            // Wait if is coming fast
                            if (WindowStateConfiguration.Minimalize.delay > 0) 
                                Task.Delay(WindowStateConfiguration.Minimalize.delay).Wait();
                        }
                        ResponsivePage.ControlVisibilities.ShowAllOfControls();
                        frm.Visible     = false;
                        frm.Location    = firstLoc;
                        frm.WindowState = FormWindowState.Minimized;
                        frm.Visible     = true;
                    });
                    #endregion
                }
                else frm.WindowState = FormWindowState.Minimized;
            };
            if (MaximizeBtn is not null) MaximizeBtn.Click += (s, e) =>
            {
                if (frm.WindowState == FormWindowState.Normal)
                {
                    if (isSmootherWindowState)
                    {
                        #region ChangeWindowState_NormalToMaximize
                        Task.Run(() =>
                        {
                            var firstLoc    = frm.Location;
                            var firstSize   = frm.Size;
                            double[] locationX  = WindowStateConfiguration.GetGradiant?.Invoke(frm.Location.X,  0,                                  WindowStateConfiguration.NormalToMaximize.times, WindowStateConfiguration.NormalToMaximize.formula);
                            double[] locationY  = WindowStateConfiguration.GetGradiant?.Invoke(frm.Location.Y,  0,                                  WindowStateConfiguration.NormalToMaximize.times, WindowStateConfiguration.NormalToMaximize.formula);
                            double[] width      = WindowStateConfiguration.GetGradiant?.Invoke(frm.Size.Width,  Screen.PrimaryScreen.Bounds.Width,  WindowStateConfiguration.NormalToMaximize.times, WindowStateConfiguration.NormalToMaximize.formula);
                            double[] height     = WindowStateConfiguration.GetGradiant?.Invoke(frm.Size.Height, Screen.PrimaryScreen.Bounds.Height, WindowStateConfiguration.NormalToMaximize.times, WindowStateConfiguration.NormalToMaximize.formula);
                            ResponsivePage.ControlVisibilities.HideAllOfControls();
                            for (int i = 0; i < locationX.Length; i++)
                            {
                                frm.Location    = new Point((int)locationX[i], (int)locationY[i]);
                                frm.Size        = new Size((int)width[i], (int)height[i]);

                                // Wait if is coming fast
                                if (WindowStateConfiguration.NormalToMaximize.delay > 0) 
                                    Task.Delay(WindowStateConfiguration.NormalToMaximize.delay).Wait();
                            }
                            ResponsivePage.ControlVisibilities.ShowAllOfControls();
                            frm.Visible     = false;
                            frm.Location    = firstLoc;
                            frm.Size        = firstSize;
                            frm.Visible     = true;
                            frm.WindowState = FormWindowState.Maximized;
                        });
                        #endregion
                    }
                    else frm.WindowState = FormWindowState.Maximized;
                }
                else if (frm.WindowState == FormWindowState.Maximized)
                {
                    if (isSmootherWindowState)
                    {
                        #region ChangeWindowState_MaximizeToNormal
                        Task.Run(() =>
                        {
                            frm.Visible     = false;
                            var currentLoc  = frm.Location;
                            var currentSize = frm.Size;
                            frm.WindowState = FormWindowState.Normal;
                            var goingToLoc  = frm.RestoreBounds.Location;
                            var goingToSize = frm.RestoreBounds.Size;
                            frm.Location    = currentLoc;
                            frm.Size        = currentSize;
                            frm.Visible     = true;

                            double[] locationX  = WindowStateConfiguration.GetGradiant?.Invoke(currentLoc.X,       goingToLoc.X,       WindowStateConfiguration.MaximizeToNormal.times, WindowStateConfiguration.MaximizeToNormal.formula);
                            double[] locationY  = WindowStateConfiguration.GetGradiant?.Invoke(currentLoc.Y,       goingToLoc.Y,       WindowStateConfiguration.MaximizeToNormal.times, WindowStateConfiguration.MaximizeToNormal.formula);
                            double[] width      = WindowStateConfiguration.GetGradiant?.Invoke(currentSize.Width,  goingToSize.Width,  WindowStateConfiguration.MaximizeToNormal.times, WindowStateConfiguration.MaximizeToNormal.formula);
                            double[] height     = WindowStateConfiguration.GetGradiant?.Invoke(currentSize.Height, goingToSize.Height, WindowStateConfiguration.MaximizeToNormal.times, WindowStateConfiguration.MaximizeToNormal.formula);
                            ResponsivePage.ControlVisibilities.HideAllOfControls();
                            frm.WindowState = FormWindowState.Normal;
                            for (int i = 0; i < locationX.Length; i++)
                            {
                                frm.Location = new Point((int)locationX[i], (int)locationY[i]);
                                frm.Size = new Size((int)width[i], (int)height[i]);

                                // Wait if is coming fast
                                if (WindowStateConfiguration.MaximizeToNormal.delay > 0) 
                                    Task.Delay(WindowStateConfiguration.MaximizeToNormal.delay).Wait();
                            }
                            ResponsivePage.ControlVisibilities.ShowAllOfControls();
                        });
                        #endregion
                    }
                    else frm.WindowState = FormWindowState.Normal;
                }
            };
            if (CloseBtn is not null) CloseBtn.Click += (s, e) =>
            {
                if (JustHideFormWhenClose) frm.Hide();
                else Environment.Exit(0);
            };
            return this;
        }

        /// <summary>
        /// Toggles smoother the WindowState of the form.
        /// </summary>
        public void EnableSmootherWindowState() => isSmootherWindowState = true;
        /// <summary>
        /// Disable toggles smoother the WindowState of the form.
        /// </summary>
        public void DisableSmootherWindowState() => isSmootherWindowState = false;
        /// <summary>
        /// It allows to change the transitions with all their details.
        /// </summary>
        public void ChangeSmootherWindowStateConfiguration(WindowStateConfiguration wsc) => WindowStateConfiguration = wsc;
        #endregion

        #region Private methots

        #endregion

        #region Varriables
        private Control frm;
        private List<Control> headerPanels = new List<Control>();
        private (Control? MinimalizeBtn, Control? MaximizeBtn, Control? CloseBtn) _buttons = (null, null, null);
        public int HeaderWidth
        {
            get
            {
                int hWidth = 50;
                if (_buttons.MinimalizeBtn is not null) hWidth += _buttons.MinimalizeBtn.Height;
                if (_buttons.MaximizeBtn is not null) hWidth += _buttons.MaximizeBtn.Height;
                if (_buttons.CloseBtn is not null) hWidth += _buttons.CloseBtn.Height;
                return hWidth;
            }
        }
        public int HeaderHeight
        {
            get
            {
                return headerPanels.Select(x => x.Height).Count() > 0 ? headerPanels.Select(x => x.Height).Max() : 50;
            }
        }
        public ResponsivePage ResponsivePage { get; set; }
        private bool isSmootherWindowState = true;
        public WindowStateConfiguration WindowStateConfiguration { get; set; } = new WindowStateConfiguration().init();
        #endregion
    }
}
