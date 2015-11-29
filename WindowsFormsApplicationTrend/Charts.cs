using System;
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
        public Charts(SortedList<DateTime, double> list, double Alpha)
        {
            InitializeComponent();
            List = list;
            LineTrendValue = Trend.Line(list.Values.ToList());
            ExpTrendValue = Trend.Exp(list.Values.ToList(), Alpha);
            LogTrendValue = Trend.Log(list.Values.ToList());
            PowTrendValue = Trend.Pow(list.Values.ToList());
            foreach (var i in list)
            {
                chart1.Series[0].Points.AddXY(i.Key, i.Value);
                chart1.Series[1].Points.AddXY(i.Key, LineTrendValue[list.Keys.IndexOf(i.Key)]);
                chart1.Series[2].Points.AddXY(i.Key, ExpTrendValue[list.Keys.IndexOf(i.Key)]);
                chart1.Series[3].Points.AddXY(i.Key, LogTrendValue[list.Keys.IndexOf(i.Key)]);
                chart1.Series[4].Points.AddXY(i.Key, PowTrendValue[list.Keys.IndexOf(i.Key)]);
            }
            chart1.Legends[1].CustomItems[0].Cells[1].Text = Trend.A(list.Values.ToList()).ToString();
            chart1.Legends[1].CustomItems[1].Cells[1].Text =Math.Round( Trend.B(list.Values.ToList()),6).ToString();
            chart1.Legends[1].CustomItems[2].Cells[1].Text = Alpha.ToString();
        }

        private void LineTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (LineTrend.Checked == true)
            {
                chart1.Series[1].Enabled = true;
                chart1.Legends[1].CustomItems[0].Enabled = true;
                chart1.Legends[1].CustomItems[1].Enabled = true;
            }
            else
            {
                chart1.Series[1].Enabled = false;
                if (LogTrend.Checked == false && PowTrend.Checked == false)
                {
                    chart1.Legends[1].CustomItems[0].Enabled = false;
                    chart1.Legends[1].CustomItems[1].Enabled = false;
                }
            }
        }

        private void ExpSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (ExpSmoothing.Checked == true)
            {
                chart1.Series[2].Enabled = true;
                chart1.Legends[1].CustomItems[2].Enabled = true;
            }
            else
            {
                chart1.Series[2].Enabled = false;
                chart1.Legends[1].CustomItems[2].Enabled = false;
            }
        }

        private void LogTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (LogTrend.Checked == true)
            {
                chart1.Series[3].Enabled = true;
                chart1.Legends[1].CustomItems[0].Enabled = true;
                chart1.Legends[1].CustomItems[1].Enabled = true;
            }
            else
            {
                chart1.Series[3].Enabled = false;
                if (LineTrend.Checked == false && PowTrend.Checked == false)
                {
                    chart1.Legends[1].CustomItems[0].Enabled = false;
                    chart1.Legends[1].CustomItems[1].Enabled = false;
                }
            }
        }

        private void PowTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (PowTrend.Checked == true)
            {
                chart1.Series[4].Enabled = true;
                chart1.Legends[1].CustomItems[0].Enabled = true;
                chart1.Legends[1].CustomItems[1].Enabled = true;
            }
            else
            {
                chart1.Series[4].Enabled = false;
                if (LogTrend.Checked == false && LineTrend.Checked == false)
                {
                    chart1.Legends[1].CustomItems[0].Enabled = false;
                    chart1.Legends[1].CustomItems[1].Enabled = false;
                }
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
