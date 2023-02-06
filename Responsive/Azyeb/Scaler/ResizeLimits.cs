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
using Responsive.Azyeb.Mermaid;
using System.Drawing;

namespace Responsive.Azyeb
{
    public class ResizeLimits
    {
        public int minWidth { get; set; } = -1;
        public int maxWidth { get; set; } = -1;
        public int minHeight { get; set; } = -1;
        public int maxHeight { get; set; } = -1;

        public bool isMinWidth
            {
                get { return minWidth != -1; }
            }

        public bool isMaxWidth
            {
                get { return maxWidth != -1; }
            }

        public bool isMinHeight
            {
                get { return minHeight != -1; }
            }

        public bool isMaxHeight
            {
                get { return maxHeight != -1; }
            }

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

        public static ResizeLimits GenerateResizeLimitsByHeader(Header header)
        {
            MermaidBuilder.AddConnection("Scaler", "Header", "Generate resizeLimits by header", true);
            return new ResizeLimits()
            {
                minWidth = header.HeaderWidth,
                minHeight = header.HeaderHeight
            };
        }

        public override string ToString()
        {
            string text = $" {(isMinWidth ? $"minWidth: {minWidth}, " : "")}{(isMinHeight ? $"minHeight: {minHeight}, " : "")}{(isMaxWidth ? $"maxWidth: {maxWidth}, " : "")}{(isMaxHeight ? $"maxHeight: {maxHeight}, " : "")}".TrimEnd(',', ' ');
            return $"{{{text}}}";
        }
    }
}