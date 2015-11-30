using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryTrend
{
    public static class Trend
    {
        public static double A(List<double> Y)
        {
            return A(Ymean(Y), Xmean(Y.Count), B(SumXY(Y), SumX2(Y.Count), Y.Count, Xmean(Y.Count), Ymean(Y)));
        }
        static double A(double Ymean, double Xmean, double b)
        {
            return Ymean - Xmean * b;
        }
        static double B(double SumXY, double SumX2, int Count, double Xmean, double Ymean)
        {
            return (SumXY - Count * Xmean * Ymean) / (SumX2 - Count * Xmean * Xmean);
        }
        public static double B(List<double> Y)
        {
            return B(SumXY(Y), SumX2(Y.Count), Y.Count, Xmean(Y.Count), Ymean(Y));
        }
        static double Xmean(int Count)
        {
            double Xmean = 0;
            for (int Xi = 1; Xi <= Count; Xi++)
            {
                Xmean += Xi;
            }
            return Xmean/Count;
        }
        static double Ymean(List<double> Y)
        {
            return Y.Sum() / Y.Count;
        }
        static double SumXY(List<double> Y)
        {
            double SumXY = 0;
            for (int Xi = 1; Xi <= Y.Count; Xi++)
            {
                SumXY += Xi * Y[Xi - 1];
            }
            return SumXY;
        }
        static double SumX2(int Count)
        {
            double SumX2 = 0;
            for (int Xi = 1; Xi <= Count; Xi++)
            {
                SumX2 += Xi * Xi;
            }
            return SumX2;
        }
        public static double R2(List<double> Y, List<double> TrendY)
        {
            double R2 = 0;
            double ymean = Ymean(Y);
            for (int i = 0; i < Y.Count; i++)
            {
                R2 += Math.Pow((Y[i] - TrendY[i]), 2) / Math.Pow((Y[i] - ymean), 2);
            }
            return 1 - R2;
        }
        public static List<double> Line(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            double a = A(Y);
            double b = B(Y);
            for (int Xi = 1; Xi <= Y.Count; Xi++)
            {
                TrendY.Add( a+ Xi * b);
            }
            return TrendY;
        }
        public static List<double> Exp(List<double> Y, double Alpha)
        {
            List<double> TrendY = new List<double>();
            TrendY.Add(Y[0]);
            TrendY.Add(Y[1]);
            for (int i = 2; i < Y.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < i-1; j++)
                {
                    sum += Math.Pow((1 - Alpha), j - 1)*Y[i-j];
                }
                TrendY.Add(Alpha * sum +Math.Pow((1 - Alpha), i - 2) * TrendY[i - 2]);
            }
            return TrendY;
        }
        public static List<double> Log(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            double a = A(Y);
            double b = B(Y);
            for (int Xi = 1; Xi <= Y.Count; Xi++)
            {
                TrendY.Add(a + Math.Log(Xi) * b);
            }
            return TrendY;
        }
        public static List<double> Pow(List<double> Y)
        {
            List<double> TrendY = new List<double>();
            double a = A(Y);
            double b = B(Y);
            for (int Xi = 1; Xi <= Y.Count; Xi++)
            {
                TrendY.Add(a* Math.Pow(Xi, b));
            }
            return TrendY;
        }
    }
}
