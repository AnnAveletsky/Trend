using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryTrend
{
    public static class Trend
    {
        public static List<double> Line(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            double Xmean = 0;
            double SumXY = 0;
            double SumX2 = 0;
            for (int Xi = 0; Xi < TrendY.Count; Xi++)
            {
                Xmean += Xi;
                SumXY += Xi * Y[Xi];
                SumX2 += Xi * Xi;
            }
            Xmean /= Y.Count;
            double Ymean = TrendY.Sum() / Y.Count;
            double b = (SumXY-Y.Count*Xmean*Ymean)/(SumX2-Y.Count*Xmean*Xmean);
            double a = Ymean - Xmean * b;

            for (int Xi = 0; Xi < Y.Count; Xi++)
            {
                TrendY.Add(a + Xi * b);
            }
            return TrendY;
        }

        public static List<double> Exp(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            
            return TrendY;
        }
        public static List<double> Log(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            for (int Xi = 0; Xi < Y.Count; Xi++)
            {
                double a = (Xi != Y.Count) ? 0 : Y[Xi + 1] - Y[Xi];
                double b = Y[Xi];
                TrendY.Add(a * Math.Log(Xi) + b);
            }
            return TrendY;
        }
        public static List<double> Pow(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            for (int Xi = 0; Xi < Y.Count; Xi++)
            {
                double a = (Xi != Y.Count) ? 0 : Y[Xi + 1] - Y[Xi];
                double b = Y[Xi];
                TrendY.Add(a * Math.Pow(Xi, b));
            }
            return TrendY;
        }
    }
}
