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
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Responsive.Azyeb
{
    public class Scaler
    {
        #region Delegates
        public event ResponsivePage.DebuggerItemChanged DebugItemChanged;

        private void ItemChanged(string name, object value)
        {
            if (DebugItemChanged is not null)
            {
                DebugItemChanged(this, name, value);
            }
        }
        #endregion
        
        #region Public methots

        public Scaler(ResponsivePage responsivePage, Control form)
        {
            this.ResponsivePage = responsivePage;
            frm = form;
        }

        public Scaler LoadResizeLimits(ResizeLimits limits)
        {
            this.resizeLimits = limits;
            return this;
        } 
        
        public Scaler LoadMouseHook()
        {
            controlsVisibilityTimer.Start();
            lastestSize = frm.Size;
            
            
            MouseHandle.AddUpRule(new System.Action(() =>
            {
                frm.Size = frm.Size;
            }));
            #region Resize
            MouseHandle.AddDownRule(delegate (Point downPosition, Point lpPoint)
            {
                if (downPosition.X > frm.Location.X + frm.Size.Width - 8 &&
                        downPosition.X < frm.Location.X + frm.Size.Width + 2 &&
                        downPosition.Y > frm.Location.Y &&
                        downPosition.Y < frm.Location.Y + frm.Size.Height - 28)
                {
                    if (!isHorizontalResize)
                    {
                        isHorizontalResize = true;
                    }
                }
                if (isHorizontalResize)
                {
                    lock (frm.Cursor) CursorHandle.TryChangeCursor(Cursors.SizeWE);

                    Size size = new Size(
                        lpPoint.X - frm.Location.X + 2,
                        frm.Size.Height
                        );

                    ItemChanged("CanIResizeWidth", this.resizeLimits.CanIResizeWidth(size));

                    if (this.resizeLimits.CanIResizeWidth(size)) frm.Size = size;

                    if (!frm.Size.Equals(lastestSize))
                    {
                        if (autoRefresh) controlsVisibilityTimer.Restart();
                        lastestSize = frm.Size;
                    }

                    if (autoRefresh)
                    {
                        if (controlsVisibilityTimer.Elapsed.TotalMilliseconds > msToRefresh)
                            this.ResponsivePage.ControlVisibilities.ShowAllOfControls();
                        else
                            this.ResponsivePage.ControlVisibilities.HideAllOfControls();
                    }
                }
            });
            MouseHandle.AddDownRule(delegate (Point downPosition, Point lpPoint)
            {
                if (downPosition.X > frm.Location.X &&
                        downPosition.X < frm.Location.X + frm.Size.Width - 28 &&
                        downPosition.Y > frm.Location.Y + frm.Size.Height - 8 &&
                        downPosition.Y < frm.Location.Y + frm.Size.Height + 2)
                {
                    if (!isVerticalResize)
                    {
                        isVerticalResize = true;
                    }
                }
                if (isVerticalResize)
                {
                    lock (frm.Cursor) CursorHandle.TryChangeCursor(Cursors.SizeNS);

                    Size size = new Size(
                        frm.Size.Width,
                        lpPoint.Y - frm.Location.Y + 2
                        );

                    ItemChanged("CanIResizeHeight", this.resizeLimits.CanIResizeHeight(size));

                    if (this.resizeLimits.CanIResizeHeight(size)) frm.Size = size;

                    if (!frm.Size.Equals(lastestSize))
                    {
                        if (autoRefresh) controlsVisibilityTimer.Restart();
                        lastestSize = frm.Size;
                    }

                    if (autoRefresh)
                    {
                        if (controlsVisibilityTimer.Elapsed.TotalMilliseconds > msToRefresh)
                            this.ResponsivePage.ControlVisibilities.ShowAllOfControls();
                        else
                            this.ResponsivePage.ControlVisibilities.HideAllOfControls();
                    }
                }
            });
            MouseHandle.AddDownRule(delegate (Point downPosition, Point lpPoint)
            {
                if (downPosition.X > frm.Location.X + frm.Size.Width - 28 &&
                        downPosition.X < frm.Location.X + frm.Size.Width &&
                        downPosition.Y > frm.Location.Y + frm.Size.Height - 28 &&
                        downPosition.Y < frm.Location.Y + frm.Size.Height)
                {
                    if (!isFullResize)
                    {
                        isFullResize = true;
                    }
                }
                if (isFullResize)
                {
                    lock (frm.Cursor) CursorHandle.TryChangeCursor(Cursors.SizeNS);

                    Size size = new Size(
                        lpPoint.X - frm.Location.X + 2,
                        lpPoint.Y - frm.Location.Y + 2
                        );

                    ItemChanged("CanIResizeWidth", this.resizeLimits.CanIResizeWidth(size));
                    ItemChanged("CanIResizeHeight", this.resizeLimits.CanIResizeHeight(size));

                    if (this.resizeLimits.CanIResizeWidth(size)) frm.Size = new Size(size.Width, frm.Size.Height);
                    if (this.resizeLimits.CanIResizeHeight(size)) frm.Size = new Size(frm.Size.Width, size.Height);

                    if (!frm.Size.Equals(lastestSize))
                    {
                        if (autoRefresh) controlsVisibilityTimer.Restart();
                        lastestSize = frm.Size;
                    }

                    if (autoRefresh)
                    {
                        if (controlsVisibilityTimer.Elapsed.TotalMilliseconds > msToRefresh)
                            this.ResponsivePage.ControlVisibilities.ShowAllOfControls();
                        else
                            this.ResponsivePage.ControlVisibilities.HideAllOfControls();
                    }
                }
            });
            #endregion
            #region ConvertCursorToOptionalCursorIn_Horizontal_Vertical_Both
            MouseHandle.AddRule(delegate (Point lpPoint)
            {
                bool isHorizontal = lpPoint.X > frm.Location.X + frm.Size.Width - 8 &&
                        lpPoint.X < frm.Location.X + frm.Size.Width + 2 &&
                        lpPoint.Y > frm.Location.Y &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height - 28;

                if (isHorizontal)
                    lock (frm.Cursor) { CursorHandle.TryChangeCursor(Cursors.SizeWE); isResizerCursor = true; }
            });
            MouseHandle.AddRule(delegate (Point lpPoint)
            {
                bool isVertical = lpPoint.X > frm.Location.X &&
                        lpPoint.X < frm.Location.X + frm.Size.Width - 28 &&
                        lpPoint.Y > frm.Location.Y + frm.Size.Height - 8 &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height + 2;

                if (isVertical)
                    lock (frm.Cursor) { CursorHandle.TryChangeCursor(Cursors.SizeNS); isResizerCursor = true; }
            });
            MouseHandle.AddRule(delegate (Point lpPoint)
            {
                bool isBoth = lpPoint.X > frm.Location.X + frm.Size.Width - 28 &&
                        lpPoint.X < frm.Location.X + frm.Size.Width &&
                        lpPoint.Y > frm.Location.Y + frm.Size.Height - 28 &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height;

                if (isBoth)
                    lock (frm.Cursor) { CursorHandle.TryChangeCursor(Cursors.SizeNWSE); isResizerCursor = true; }
            });
            #endregion
            #region IfIn_Horizontal_Vertical_Both__AND__CursorNotDefualt
            MouseHandle.AddRule(delegate (Point lpPoint)
            {
                bool isHorizontal = lpPoint.X > frm.Location.X + frm.Size.Width - 8 &&
                        lpPoint.X < frm.Location.X + frm.Size.Width + 2 &&
                        lpPoint.Y > frm.Location.Y &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height - 28;
                bool isVertical = lpPoint.X > frm.Location.X &&
                        lpPoint.X < frm.Location.X + frm.Size.Width - 28 &&
                        lpPoint.Y > frm.Location.Y + frm.Size.Height - 8 &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height + 2;
                bool isBoth = lpPoint.X > frm.Location.X + frm.Size.Width - 28 &&
                        lpPoint.X < frm.Location.X + frm.Size.Width &&
                        lpPoint.Y > frm.Location.Y + frm.Size.Height - 28 &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height;
                if (!isHorizontal && !isVertical && !isBoth && isResizerCursor)
                {
                    CursorHandle.TryChangeCursor(Cursors.Default);
                    isHorizontalResize = false;
                    isVerticalResize = false;
                    isFullResize = false;
                }

                if (controlsVisibilityTimer.Elapsed.TotalMilliseconds > msToRefresh)
                {
                    controlsVisibilityTimer.Reset();
                    this.ResponsivePage.ControlVisibilities.ShowAllOfControls();
                }

            });
            return this;

            #endregion
        }

        public Scaler DisableAutoRefresh()
        {
            autoRefresh = false;
            ItemChanged("AutoRefresh", autoRefresh);
            return this;
        }

        public Scaler EnableAutoRefresh()
        {
            autoRefresh = true;
            ItemChanged("AutoRefresh", autoRefresh);
            return this;
        }

        public Scaler UpdateAutoRefreshMillisecond(int ms)
        {
            msToRefresh = ms;
            ItemChanged("AutoRefreshMillisecond", ms);
            return this;
        }

        #endregion
        
        #region Varriables
        public ResponsivePage ResponsivePage { get; set; }
        private int msToRefresh = 100;
        public ResizeLimits resizeLimits { get; set; } = new ResizeLimits();
        Control frm { get; set; }
        bool isHorizontalResize { get; set; } = false;
        bool isVerticalResize { get; set; } = false;
        bool isFullResize { get; set; } = false;
        private bool isResizerCursor { get; set; } = false;
        private Dictionary<Control, bool> visibilities { get; set; } = new Dictionary<Control, bool>();
        public Stopwatch controlsVisibilityTimer { get; set; } = new Stopwatch();
        private Size lastestSize { get; set; }
        private bool autoRefresh { get; set; } = true;

        #endregion
    }
}