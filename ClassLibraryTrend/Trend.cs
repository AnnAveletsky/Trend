using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryTrend
{
    public static class Trend
    {
        public static double SumT1T2(List<double> T1, List<double> T2)
        {
            double result=0;
            for (int i = 0; i < T1.Count; i++)
            {
                result+=T1[i]*T2[i];
            }
            return result;
        }
        public static double SumT1SumT2(List<double> T1, List<double> T2)
        {
            return T1.Sum()*T2.Sum();
        }
        public static double DelMinus(double Sum1T1T2, double Sum1T1SumT2, double Sum2T1T2, double Sum2T1SumT2,int Count)
        {
            return (Count * Sum1T1T2 - Sum1T1SumT2) / (Count * Sum2T1T2 - Sum2T1SumT2);
        }
        public static double Minus(double Count,double Sum1,double Coefficient,double Sum2)
        {
            return 1 / Count * (Sum1 - Coefficient * Sum2);
        }
        public static double R2(List<double> Y, List<double> TrendY)
        {
            double chisl = 0;
            double znam = 0;
            for (int i = 0; i < Y.Count; i++)
            {
                chisl+=Math.Pow((Y[i] - TrendY[i]), 2);
                znam+=Math.Pow((Y[i] - Y.Sum()/Y.Count), 2);
            }
            return 1 - chisl/znam;
        }
        public static double B(List<double> Y, List<double> X)
        {
            return DelMinus(SumT1T2(Y, X), SumT1SumT2(Y, X), SumT1T2(X, X), SumT1SumT2(X, X), Y.Count);
        }
        public static double A(List<double> Y, List<double> X,double b)
        {
            return Minus(Y.Count, Y.Sum(), b, X.Sum());
        }
        public static List<double> Line(List<double> Y,List<double> X)
        {
            List<double> TrendY = new List<double>();
            double b = B(Y, X);
            double a = A(Y,X,b);
            for (int Xi = 1; Xi <= Y.Count; Xi++)
            {
                TrendY.Add(a + Xi * b);
            }
            return TrendY;
        }
        public static List<double> Exp(List<double> Y, List<double> X)
        {
           Y= Y.Select(y => Math.Log(y)).ToList();
            List<double> TrendY = new List<double>();
            double b = B(Y, X);
            double a =Math.Exp( A(Y, X, b));
            for (int Xi = 1; Xi <= Y.Count; Xi++)
            {
                TrendY.Add(a * Math.Exp(Xi *b));
            }
            return TrendY;
        }
        public static List<double> Log(List<double> Y, List<double> X)
        {
            X = X.Select(x => Math.Log(x)).ToList();
            List<double> TrendY = new List<double>();
            double b = B(Y, X);
            double a = A(Y, X, b);
            for (int Xi = 1; Xi <= Y.Count; Xi++)
            {
                TrendY.Add(b * Math.Log(Xi) + a);
            }
            return TrendY;
        }
        public static List<double> Pow(List<double> Y, List<double> X)
        {
            Y = Y.Select(y => Math.Log(y)).ToList();
            X = X.Select(x => Math.Log(x)).ToList();
            List<double> TrendY = new List<double>();
            double b = B(Y, X);
            double a =Math.Exp( A(Y, X, b));
            for (int Xi = 1; Xi <= Y.Count; Xi++)
            {
                TrendY.Add(a* Math.Pow(Xi, b));
            }
            return TrendY;
        }
    }
}
