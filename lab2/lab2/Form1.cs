using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int N = 28;
        static double[] px;
        static int round = 5;
        static Random rand = new Random();
        public void Calc_px()
        {
            //  N = Convert.ToInt32(numericUpDown1.Value);
            // int K = Convert.ToInt32(numericUpDown2.Value);
            //  int round = Convert.ToInt32(numericUpDown3.Value);
            //  double sI = 0;
            //  double sH = 0;
            richTextBox1.Clear();
            // for (int k = 1; k <= K; k++)
            // {
            // richTextBox1.AppendText("Эксперимент: " + k.ToString() + "\n");
            px = new double[N];
            double b = 0;
            double s = 0;
            int offset = 0;
            int count = 0;
            if (N % 10 > 0 || N == 10)
            {
                b = 1 / (Convert.ToDouble(N) / 2);
                offset = N / (N / 2);
            }
            else
            {
                b = 1.0 / 10;
                offset = N / 10;
            }
            while (s != 1 && count < N)
            {
                double bv = b;
                double sv = 0;
                for (int i = count; i < count + offset - 1; i++)
                {
                    px[i] = bv * rand.NextDouble();
                    bv -= px[i];
                    sv += px[i];

                }
                px[count + offset - 1] = b - sv;
                sv += px[count + offset - 1];
                s += sv;
                count += offset;
            }
            richTextBox1.AppendText("\n");
            for (int i = 0; i < N; i++)
            {
                richTextBox1.AppendText("px[" + (i + 1).ToString() + "] = " + Math.Round(px[i], round).ToString() + "\n");
            }
            richTextBox1.AppendText("s = " + s.ToString() + "\n");
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Calc_px();
        }
    }
}
