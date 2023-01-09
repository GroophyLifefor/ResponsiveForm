using System.Drawing;
using System.Windows.Forms;

namespace Responsive
{
    public class Resizer
    {
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

            public bool CanIResizeWidth(Size size) => !(
                isMinWidth && size.Width < minWidth ||
                isMaxWidth && size.Width > maxWidth);

            public bool CanIResizeHeight(Size size) => !(
                isMinHeight && size.Height < minHeight ||
                isMaxHeight && size.Height > maxHeight);
        }

        public void LoadResizeLimits(Control cntl, ResizeLimits limits)
        {
            cntl.SizeChanged += (s, e) =>
            {
                if (cntl is Form)
                {
                    if (((Form)cntl).WindowState != FormWindowState.Normal) return;
                }
                
                Size size = cntl.Size;
                if (limits.isMinWidth && size.Width < limits.minWidth) size = new Size(limits.minWidth, size.Height);
                if (limits.isMaxWidth && size.Width > limits.maxWidth) size = new Size(limits.maxWidth, size.Height);
                if (limits.isMinHeight && size.Height < limits.minHeight) size = new Size(size.Width, limits.minHeight);
                if (limits.isMaxHeight && size.Height > limits.maxHeight) size = new Size(size.Width, limits.maxHeight);
                cntl.Size = size;
            };
        }

        public void LoadMouseHook(Control mw, ResizeLimits limits)
        {
            frm = mw;

            MouseHandle.Init();
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

                    if (limits.CanIResizeWidth(size)) frm.Size = size;
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

                    if (limits.CanIResizeHeight(size)) frm.Size = size;
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

                    if (limits.CanIResizeWidth(size)) frm.Size = new Size(size.Width, frm.Size.Height);
                    if (limits.CanIResizeHeight(size)) frm.Size = new Size(frm.Size.Width, size.Height);
                }
            });

            MouseHandle.addRule(delegate (Point lpPoint)
            {
                if (lpPoint.X > frm.Location.X + frm.Size.Width - 8 &&
                        lpPoint.X < frm.Location.X + frm.Size.Width + 2 &&
                        lpPoint.Y > frm.Location.Y &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height - 28)
                    lock (frm.Cursor) TryChangeCursor(Cursors.SizeWE);
            });
            MouseHandle.addRule(delegate (Point lpPoint)
            {
                if (lpPoint.X > frm.Location.X &&
                        lpPoint.X < frm.Location.X + frm.Size.Width - 28 &&
                        lpPoint.Y > frm.Location.Y + frm.Size.Height - 8 &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height + 2)
                    lock (frm.Cursor) TryChangeCursor(Cursors.SizeNS);
            });
            MouseHandle.addRule(delegate (Point lpPoint)
            {
                if (lpPoint.X > frm.Location.X + frm.Size.Width - 28 &&
                        lpPoint.X < frm.Location.X + frm.Size.Width &&
                        lpPoint.Y > frm.Location.Y + frm.Size.Height - 28 &&
                        lpPoint.Y < frm.Location.Y + frm.Size.Height)
                    lock (frm.Cursor) TryChangeCursor(Cursors.SizeNWSE);
            });
            MouseHandle.addRule(delegate (Point lpPoint)
            {
                if (isHorizontalResize)
                {
                    isHorizontalResize = false;
                    lock (frm.Cursor) TryChangeCursor(Cursors.Default);
                }
            });
            MouseHandle.addRule(delegate (Point lpPoint)
            {
                if (isVerticalResize)
                {
                    isVerticalResize = false;
                    lock (frm.Cursor) TryChangeCursor(Cursors.Default);
                }
            });
            MouseHandle.addRule(delegate (Point lpPoint)
            {
                if (isFullResize)
                {
                    isFullResize = false;
                    lock (frm.Cursor) TryChangeCursor(Cursors.Default);
                }
            });
        }

        public static void TryChangeCursor(Cursor cursor)
        {
            for (int i = 0;i < 5;i++ )
            {
                try
                {
                    frm.Cursor = cursor;
                    break;
                } catch {
                }
            }
        }

        static Control frm { get; set; }
        static bool isHorizontalResize { get; set; } = false;
        static bool isVerticalResize { get; set; } = false;
        static bool isFullResize { get; set; } = false;
    }
}