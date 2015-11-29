using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
                double Alpha = 0;
                if (double.TryParse(textBox1.Text.ToString(), out Alpha)&&Alpha<=1)
                {
                    Charts charts = new Charts(list, Alpha);
                    charts.Activate();
                    charts.Show();
                }
                else
                {
                    MessageBox.Show("Чтото не так с коэфициентом сглаживания");
                }
            }
            else
            {
                MessageBox.Show("Список с данными слишком мал");
            }
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader myStream = new StreamReader(openFileDialog1.FileName);
                int i=0;
                while(myStream.EndOfStream==false){
                    try{
                    string[] line=myStream.ReadLine().Split('\t');
                        DateTime dateTime;
                        double value;
                        if (DateTime.TryParse(line[0], out dateTime) == true && double.TryParse(line[1], out value)==true)
                        {
                            dataGridView1.Rows.Add(dateTime.ToShortDateString(), value);
                        }
                    }catch{
                        if(i!=0){
                            MessageBox.Show("Ошибка чтения файла");
                        }
                    }
                    i++;
                }
                 
            }
        }



    }
}
