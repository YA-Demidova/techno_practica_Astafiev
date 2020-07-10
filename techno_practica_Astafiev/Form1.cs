using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace techno_practica_Astafiev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            x = new List<float>();
            y = new List<float>();
            x1 = new List<float>();
            y1 = new List<float>();
            dataGV.ColumnCount = 2;

        }

        public List<float> x;
        public List<float> y;
        public List<float> y1;
        public List<float> x1;
        float a, b, SumXY, SumX2, SumX, SumY;
        Random rnd = new Random();

        private void btnRandom_Click(object sender, EventArgs e)
        {
            dataGV.RowCount = 10;
            for (int X=0;X<9;X++)
            {
                dataGV.Rows[X].Cells[0].Value = X;
                dataGV.Rows[X].Cells[1].Value = X * rnd.Next(0, 10); 
            }


            for (int i = 0; i < dataGV.RowCount - 1; i++)
            {
                x.Add((float)Convert.ToDouble(dataGV.Rows[i].Cells[0].Value));
                y.Add((float)Convert.ToDouble(dataGV.Rows[i].Cells[1].Value));
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            chrLineAPX.Series[0].Points.DataBindXY(x, y);
            chrLineAPX.Series[1].Points.DataBindXY(x1, y1);

            for (int i = 0; i < dataGV.RowCount - 1; i++)
            {
                x.Add((float)Convert.ToDouble(dataGV.Rows[i].Cells[0].Value));
                y.Add((float)Convert.ToDouble(dataGV.Rows[i].Cells[1].Value));
            }
            APX();
            chrLineAPX.ChartAreas[0].AxisX.Maximum = dataGV.RowCount - 1;
            x1.Add(0); 
            x1.Add(Convert.ToInt32(chrLineAPX.ChartAreas[0].AxisX.Maximum));

            y1.Add(Convert.ToInt64(b)); 
            y1.Add(Convert.ToInt32(a * x1[1] + b));

            chrLineAPX.Series[0].Points.DataBindXY(x, y);
            chrLineAPX.Series[1].Points.DataBindXY(x1, y1);

            if (b < 0)
                label1.Text = "y = " + a + "x - " + Math.Abs(b);
            else
                label1.Text = "y = " + a + "x + " + b;
        }

        private void APX()
        {
            SumX = 0; SumY = 0; SumXY = 0; SumX2 = 0;
            for (int i = 0; i < x.Count; i++)
            {
                SumX += x[i];
                SumY += y[i];
                SumXY += x[i] * y[i];
                SumX2 += x[i] * x[i];
            }
            a = (float)(x.Count * SumXY - SumX * SumY) / (float)(x.Count * SumX2 - SumX * SumX);
            b = (float)(SumY - a * SumX) / (float)x.Count;
        }

    }
}
