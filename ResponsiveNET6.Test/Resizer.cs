using ResponsiveNET6.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static ScintillaNET.Style;

namespace ResponsiveNET6
{
    public class Resizer
    {
        public event DebugItemChanged DebugItemChanged;

        private void ItemChanged(string name, object value)
        {
            if (DebugItemChanged != null)
            {
                DebugItemChanged(this, name, value);
            }
        }


        public class ResizeLimits
        {
            public int minWidth { get; set; } = -1;
            public int maxWidth { get; set; } = -1;
            public int minHeight { get; set; } = -1;
            public int maxHeight { get; set; } = -1;

            public bool isMinWidth { get { return minWidth != -1; } }
            public bool isMaxWidth { get { return maxWidth != -1; } }
            public bool isMinHeight { get { return minHeight != -1; } }
            public bool isMaxHeight { get { return maxHeight != -1; } }

            public bool CanIResizeWidth(Size size) =>
                !(
                isMinWidth && size.Width < minWidth ||
                isMaxWidth && size.Width > maxWidth
                );

            public bool CanIResizeHeight(Size size) =>
                !(
                isMinHeight && size.Height < minHeight ||
                isMaxHeight && size.Height > maxHeight
                );
        }

        public void GenerateResizeLimitsByMoveForm(MoveForm moveForm)
        {
            var limits = new ResizeLimits();
            if (!(moveForm.buttons.minBtn is null))
            {
                limits.minWidth = moveForm.buttons.minBtn.Width + moveForm.buttons.maxBtn.Width + moveForm.buttons.closeBtn.Width + 50;
            }
            limits.minHeight = moveForm.panel.Height + 50;
            LoadResizeLimits(limits);
        }

        public void LoadResizeLimits(out ResizeLimits limits, int minWidth = -1, int maxWidth = -1, int minHeight = -1, int maxHeight = -1)
        {
            ResizeLimits resizeLimits = new ResizeLimits()
            {
                minWidth = minWidth,
                maxWidth = maxWidth,
                minHeight = minHeight,
                maxHeight = maxHeight
            };
            LoadResizeLimits(resizeLimits);
            limits = resizeLimits;
        }
        public void LoadResizeLimits(int minWidth = -1, int maxWidth = -1, int minHeight = -1, int maxHeight = -1)
        {
            ResizeLimits resizeLimits = new ResizeLimits()
            {
                minWidth = minWidth,
                maxWidth = maxWidth,
                minHeight = minHeight,
                maxHeight = maxHeight
            };
            LoadResizeLimits(resizeLimits);
        }
        public void LoadResizeLimits(ResizeLimits limits) => resizeLimits = limits;

