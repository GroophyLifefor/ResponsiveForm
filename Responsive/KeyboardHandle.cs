using H.Hooks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Responsive
{
    public class KeyboardHandle
    {
        public static void Init()
        {
            var keyboardHook = new LowLevelKeyboardHook();

            //Configure delegates
            keyboardHook.Down += (sender, _) => 
            {
                foreach (var rule in DownRules)
                    rule(_);
            };
            keyboardHook.Up += (sender, _) => 
            {
                foreach (var rule in UpRules)
                    rule(_);
            };

            keyboardHook.Start();
        }

        public static void addDownRule(Action<KeyboardEventArgs> action)
        {
            DownRules.Add(action);
        }
        public static void addUpRule(Action<KeyboardEventArgs> action)
        {
            UpRules.Add(action);
        }

        static List<Action<KeyboardEventArgs>> DownRules = new List<Action<KeyboardEventArgs>>();
        static List<Action<KeyboardEventArgs>> UpRules = new List<Action<KeyboardEventArgs>>();
    }
}
