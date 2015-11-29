using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryTrend;
using System.Collections.Generic;

namespace UnitTestProjectTrend
{
    [TestClass]
    public class TestTrends
    {
        [TestMethod]
        public void LineTrend()
        {
            Trend.Line(new List<double>() { 1, 2, 5, 6 });
        }
        [TestMethod]
        public void LogTrend()
        {
            Trend.Log(new List<double>() { 1, 2, 5, 6 });
        }
        [TestMethod]
        public void PowTrend()
        {
            Trend.Pow(new List<double>() { 1, 2, 5, 6 });
        }
    }
}
