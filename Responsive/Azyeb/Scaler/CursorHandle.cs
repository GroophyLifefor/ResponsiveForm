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
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Responsive.Azyeb
{
    internal class CursorHandle
    {
        public static void TryChangeCursor(Cursor cursor)
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    if (Form.ActiveForm != null)
                    {
                        Form.ActiveForm.Cursor = cursor;
                        break;
                    }
                }
                catch
                {
                    // ignored
                }
            }
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

        public static CursorTypes GetCursor()
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
        internal struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CURSORINFO
        {
            public int cbSize;        // Specifies the size, in bytes, of the structure. 
                                        // The caller must set this to Marshal.SizeOf(typeof(CURSORINFO)).
            public int flags;         // Specifies the cursor state. This parameter can be one of the following values:
                                        //    0             The cursor is hidden.
                                        //    CURSOR_SHOWING    The cursor is showing.
            public IntPtr hCursor;          // Handle to the cursor. 
            public POINT ptScreenPos;       // A POINT structure that receives the screen coordinates of the cursor. 
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetCursorInfo(out CURSORINFO pci);
    }
}