        public void LoadMouseHook(Control mw)                   => LoadMouseHook(mw, new ResizeLimits(), 100);
        public void LoadMouseHook(Control mw, int msToRefresh)  => LoadMouseHook(mw, new ResizeLimits(), msToRefresh);
        public void LoadMouseHook(Control mw, ResizeLimits limits, int msToRefresh = 100)
        {
            frm = mw;
            LoadResizeLimits(limits);
            controlsVisibilityTimer.Start();

            for (int i = 0; i < frm.Controls.Count; i++)
            {
                if (frm.Controls[i] is Panel)
                {
                    for (int p = 0; p < frm.Controls[i].Controls.Count; p++)
                    {
                        visibilities.Add(frm.Controls[i].Controls[p], frm.Controls[i].Controls[p].Visible);
                    }
                }
                else visibilities.Add(frm.Controls[i], frm.Controls[i].Visible);
            }
            LastestSize = frm.Size;

            MouseHandle.Init();
            MouseHandle.addUpRule(new System.Action(() =>
            {
                frm.Size = frm.Size;
            }));
            #region Resize
            MouseHandle.addDownRule(delegate (Point downPosition, Point lpPoint)
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
                    lock (frm.Cursor) TryChangeCursor(Cursors.SizeWE);

                    Size size = new Size(
                        lpPoint.X - frm.Location.X + 2,
                        frm.Size.Height
                        );

                    ItemChanged("CanIResizeWidth", resizeLimits.CanIResizeWidth(size));

                    if (resizeLimits.CanIResizeWidth(size)) frm.Size = size;

                    if (!frm.Size.Equals(LastestSize))
                    {
                        if (AutoRefresh) controlsVisibilityTimer.Restart();
                        LastestSize = frm.Size;
                    }

                    if (AutoRefresh)
                    {
                        if (controlsVisibilityTimer.Elapsed.TotalMilliseconds > msToRefresh)
                            ShowAllOfControls();
                        else
                            HideAllOfControls();
                    }
                }
            });
            MouseHandle.addDownRule(delegate (Point downPosition, Point lpPoint)
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
                    lock (frm.Cursor) TryChangeCursor(Cursors.SizeNS);

                    Size size = new Size(
                        frm.Size.Width,
                        lpPoint.Y - frm.Location.Y + 2
                        );

                    ItemChanged("CanIResizeHeight", resizeLimits.CanIResizeHeight(size));

                    if (resizeLimits.CanIResizeHeight(size)) frm.Size = size;

                    if (!frm.Size.Equals(LastestSize))
                    {
                        if (AutoRefresh) controlsVisibilityTimer.Restart();
                        LastestSize = frm.Size;
                    }

                    if (AutoRefresh)
                    {
                        if (controlsVisibilityTimer.Elapsed.TotalMilliseconds > msToRefresh)
                            ShowAllOfControls();
                        else
                            HideAllOfControls();
                    }
                }
            });
            MouseHandle.addDownRule(delegate (Point downPosition, Point lpPoint)
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
                    lock (frm.Cursor) TryChangeCursor(Cursors.SizeNS);

                    Size size = new Size(
                        lpPoint.X - frm.Location.X + 2,
                        lpPoint.Y - frm.Location.Y + 2
                        );

                    ItemChanged("CanIResizeWidth", resizeLimits.CanIResizeWidth(size));
                    ItemChanged("CanIResizeHeight", resizeLimits.CanIResizeHeight(size));

                    if (resizeLimits.CanIResizeWidth(size)) frm.Size = new Size(size.Width, frm.Size.Height);
                    if (resizeLimits.CanIResizeHeight(size)) frm.Size = new Size(frm.Size.Width, size.Height);

                    if (!frm.Size.Equals(LastestSize))
                    {
                        if (AutoRefresh) controlsVisibilityTimer.Restart();
                        LastestSize = frm.Size;
                    }

                    if (AutoRefresh)
                    {
                        if (controlsVisibilityTimer.Elapsed.TotalMilliseconds > msToRefresh)
                            ShowAllOfControls();
                        else
                            HideAllOfControls();
                    }
                }
            });
            #endregion
            #region ConvertCursorToOptionalCursorIn_Horizontal_Vertical_Both
            MouseHandle.addRule(delegate (Point lpPoint)
            {
                bool isHorizontal = lpPoint.X > frm.Location.X + frm.Size.Width - 8 &&
                        lpPoint.X < frm.Location.X + frm.Size.Width + 2 &&
                        lpPoint.Y > frm.Location.Y &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height - 28;

                if (isHorizontal)
                    lock (frm.Cursor) { TryChangeCursor(Cursors.SizeWE); isResizerCursor = true; }
            });
            MouseHandle.addRule(delegate (Point lpPoint)
            {
                bool isVertical = lpPoint.X > frm.Location.X &&
                        lpPoint.X < frm.Location.X + frm.Size.Width - 28 &&
                        lpPoint.Y > frm.Location.Y + frm.Size.Height - 8 &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height + 2;

                if (isVertical)
                    lock (frm.Cursor) { TryChangeCursor(Cursors.SizeNS); isResizerCursor = true; }
            });
            MouseHandle.addRule(delegate (Point lpPoint)
            {
                bool isBoth = lpPoint.X > frm.Location.X + frm.Size.Width - 28 &&
                        lpPoint.X < frm.Location.X + frm.Size.Width &&
                        lpPoint.Y > frm.Location.Y + frm.Size.Height - 28 &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height;

                if (isBoth)
                    lock (frm.Cursor) { TryChangeCursor(Cursors.SizeNWSE); isResizerCursor = true; }
            });
            #endregion
            #region IfIn_Horizontal_Vertical_Both__AND__CursorNotDefualt
            MouseHandle.addRule(delegate (Point lpPoint)
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
                    TryChangeCursor(Cursors.Default);
                    isHorizontalResize = false;
                    isVerticalResize = false;
                    isFullResize = false;
                }

                if (controlsVisibilityTimer.Elapsed.TotalMilliseconds > msToRefresh)
                {
                    controlsVisibilityTimer.Reset();
                    ShowAllOfControls();
                }

            });
            #endregion
        }

        public void DisableAutoRefresh() => AutoRefresh = false;
        public void EnableAutoRefresh() => AutoRefresh = true;

        private void HideAllOfControls()
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
        }

        private void ShowAllOfControls()
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
        }

        private void TryChangeCursor(Cursor cursor)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    frm.Cursor = cursor;
                    break;
                }
                catch
                {
                }
            }
        }

        public void LoadRoundedBorders() => LoadRoundedBorders(frm);
        public void LoadRoundedBorders(Control control)
        {
            control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, 20, 20));
            control.SizeChanged += (_, __) => control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, control.Width, control.Height, 20, 20));
        }

        public enum CursorTypes
        {
            UNKOWNCURSOR,
            AppStarting,
            Arrow,
            Cross,
            Default,
            IBeam,
            No,
            SizeAll,
            SizeNESW,
            SizeNS,
            SizeNWSE,
            SizeWE,
            UpArrow,
            WaitCursor,
            Help,
            HSplit,
            VSplit,
            NoMove2D,
            NoMoveHoriz,
            NoMoveVert,
            PanEast,
            PanNE,
            PanNorth,
            PanNW,
            PanSE,
            PanSouth,
            PanSW,
            PanWest,
            Hand
        }

        public CursorTypes GetCursor()
        {
            CURSORINFO pci;
            pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
            if (!GetCursorInfo(out pci))
                throw new Win32Exception(Marshal.GetLastWin32Error());

            var h = pci.hCursor;
            if (h == Cursors.AppStarting.Handle) return CursorTypes.AppStarting;
            if (h == Cursors.Arrow.Handle) return CursorTypes.Arrow;
            if (h == Cursors.Cross.Handle) return CursorTypes.Cross;
            if (h == Cursors.Default.Handle) return CursorTypes.Default;
            if (h == Cursors.IBeam.Handle) return CursorTypes.IBeam;
            if (h == Cursors.No.Handle) return CursorTypes.No;
            if (h == Cursors.SizeAll.Handle) return CursorTypes.SizeAll;
            if (h == Cursors.SizeNESW.Handle) return CursorTypes.SizeNESW;
            if (h == Cursors.SizeNS.Handle) return CursorTypes.SizeNS;
            if (h == Cursors.SizeNWSE.Handle) return CursorTypes.SizeNWSE;
            if (h == Cursors.SizeWE.Handle) return CursorTypes.SizeWE;
            if (h == Cursors.UpArrow.Handle) return CursorTypes.UpArrow;
            if (h == Cursors.WaitCursor.Handle) return CursorTypes.WaitCursor;
            if (h == Cursors.Help.Handle) return CursorTypes.Help;
            if (h == Cursors.HSplit.Handle) return CursorTypes.HSplit;
            if (h == Cursors.VSplit.Handle) return CursorTypes.VSplit;
            if (h == Cursors.NoMove2D.Handle) return CursorTypes.NoMove2D;
            if (h == Cursors.NoMoveHoriz.Handle) return CursorTypes.NoMoveHoriz;
            if (h == Cursors.NoMoveVert.Handle) return CursorTypes.NoMoveVert;
            if (h == Cursors.PanEast.Handle) return CursorTypes.PanEast;
            if (h == Cursors.PanNE.Handle) return CursorTypes.PanNE;
            if (h == Cursors.PanNorth.Handle) return CursorTypes.PanNorth;
            if (h == Cursors.PanNW.Handle) return CursorTypes.PanNW;
            if (h == Cursors.PanSE.Handle) return CursorTypes.PanSE;
            if (h == Cursors.PanSouth.Handle) return CursorTypes.PanSouth;
            if (h == Cursors.PanSW.Handle) return CursorTypes.PanSW;
            if (h == Cursors.PanWest.Handle) return CursorTypes.PanWest;
            if (h == Cursors.Hand.Handle) return CursorTypes.Hand;
            return CursorTypes.UNKOWNCURSOR;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct POINT
        {
            public Int32 x;
            public Int32 y;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public Int32 cbSize;        // Specifies the size, in bytes, of the structure. 
                                        // The caller must set this to Marshal.SizeOf(typeof(CURSORINFO)).
            public Int32 flags;         // Specifies the cursor state. This parameter can be one of the following values:
                                        //    0             The cursor is hidden.
                                        //    CURSOR_SHOWING    The cursor is showing.
            public IntPtr hCursor;          // Handle to the cursor. 
            public POINT ptScreenPos;       // A POINT structure that receives the screen coordinates of the cursor. 
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            Int32 nLeftRect,     // x-coordinate of upper-left corner
            Int32 nTopRect,      // y-coordinate of upper-left corner
            Int32 nRightRect,    // x-coordinate of lower-right corner
            Int32 nBottomRect,   // y-coordinate of lower-right corner
            Int32 nWidthEllipse, // width of ellipse
            Int32 nHeightEllipse // height of ellipse
        );

        Control frm { get; set; }
        bool isHorizontalResize { get; set; } = false;
        bool isVerticalResize { get; set; } = false;
        bool isFullResize { get; set; } = false;
        private bool isResizerCursor = false;
        private Dictionary<Control, bool> visibilities { get; set; } = new Dictionary<Control, bool>();
        public Stopwatch controlsVisibilityTimer { get; set; } = new Stopwatch();
        private Size LastestSize { get; set; }
        private bool alreadyHidden { get; set; } = false;
        private bool alreadyShown { get; set; } = true;
        private bool AutoRefresh { get; set; } = true;
        ResizeLimits resizeLimits { get; set; } = new ResizeLimits();
    }
}