using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryTrend
{
    public static class Trend
    {
        public static List<double> Log(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            for (int i = 0; i < Y.Count; i++)
            {
                double a = (i != Y.Count) ? 0 : Y[i + 1] - Y[i];
                double b = Y[i];
                TrendY.Add(a * Math.Log(i) + b);
            }
            return TrendY;
        }
        public static List<double> Pow(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            for (int i = 0; i < Y.Count; i++)
            {
                double a = (i != Y.Count) ? 0 : Y[i + 1] - Y[i];
                double b = Y[i];
                TrendY.Add(a * Math.Pow(i,b));
            }
            return TrendY;
        }
    }
}
