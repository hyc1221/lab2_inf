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
        static double[,] px;
        static double[,,] pxy;
        static int round = 5;
        static Random rand = new Random();
        static int K = 0;

        public void Output_pxy(int k)
        {
            richTextBox2.Clear();
            double s = 0;
            richTextBox3.AppendText("p(x/y):\n");
            for (int i = 0; i < N; i++)
            {
                s = 0;
                for (int j = 0; j < N; j++)
                {
                    richTextBox2.AppendText(String.Format("{0:f17} ", pxy[k, i, j]));
                    s += pxy[k, i, j];
                }
                richTextBox2.AppendText("\n");
                richTextBox3.AppendText("s[" + (i + 1).ToString() + "] = " + s.ToString() + "\n");
            }
            richTextBox3.AppendText("\n");
            for (int i = 0; i < N; i++)
            {
                richTextBox2.Find(String.Format("{0:f17} ", pxy[k, i, i]));
                richTextBox2.SelectionColor = Color.Red;
            }
        }

        public void Output_px(int k)
        {
            richTextBox3.Clear();
            richTextBox1.Clear();
            double s = 0;
            for (int i = 0; i < N; i++)
            {
                richTextBox1.AppendText("px[" + (i + 1).ToString() + "] = " + Math.Round(px[k, i], round).ToString() + "\n");
                s += px[k, i];
            }
            richTextBox3.AppendText("p(x): \n" + "s = " + s.ToString() + "\n\n");
        }

        public void Calc_pxy(int kv)
        {
            pxy = new double[kv, N, N];
            for (int k = 0; k < K; k++)
            {
                for (int h = 0; h < N; h++) pxy[k, h, h] = 0.7 + 0.3 * rand.NextDouble();
                for (int i = 0; i < N; i++)
                {
                    double b = 1 - pxy[k, i, i];
                    for (int j = 0; j < N; j++)
                    {
                        if (i != j)
                        {
                            pxy[k, i, j] = b * rand.NextDouble();
                            b -= pxy[k, i, j];
                        }
                    }
                }
            }
        }

        public void Calc_px(int kv)
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
            px = new double[kv, N];
            for (int k = 0; k < K; k++)
            {
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
                        px[k, i] = bv * rand.NextDouble();
                        bv -= px[k, i];
                        sv += px[k, i];

                    }
                    px[k, count + offset - 1] = b - sv;
                    sv += px[k, count + offset - 1];
                    s += sv;
                    count += offset;
                }
             //   richTextBox1.AppendText("\n");
                
              
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            K = Convert.ToInt32(numericUpDown1.Value);
            int k = Convert.ToInt32(numericUpDown2.Value) - 1;
            Calc_px(K);
            Calc_pxy(K);
            Output_px(k);
            Output_pxy(k);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown2.Maximum = numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int k = Convert.ToInt32(numericUpDown2.Value) - 1;
            Output_px(k);
            Output_pxy(k);
        }
    }
}
