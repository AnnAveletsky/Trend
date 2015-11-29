using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplicationTrend
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            dataGridView1.Rows.Add(new DateTime(1994, 1, 30).ToShortDateString(), 11);
            dataGridView1.Rows.Add(new DateTime(1994, 2, 28).ToShortDateString(), 13);
            dataGridView1.Rows.Add(new DateTime(1994, 3, 30).ToShortDateString(), 12);
            dataGridView1.Rows.Add(new DateTime(1994, 4, 30).ToShortDateString(), 16);
        }

        private void BtnGrid_Click(object sender, EventArgs e)
        {
            SortedList<DateTime, double> list = new SortedList<DateTime, double>();
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DateTime dataTimeResult = new DateTime();
                double doubleResult = 0;
                if (DateTime.TryParse(dataGridView1.Rows[i].Cells[0].Value.ToString(), out dataTimeResult)==false)
                {
                    MessageBox.Show("В 1м столбце, в" + (i + 1) + "й строке обшибка");
                    return;
                }
                if (Double.TryParse(dataGridView1.Rows[i].Cells[1].Value.ToString(), out doubleResult) == false)
                {
                    MessageBox.Show("В 2м столбце, в" + (i + 1) + "й строке обшибка");
                    return;
                }
                try
                {
                    list.Add(dataTimeResult, doubleResult);

                }
                catch
                {
                    MessageBox.Show("В " + (i + 1) + "й строке обшибка. Запись с таким ключём уже существует");
                }
            }
            if (list.Count != 0&&list.Count!=1)
            {
                Charts charts = new Charts(list);
                charts.Activate();
                charts.Show();
            }
            else
            {
                MessageBox.Show("Список с данными слишком мал");
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }



    }
}
