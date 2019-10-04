//хачу питсЫ
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
         int N = 28;
         double[,] px, py;
         double[] hx, hxy, ixy;
         double shx, shxy, sixy;
         double[,,] pxy, s_pxy;
         int round = 5;
         Random rand = new Random();
         int K = 0;
       // bool changed = false;
        public void Output_pxy(int k, bool bb)
        {
            if (bb) richTextBox2.Clear();
            double s = 0;
            richTextBox3.AppendText("p(x/y):\n");
            for (int i = 0; i < N; i++)
            {
                s = 0;
                for (int j = 0; j < N; j++)
                {
                    if (bb) richTextBox2.AppendText(String.Format("{0:f17} ", pxy[k, i, j]));
                    s += pxy[k, i, j];
                }
                if (bb) richTextBox2.AppendText("\n");
                richTextBox3.AppendText("s[" + String.Format("{0:d2}", i + 1) + "] = " + s.ToString() + "\n");
            }
            richTextBox3.AppendText("\n");
            if (bb)
            for (int i = 0; i < N; i++)
            {
                richTextBox2.Find(String.Format("{0:f17} ", pxy[k, i, i]));
                richTextBox2.SelectionColor = Color.Red;
            }
        }

        public void Output_s_pxy(int k, bool bb)
        {
            if (bb) richTextBox4.Clear();
            double s = 0;
            richTextBox3.AppendText("s_p(x/y):\n");
            for (int i = 0; i < N; i++)
            {
                s = 0;
                for (int j = 0; j < N; j++)
                {
                   if (bb) richTextBox4.AppendText(String.Format("{0:f17} ", s_pxy[k, i, j]));
                    s += s_pxy[k, i, j];
                }
                if (bb) richTextBox4.AppendText("\n");
                richTextBox3.AppendText("s[" + String.Format("{0:d2}", i + 1) + "] = " + s.ToString() + "\n");
            }
            richTextBox3.AppendText("\n");
            if (bb)
            for (int i = 0; i < N; i++)
            {
                richTextBox4.Find(String.Format("{0:f17} ", s_pxy[k, i, i]));
                richTextBox4.SelectionColor = Color.Red;
            }
        }

        public void Output_px(int k)
        {
            richTextBox3.Clear();
            richTextBox1.Clear();
            richTextBox1.AppendText("px : \n");
            double s = 0;
            for (int i = 0; i < N; i++)
            {
                richTextBox1.AppendText("px[" + String.Format("{0:d2}", i + 1) + "] = " + Math.Round(px[k, i], round).ToString() + "\n");
                s += px[k, i];
            }
            richTextBox1.AppendText("\n");
            richTextBox3.AppendText("p(x): \n" + "s = " + s.ToString() + "\n\n");
        }

        public void Output_py(int k)
        {
            richTextBox1.AppendText("py : \n");
            double s = 0;
            for (int i = 0; i < N; i++)
            {
                richTextBox1.AppendText("py[" + String.Format("{0:d2}", i + 1) + "] = " + Math.Round(py[k, i], round).ToString() + "\n");
                s += px[k, i];
            }
            richTextBox3.AppendText("p(y): \n" + "s = " + s.ToString() + "\n\n");
        }

        public void Output_rez(int kv)
        {
            richTextBox5.Clear();
            for (int k = 0; k < kv; k++)
            {
                richTextBox5.AppendText("Эксперимент " + (k + 1).ToString() + ": \n");
                richTextBox5.AppendText("H(x) = " + Math.Round(hx[k], round).ToString() + "\n");
                richTextBox5.AppendText("H(X/Y) = " + Math.Round(hxy[k], round).ToString() + "\n");
                richTextBox5.AppendText("I(X,Y) = " + Math.Round(ixy[k], round).ToString() + "\n\n");
            }
            textBox1.Text = Math.Round(shx, round).ToString();
            textBox2.Text = Math.Round(shxy, round).ToString();
            textBox3.Text = Math.Round(sixy, round).ToString();
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

        public void Calc_s_pxy(int kv)
        {
            s_pxy = new double[kv, N, N];
            for (int k = 0; k < K; k++)
            {
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        s_pxy[k, i, j] = py[k, j] * pxy[k, i, j];
            }
        }

        public void Calc_px(int kv)
        {
            richTextBox1.Clear();
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
            }
        }

        public void Calc_py(int kv)
        {
            py = new double[kv, N];
            for (int k = 0; k < kv; k++)
            {
                for (int i = 0; i < N; i++) py[k, i] = 0;
                for (int j = 0; j < N; j++)
                {
                    for (int i = 0; i < N; i++)
                    {
                        py[k, j] += px[k, i] * pxy[k, i, j];
                    }
                }
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            round = Convert.ToInt32(numericUpDown3.Value);
            int k = Convert.ToInt32(numericUpDown2.Value) - 1;
            Output_rez(K);
            Output_px(k);
            Output_py(k);
            Output_pxy(k, false);
            Output_s_pxy(k, false);
        }

        public void Calc_Hx(int kv)
        {
            hx = new double[kv];
            shx = 0;
            for (int k = 0; k < kv; k++)
            {
                hx[k] = 0;
                for (int i = 0; i < N; i++) hx[k] -= px[k, i] * Math.Log(px[k, i], 2);
                shx += hx[k];
            }
            shx /= kv;
            
        }

        public void Calc_Hxy(int kv)
        {
            hxy = new double[kv];
            shxy = 0;
            for (int k = 0; k < kv; k++)
            {
                hxy[k] = 0;
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        hxy[k] -= s_pxy[k, i, j] * Math.Log(pxy[k, i, j], 2);
                shxy += hxy[k];
            }
            shxy /= kv;
        }

        public void Calc_Ixy(int kv)
        {
            ixy = new double[kv];
            sixy = 0;
            for (int k = 0; k < kv; k++)
            {
                ixy[k] = hx[k] - hxy[k];
                sixy += ixy[k];
            }
            sixy /= kv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            numericUpDown2.Maximum = numericUpDown1.Value;
            numericUpDown2.Value = 1;
            K = Convert.ToInt32(numericUpDown1.Value);
            int k = Convert.ToInt32(numericUpDown2.Value) - 1;
            round = Convert.ToInt32(numericUpDown3.Value);
            Calc_px(K);
            Calc_pxy(K);
            Calc_py(K);
            Calc_s_pxy(K);
            Calc_Hx(K);
            Calc_Hxy(K);
            Calc_Ixy(K);
            Output_rez(K);
            Output_px(k);
            Output_py(k);
            Output_pxy(k, true);
            Output_s_pxy(k, true);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           // numericUpDown2.Maximum = numericUpDown1.Value;

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int k = Convert.ToInt32(numericUpDown2.Value) - 1;
            Output_px(k);
            Output_py(k);
            Output_pxy(k, true);
            Output_s_pxy(k, true);
        }
    }
}
