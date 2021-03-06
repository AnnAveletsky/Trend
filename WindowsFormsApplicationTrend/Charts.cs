﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibraryTrend;
using System.IO;

namespace WindowsFormsApplicationTrend
{
    public partial class Charts : Form
    {
        SortedList<DateTime, double> List;
        List<double> LineTrendValue;
        List<double> ExpTrendValue;
        List<double> LogTrendValue;
        List<double> PowTrendValue;
        public Charts(SortedList<DateTime, double> list)
        {
            InitializeComponent();
            List = list;
            List<double> X = list.Keys.Select(p => (double)list.Keys.IndexOf(p) + 1).ToList();
            List<double> Y = list.Values.ToList();
            LineTrendValue = Trend.Line(Y, X);
            ExpTrendValue = Trend.Exp(Y, X);
            LogTrendValue = Trend.Log(Y, X);
            PowTrendValue = Trend.Pow(Y, X);
            foreach (var i in list)
            {
                chart1.Series[0].Points.AddXY(i.Key, i.Value);
                chart1.Series[1].Points.AddXY(i.Key, LineTrendValue[list.Keys.IndexOf(i.Key)]);
                chart1.Series[2].Points.AddXY(i.Key, ExpTrendValue[list.Keys.IndexOf(i.Key)]);
                chart1.Series[3].Points.AddXY(i.Key, LogTrendValue[list.Keys.IndexOf(i.Key)]);
                chart1.Series[4].Points.AddXY(i.Key, PowTrendValue[list.Keys.IndexOf(i.Key)]);
            }
            double Min = Math.Min(Math.Min(Math.Min(list.Values.Min(), LineTrendValue.Min()), Math.Min(ExpTrendValue.Min(), LogTrendValue.Min())), PowTrendValue.Min());
            double Max = Math.Max(Math.Max(Math.Max(list.Values.Max(), LineTrendValue.Max()), Math.Max(ExpTrendValue.Max(), LogTrendValue.Max())), PowTrendValue.Max());
            chart1.ChartAreas[0].AxisY.Minimum = Min - (Max - Min) / 10;
            chart1.ChartAreas[0].AxisY.Maximum = Max + (Max - Min) / 5;

            chart1.Legends[1].CustomItems[0].Cells[1].Text = Trend.R2(Y, LineTrendValue).ToString();
            double b = Trend.B(Y, X);
            double a = Trend.A(Y, X, b);
            chart1.Legends[1].CustomItems[0].Cells[2].Text = "a+x*b=" + a + "+x*" + b;


            chart1.Legends[1].CustomItems[1].Cells[1].Text = Trend.R2(Y, ExpTrendValue).ToString();
            List<double> Y1 = Y.Select(y => Math.Log(y)).ToList();
            b = Trend.B(Y1, X);
            a = Trend.A(Y1, X, b);
            chart1.Legends[1].CustomItems[1].Cells[2].Text = "a*e^(x*b)=" + a + "*e^(x*" + b + ")";

            chart1.Legends[1].CustomItems[2].Cells[1].Text = Trend.R2(Y, LogTrendValue).ToString();
            List<double> X1 = X.Select(x => Math.Log(x)).ToList();
            b = Trend.B(Y, X1);
            a = Trend.A(Y, X1, b);
            chart1.Legends[1].CustomItems[2].Cells[2].Text = "a+ln(x*b)=" + a + "+ln(x*" + b + ")";

            chart1.Legends[1].CustomItems[3].Cells[1].Text = Trend.R2(Y, PowTrendValue).ToString();
            b = Trend.B(Y1, X1);
            a = Trend.A(Y1, X1, b);
            chart1.Legends[1].CustomItems[3].Cells[2].Text = "a*x^b=" + a + "*x^" + b;
        }

        private void LineTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (LineTrend.Checked == true)
            {
                chart1.Series[1].Enabled = true;
                chart1.Legends[1].CustomItems[0].Enabled = true;
            }
            else
            {
                chart1.Series[1].Enabled = false;
                chart1.Legends[1].CustomItems[0].Enabled = false;
                
            }
        }

        private void ExpSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (ExpSmoothing.Checked == true)
            {
                chart1.Series[2].Enabled = true;
                chart1.Legends[1].CustomItems[1].Enabled = true;
            }
            else
            {
                chart1.Series[2].Enabled = false;
                chart1.Legends[1].CustomItems[1].Enabled = false;
            }
        }

        private void LogTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (LogTrend.Checked == true)
            {
                chart1.Series[3].Enabled = true;
                chart1.Legends[1].CustomItems[2].Enabled = true;
            }
            else
            {
                chart1.Series[3].Enabled = false;
                chart1.Legends[1].CustomItems[2].Enabled = false;
            }
        }

        private void PowTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (PowTrend.Checked == true)
            {
                chart1.Series[4].Enabled = true;
                chart1.Legends[1].CustomItems[3].Enabled = true;
            }
            else
            {
                chart1.Series[4].Enabled = false;
                    chart1.Legends[1].CustomItems[3].Enabled = false;
            }
        }

        private void ShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowGrid.Checked == true)
            {
                chart1.Series[0].Enabled = true;
            }
            else
            {
                chart1.Series[0].Enabled = false;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            StreamWriter myStream;
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|jpg files (*.jpg)|*.jpg";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FilterIndex == 1)
                {
                    myStream = new StreamWriter(saveFileDialog1.FileName);
                    myStream.Write("Дата\tЗначения функции\tЛинейный тренд\tЭкспененциальное сглаживание\tЛогарифмический тренд\tСтепенной тренд");
                    for (int i = 0; i < List.Keys.Count; i++)
                    {
                        myStream.Write("\n" + List.Keys[i].ToShortDateString() + "\t" + List.Values[i] + "\t" + LineTrendValue[i] + "\t" + ExpTrendValue[i] + "\t" + LogTrendValue[i] + "\t" + PowTrendValue[i]);
                    }
                    myStream.Close();
                }
                else
                {
                    chart1.SaveImage(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

            }
        }
    }
}
