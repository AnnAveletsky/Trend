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

namespace WindowsFormsApplicationTrend
{
    public partial class Charts : Form
    {
        public Charts(SortedList<DateTime, double> list)
        {
            InitializeComponent();
            List<double> LineTrend = Trend.Line(list.Values.ToList());
            List<double> LogTrend = Trend.Log(list.Values.ToList());
            List<double> PowTrend = Trend.Pow(list.Values.ToList());
            foreach (var i in list)
            {
                chart1.Series[0].Points.AddXY(i.Key, i.Value);
                chart1.Series[1].Points.AddXY(i.Key, LineTrend[list.Keys.IndexOf(i.Key)]);
                chart1.Series[3].Points.AddXY(i.Key, LogTrend[list.Keys.IndexOf(i.Key)]);
                chart1.Series[4].Points.AddXY(i.Key, PowTrend[list.Keys.IndexOf(i.Key)]);
            }

        }

        private void LineTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (LineTrend.Checked == true)
            {
                chart1.Series[1].Enabled = true;
            }
            else
            {
                chart1.Series[1].Enabled = false;
            }
        }

        private void ExpSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (ExpSmoothing.Checked == true)
            {
                chart1.Series[2].Enabled = true;
            }
            else
            {
                chart1.Series[2].Enabled = false;
            }
        }

        private void LogTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (LogTrend.Checked == true)
            {
                chart1.Series[3].Enabled = true;
            }
            else
            {
                chart1.Series[3].Enabled = false;
            }
        }

        private void PowTrend_CheckedChanged(object sender, EventArgs e)
        {
            if (PowTrend.Checked == true)
            {
                chart1.Series[4].Enabled = true;
            }
            else
            {
                chart1.Series[4].Enabled = false;
            }
        }
        
    }
}
