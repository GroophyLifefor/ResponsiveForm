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
using H.Hooks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Responsive.Azyeb
{
    internal class MouseHandle
    {

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        static MouseHandle()
        {
            // Start mouse hook and mouse move thread
            var mouseHook = new LowLevelMouseHook();
            Thread t = new Thread(MouseMove);

            //Configure delegates
            mouseHook.Down += (sender, _) => { downPosition = _.Position; isDownMove = true; };
            mouseHook.Up += (sender, _) => {
                downPosition = Point.Empty; isDownMove = false;
                Cursor.Current = Cursors.Default;
            };

            //Start it
            mouseHook.Start();
            t.Start();
        }

        /// <summary>
        /// Add a mouse down event rule
        /// </summary>
        /// <param name="action"></param>
        public static void AddDownRule(Action<Point, Point> action)
        {
            lock (_lock)
            {
                DownRules.Add(action);
            }
        }

        /// <summary>
        /// Add a mouse up event rule
        /// </summary>
        /// <param name="action"></param>
        public static void AddUpRule(Action action)
        {
            UpRules.Add(action);
        }

        /// <summary>
        /// Add a mouse movement rule
        /// </summary>
        /// <param name="action"></param>
        public static void AddRule(Action<Point> action)
        {
            lock (_lock)
            {
                Rules.Add(action);
            }
        }

        /// <summary>
        /// Continuously check for mouse events
        /// </summary>
        private static void MouseMove()
        {
            for (; ; )
            {
                Point lpPoint;
                GetCursorPos(out lpPoint);
                if (isDownMove)
                {
                    lock (_lock)
                    {
                        foreach (var rule in DownRules) rule(downPosition, lpPoint);
                    }
                }
                else if (isUp)
                {
                    foreach (var rule in UpRules) rule();
                }
                else // every moment
                {
                    lock (_lock)
                    {
                        foreach (var rule in Rules) rule(lpPoint);
                    }
                }
            }
        }

        private static readonly object _lock = new object();

        static List<Action<Point, Point>> DownRules = new List<Action<Point, Point>>();
        static List<Action> UpRules = new List<Action>();
        static List<Action<Point>> Rules = new List<Action<Point>>();

        static Point downPosition = Point.Empty;

        static bool isDownMove = false;
        static bool isUp = false;
    }
}
