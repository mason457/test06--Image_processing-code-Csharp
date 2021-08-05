using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _Dhw2
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Bitmap newbmp;
        int[,] img;
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Bmp File(*.bmp)|*.bmp|jpg File(*.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp = new Bitmap(openFileDialog1.FileName);                //圖片像素資料存於變數bmp
                pictureBox1.Image = bmp;                                   //顯示於pictureBox1.
                img = hw2.BmpToAry.Transfer(bmp);                         //將相速資料置入test.BmpToAry.Transfer函式，輸出陣列img            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                newbmp.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Bmp);   //圖檔newbmp輸出
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                int HEIGHT = img.GetLength(0);
                int WIDTH = img.GetLength(1);
                //int value = int.Parse(textBox1.Text);
                for (int i = 0; i < HEIGHT; i++)
                {
                    for (int j = 0; j < WIDTH; j++)
                    {
                        img[i, j] = 255-img[i,j];
                    }
                }
                newbmp = hw2.BmpToAry.Invert(img);
                pictureBox2.Image = newbmp;
            }
            else
            {
                MessageBox.Show("請先載入圖形");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                int i, j;

                Color c = new Color();
                //Color cc = new Color();


                Bitmap box1 = new Bitmap(pictureBox1.Image);
                //Bitmap box2 = new Bitmap(pictureBox2.Image);
                double ws, a = 2;
                int kp, kq, sr;
                int sg, k1, k2, sb, ki, kj;
                float m, n;

                int[,] r1 = new int[256, 256];
                int[,] g1 = new int[256, 256];
                int[,] b1 = new int[256, 256];
                int[,] r2 = new int[256, 256];
                int[,] g2 = new int[256, 256];
                int[,] b2 = new int[256, 256];
                float[,] h1 = new float[256, 256];
                float[,] h2 = new float[256, 256];
                float[,] h3 = new float[256, 256];

                //3*3遮罩
                h1[0, 0] = 0.11f; h1[0, 1] = 0.11f; h1[0, 2] = 0.11f;
                h1[1, 0] = 0.11f; h1[1, 1] = 0.11f; h1[1, 2] = 0.11f;
                h1[2, 0] = 0.11f; h1[2, 1] = 0.11f; h1[2, 2] = 0.11f;

                int HEIGHT = img.GetLength(0);
                int WIDTH = img.GetLength(1);

                for (i = 0; i <= HEIGHT - 1; i++)
                {
                    for (j = 0; j <= WIDTH - 1; j++)
                    {
                        c = box1.GetPixel(i, j);
                        r1[i, j] = c.R;
                        g1[i, j] = c.G;
                        b1[i, j] = c.B;
                    }
                }
                m = 128 * 2; n = 128 * 2;
                for (k1 = 1; k1 < HEIGHT - 1; k1++)
                {
                    for (k2 = 1; k2 < WIDTH - 1; k2++)
                    {
                        sr = 0; sg = 0;
                        sb = 0;
                        for (i = 0; i <= 2; i++)
                        {
                            for (j = 0; j <= 2; j++)
                            {
                                ws = 2 * 3.14 * a * a;
                                (i * i + j * j) / 2 * a * a;
                                ki = k1 - i;
                                kj = k2 - j;
                                if ((ki >= 0) & (ki < m) & (kj >= 0) & (kj < n))
                                {
                                    sr = sr + (int)(r1[ki, kj] * h1[i, j]);
                                    sg = sg + (int)(g1[ki, kj] * h1[i, j]);
                                    sb = sb + (int)(b1[ki, kj] * h1[i, j]);
                                }
                            }
                        }
                        kp = k1 - 1;
                        kq = k2 - 1;
                        r2[kp, kq] = sr;
                        g2[kp, kq] = sg;
                        b2[kp, kq] = sb;

                        Color c1 = Color.FromArgb(r2[kp, kq], g2[kp, kq], b2[kp, kq]);
                        box1.SetPixel(k1, k2, c1);
                    }
                    pictureBox2.Refresh();
                    pictureBox2.Image = box1;
                }
            }

            else
            {
                MessageBox.Show("請先載入圖形");
            }
        }
        

        private void button5_Click(object sender, EventArgs e)  //低通濾波
        {
           
            if (pictureBox1.Image != null)
            {
                int i, j;
                const int typeA = 3;
                const int typeB = 5;
                const int typeC = 7;
                int type = 0;
                type = int.Parse(textBox1.Text);

                Color c = new Color();
                //Color cc = new Color();


                Bitmap box1 = new Bitmap(pictureBox1.Image);
                //Bitmap box2 = new Bitmap(pictureBox2.Image);
                int kp, kq, sr;
                int sg, k1, k2, sb, ki, kj;
                float m, n;

                int[,] r1 = new int[256, 256];
                int[,] g1 = new int[256, 256];
                int[,] b1 = new int[256, 256];
                int[,] r2 = new int[256, 256];
                int[,] g2 = new int[256, 256];
                int[,] b2 = new int[256, 256];
                float[,] h1 = new float[256, 256];
                float[,] h2 = new float[256, 256];
                float[,] h3 = new float[256, 256];

                //3*3遮罩
                h1[0, 0] = 0.11f; h1[0, 1] = 0.11f; h1[0, 2] = 0.11f;
                h1[1, 0] = 0.11f; h1[1, 1] = 0.11f; h1[1, 2] = 0.11f;
                h1[2, 0] = 0.11f; h1[2, 1] = 0.11f; h1[2, 2] = 0.11f;

                //5*5遮罩
                h2[0, 0] = 0.04f; h2[0, 1] = 0.04f; h2[0, 2] = 0.04f; h2[0, 3] = 0.04f; h2[0, 4] = 0.04f;
                h2[1, 0] = 0.04f; h2[1, 1] = 0.04f; h2[1, 2] = 0.04f; h2[1, 3] = 0.04f; h2[1, 4] = 0.04f;
                h2[2, 0] = 0.04f; h2[2, 1] = 0.04f; h2[2, 2] = 0.04f; h2[2, 3] = 0.04f; h2[2, 4] = 0.04f;
                h2[3, 0] = 0.04f; h2[3, 1] = 0.04f; h2[3, 2] = 0.04f; h2[3, 3] = 0.04f; h2[3, 4] = 0.04f;
                h2[4, 0] = 0.04f; h2[4, 1] = 0.04f; h2[4, 2] = 0.04f; h2[4, 3] = 0.04f; h2[4, 4] = 0.04f;

                //7*7遮罩
                h3[0, 0] = 0.02f; h3[0, 1] = 0.02f; h3[0, 2] = 0.02f; h3[0, 3] = 0.02f; h3[0, 4] = 0.02f; h3[0, 5] = 0.02f; h3[0, 6] = 0.02f;
                h3[1, 0] = 0.02f; h3[1, 1] = 0.02f; h3[1, 2] = 0.02f; h3[1, 3] = 0.02f; h3[1, 4] = 0.02f; h3[1, 5] = 0.02f; h3[1, 6] = 0.02f;
                h3[2, 0] = 0.02f; h3[2, 1] = 0.02f; h3[2, 2] = 0.02f; h3[2, 3] = 0.02f; h3[2, 4] = 0.02f; h3[2, 5] = 0.02f; h3[2, 6] = 0.02f;
                h3[3, 0] = 0.02f; h3[3, 1] = 0.02f; h3[3, 2] = 0.02f; h3[3, 3] = 0.02f; h3[3, 4] = 0.02f; h3[3, 5] = 0.02f; h3[3, 6] = 0.02f;
                h3[4, 0] = 0.02f; h3[4, 1] = 0.02f; h3[4, 2] = 0.02f; h3[4, 3] = 0.02f; h3[4, 4] = 0.02f; h3[4, 5] = 0.02f; h3[4, 6] = 0.02f;
                h3[5, 0] = 0.02f; h3[5, 1] = 0.02f; h3[5, 2] = 0.02f; h3[5, 3] = 0.02f; h3[5, 4] = 0.02f; h3[5, 5] = 0.02f; h3[5, 6] = 0.02f;
                h3[6, 0] = 0.02f; h3[6, 1] = 0.02f; h3[6, 2] = 0.02f; h3[6, 3] = 0.02f; h3[6, 4] = 0.02f; h3[6, 5] = 0.02f; h3[6, 6] = 0.02f;

                int HEIGHT = img.GetLength(0);
                int WIDTH = img.GetLength(1);

                for (i = 0; i <= HEIGHT - 1; i++)
                {
                    for (j = 0; j <= WIDTH - 1; j++)
                    {
                        c = box1.GetPixel(i, j);
                        r1[i, j] = c.R;
                        g1[i, j] = c.G;
                        b1[i, j] = c.B;
                    }
                }
                m = 128 * 2; n = 128 * 2;
                for (k1 = 1; k1 < HEIGHT - 1; k1++)
                {
                    for (k2 = 1; k2 < WIDTH - 1; k2++)
                    {
                        sr = 0; sg = 0;
                        sb = 0;

                        switch (type)
                        {
                            case typeA: //3*3
                                for (i = 0; i <= 2; i++)
                                {
                                    for (j = 0; j <= 2; j++)
                                    {
                                        ki = k1 - i;
                                        kj = k2 - j;
                                        if ((ki >= 0) & (ki < m) & (kj >= 0) & (kj < n))
                                        {
                                            sr = sr + (int)(r1[ki, kj] * h1[i, j]);
                                            sg = sg + (int)(g1[ki, kj] * h1[i, j]);
                                            sb = sb + (int)(b1[ki, kj] * h1[i, j]);
                                        }
                                    }
                                }
                                break;

                            case typeB: //5*5
                                for (i = 0; i <= 4; i++)
                                {
                                    for (j = 0; j <= 4; j++)
                                    {
                                        ki = k1 - i;
                                        kj = k2 - j;
                                        if ((ki >= 0) & (ki < m) & (kj >= 0) & (kj < n))
                                        {
                                            sr = sr + (int)(r1[ki, kj] * h2[i, j]);
                                            sg = sg + (int)(g1[ki, kj] * h2[i, j]);
                                            sb = sb + (int)(b1[ki, kj] * h2[i, j]);
                                        }
                                    }
                                }
                                break;

                            case typeC:  //7*7
                                for (i = 0; i <= 6; i++)
                                {
                                    for (j = 0; j <= 6; j++)
                                    {
                                        ki = k1 - i;
                                        kj = k2 - j;
                                        if ((ki >= 0) & (ki < m) & (kj >= 0) & (kj < n))
                                        {
                                            sr = sr + (int)(r1[ki, kj] * h3[i, j]);
                                            sg = sg + (int)(g1[ki, kj] * h3[i, j]);
                                            sb = sb + (int)(b1[ki, kj] * h3[i, j]);
                                        }
                                    }
                                }
                                break;
                        }


                        kp = k1 - 1;
                        kq = k2 - 1;
                        r2[kp, kq] = sr;
                        g2[kp, kq] = sg;
                        b2[kp, kq] = sb;

                        Color c1 = Color.FromArgb(r2[kp, kq], g2[kp, kq], b2[kp, kq]);
                        box1.SetPixel(k1, k2, c1);
                    }
                    pictureBox2.Refresh();
                    pictureBox2.Image = box1;
                }
            }
            else
            {
                MessageBox.Show("請先載入圖形");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}