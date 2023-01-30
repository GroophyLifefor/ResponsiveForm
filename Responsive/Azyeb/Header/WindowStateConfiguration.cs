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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsive.Azyeb
{
    public class WindowStateConfiguration
    {
        public WindowStateChangeItem Minimalize { get; set; } = new WindowStateChangeItem(20, 5, null);
        public WindowStateChangeItem MaximizeToNormal { get; set; } = new WindowStateChangeItem(20, -1, null);
        public WindowStateChangeItem NormalToMaximize { get; set; } = new WindowStateChangeItem(20, -1, null);
        public Func<double, double, int, Func<double, double, double, double, double>, double[]>? GetGradiant { internal get; set; }

        internal static double EllipseFormula(double A, double B, double X, double addTo)
            => Math.Sqrt(Math.Pow(A, 2) - ((Math.Pow(A, 2) * Math.Pow(X, 2)) / Math.Pow(B, 2))) + addTo;

        internal static double[] GetGradiantSlowToFast(double start, double end, int times, Func<double, double, double, double, double> formula)
        {
            // A is width/2 of ellipse and B is height/2 of ellipse.
            // Ellipse math formula was   √(A^2 - ((A^2 * X^2) / B^2))
            // Thank you to my math teacher(Aybike hocam) who helped me write this algorithm.
            double[] doubles = new double[times + 1];
            if (end > start)
            {
                double A = end - start;
                double B = times;
                for (int i = 0; i < times + 1; i++)
                {
                    doubles[i] = formula(A, B, i, start);
                }
                doubles = doubles.Reverse().ToArray();
                for (int i = 0; i < doubles.Length / 2; i++)
                {
                    (doubles[i], doubles[doubles.Length - i - 1]) = (start + (end - doubles[doubles.Length - i - 1]), end - (doubles[i] - start));
                }
            }
            else if (end == start)
            {
                doubles = Enumerable.Range(0, times + 1).Select(x => start).ToArray();
            }
            else
            {
                double A = end - start;
                double B = times;
                for (int i = 0; i < times + 1; i++)
                {
                    doubles[i] = formula(A, B, i, end);
                }
                return doubles;
            }
            return doubles;
        }

        public WindowStateConfiguration init()
        {
            Minimalize          = new WindowStateChangeItem(20,  5, EllipseFormula);
            MaximizeToNormal    = new WindowStateChangeItem(20, -1, EllipseFormula);
            NormalToMaximize    = new WindowStateChangeItem(20, -1, EllipseFormula);
            GetGradiant         = GetGradiantSlowToFast;
            return this;
        }


    }
}
