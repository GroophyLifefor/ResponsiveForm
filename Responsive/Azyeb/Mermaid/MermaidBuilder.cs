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
using System.Text;

namespace Responsive.Azyeb.Mermaid
{
    public delegate void TextChanged(object? sender, EventArgs e);
    public class MermaidBuilder
    {
        public static event TextChanged? TextChanged;

        private static void _TextChanged()
        {
            if (TextChanged is not null)
            {
                TextChanged(null, new EventArgs());
            }
        }
        
        static MermaidBuilder()
        {
            _mermaidBuilder.AppendLine("sequenceDiagram");
            if (_enable) _TextChanged();
        }

        internal static void AddConnection(string from, string to, string text, bool translucent = false)
        {
            if (!_enable) return;
            _mermaidBuilder.AppendLine($"    {from}{(translucent ? "-->>+" : "->>+")}{to}: {text}");
            _TextChanged();
        }

        public static void Enable()
            => _enable = true;

        public static void Disable()
            => _enable = false;

        public static string GetMermaid()
            => _mermaidBuilder.ToString();

        private static StringBuilder _mermaidBuilder { get; set; } = new StringBuilder();
        private static bool _enable = true;
    }
}
