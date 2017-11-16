using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        int c = 0, x, y;
        int Point = 0;
        int way = 1;

        Color colorHead = Color.FromArgb(42, 42, 42);
        Color colorBody = Color.FromArgb(64, 64, 64);
        Color colorMain = Color.FromArgb(8, 8, 8);
        Color colorApple = Color.FromArgb(158, 158, 158);

        List<int> bodyX = new List<int>();
        List<int> bodyY = new List<int>();

        public Form1()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 30;
            dataGridView1.RowCount = 30;

            dataGridView2.ColumnCount = 7;
            dataGridView2.RowCount = 7;

            clearBody();
            fieldZeroPosition();
        }

        public void clearBody()
        {
            bodyX.Clear();
            bodyY.Clear();
            bodyX.AddRange(new int[] { 16, 15, 14 });
            bodyY.AddRange(new int[] { 14, 14, 14 });
            label1.Text = String.Format("Ваш счёт: 0");
        }


        public void fieldZeroPosition()
        {
            int a = 0, b = 0;
            while (a < 30)
            {
                while (b < 30)
                {
                    dataGridView1.Rows[a].Cells[b].Style.BackColor = colorMain;
                    b++;
                }
                b = 0;
                a++;
            }          
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    bodyX.Add(x);
                    bodyY.Add(y - 1);
                    if (bodyY[bodyY.Count - 1] == -1) bodyY[bodyY.Count - 1] = 29;
                    way = 2;
                    test(bodyX, bodyY);
                    break;
                case Keys.Right:
                    bodyX.Add(x);
                    bodyY.Add(y + 1);
                    if (bodyY[bodyY.Count - 1] == 30) bodyY[bodyY.Count - 1] = 0; 
                    way = 4;
                    test(bodyX, bodyY);
                    break;
                case Keys.Up:
                    bodyX.Add(x - 1);
                    bodyY.Add(y);
                    if (bodyX[bodyX.Count - 1] == -1) bodyX[bodyX.Count - 1] = 29;
                    way = 1;
                    test(bodyX, bodyY);
                    break;
                case Keys.Down:
                    bodyX.Add(x + 1);
                    bodyY.Add(y);
                    if (bodyX[bodyX.Count - 1] == 30) bodyX[bodyX.Count - 1] = 0;
                    way = 3;
                    test(bodyX, bodyY);
                    break;
                default: return base.ProcessCmdKey(ref msg, keyData);
            }
            return true;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            panel1.Visible = false;
            timer1.Enabled = false;

            moveRedraw(this.bodyX, this.bodyY, true);
            createApple();

            timer2.Enabled = true;
        }

        public void createApple()
        {
            Random rand = new Random();
            dataGridView1.Rows[rand.Next(1, 28)].Cells[rand.Next(1, 28)].Style.BackColor = colorApple;
        }

        public void gameOver()
        {
            timer2.Enabled = false;
            Point = 0;
            label1.Visible = false;
            fieldZeroPosition();
            clearBody();
            panel1.Visible = true;
            timer1.Enabled = true;
        }

        public void test(List<int> bodyX, List<int> bodyY)
        {
            try
            {
                if (dataGridView1.Rows[bodyX[bodyX.Count - 1]].Cells[bodyY[bodyY.Count - 1]].Style.BackColor != colorMain
                    &&
                    dataGridView1.Rows[bodyX[bodyX.Count - 1]].Cells[bodyY[bodyY.Count - 1]].Style.BackColor != colorApple
                    ||
                    dataGridView1.Rows[bodyX[bodyX.Count - 1]].Cells[bodyY[bodyY.Count - 1]].Style.BackColor == colorBody)
                {
                    gameOver();
                }
                else
                {
                    if (dataGridView1.Rows[bodyX[bodyX.Count - 1]].Cells[bodyY[bodyY.Count - 1]].Style.BackColor != colorApple)
                    {
                        moveRedraw(bodyX, bodyY, false);
                    }
                    else
                    {
                        moveRedraw(bodyX, bodyY, true);
                        label1.Text = String.Format("Ваш счёт: {0}", ++Point);
                        createApple();
                    }
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
                gameOver();
            }

            
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            switch (way)
            {
                case 1:
                    bodyX.Add(x - 1);
                    bodyY.Add(y);
                    if (bodyX[bodyX.Count - 1] == -1) bodyX[bodyX.Count - 1] = 29;
                    test(bodyX, bodyY);
                    break;
                case 2:
                    bodyX.Add(x);
                    bodyY.Add(y - 1);
                    if (bodyY[bodyY.Count - 1] == -1) bodyY[bodyY.Count - 1] = 29;
                    test(bodyX, bodyY);
                    break;
                case 3:
                    bodyX.Add(x + 1);
                    bodyY.Add(y);
                    if (bodyX[bodyX.Count - 1] == 30) bodyX[bodyX.Count - 1] = 0;
                    test(bodyX, bodyY);
                    break;
                case 4:
                    bodyX.Add(x);
                    bodyY.Add(y + 1);
                    if (bodyY[bodyY.Count - 1] == 30) bodyY[bodyY.Count - 1] = 0;
                    test(bodyX, bodyY);
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorHead = Color.FromArgb(42, 42, 42);
            colorBody = Color.FromArgb(64, 64, 64);
            colorMain = Color.FromArgb(8, 8, 8);
            colorApple = Color.FromArgb(158, 158, 158);
            timer3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorHead = Color.FromArgb(0, 130, 0);
            colorBody = Color.FromArgb(0, 200, 0);
            colorMain = Color.FromArgb(8, 8, 8);
            colorApple = Color.FromArgb(222, 4, 3);
            timer3.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            colorHead = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            colorBody = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            colorMain = Color.FromArgb(8, 8, 8);
            colorApple = Color.FromArgb(158, 158, 158);
            timer3.Enabled = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            colorHead = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            colorBody = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }

        public void moveRedraw(List<int> bodyX, List<int> bodyY, bool Apple)
        {
            int i = 0;

            while (i < bodyX.Count)
            {
                if (i == 0)
                {
                    dataGridView1.Rows[bodyX[i]].Cells[bodyY[i]].Style.BackColor = colorMain;
                }
                else
                {
                    if (i == bodyX.Count - 2)
                    {
                        dataGridView1.Rows[bodyX[i]].Cells[bodyY[i]].Style.BackColor = colorBody;
                    }
                    else
                    {
                        if (i == bodyX.Count - 1)
                        {
                            dataGridView1.Rows[bodyX[i]].Cells[bodyY[i]].Style.BackColor = colorHead;

                            x = bodyX[i];
                            y = bodyY[i];
                        }
                    }
                }
                i++;
            }
            if (!Apple)
            {
                bodyX.RemoveAt(0);
                bodyY.RemoveAt(0);
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (c)
            {
                case 1:
                    dataGridView2.Rows[2].Cells[1].Style.BackColor = colorMain;

                    dataGridView2.Rows[1].Cells[3].Style.BackColor = colorHead;
                    dataGridView2.Rows[1].Cells[4].Style.BackColor = colorBody;
                    break;
                case 2:
                    dataGridView2.Rows[3].Cells[1].Style.BackColor = colorMain;

                    dataGridView2.Rows[1].Cells[2].Style.BackColor = colorHead;
                    dataGridView2.Rows[1].Cells[3].Style.BackColor = colorBody;
                    break;
                case 3:
                    dataGridView2.Rows[4].Cells[1].Style.BackColor = colorMain;

                    dataGridView2.Rows[1].Cells[1].Style.BackColor = colorHead;
                    dataGridView2.Rows[1].Cells[2].Style.BackColor = colorBody;
                    break;
                case 4:
                    dataGridView2.Rows[5].Cells[1].Style.BackColor = colorMain;

                    dataGridView2.Rows[2].Cells[1].Style.BackColor = colorHead;
                    dataGridView2.Rows[1].Cells[1].Style.BackColor = colorBody;
                    break;
                case 5:
                    dataGridView2.Rows[5].Cells[2].Style.BackColor = colorMain;

                    dataGridView2.Rows[3].Cells[1].Style.BackColor = colorHead;
                    dataGridView2.Rows[2].Cells[1].Style.BackColor = colorBody;
                    break;
                case 6:
                    dataGridView2.Rows[5].Cells[3].Style.BackColor = colorMain;

                    dataGridView2.Rows[4].Cells[1].Style.BackColor = colorHead;
                    dataGridView2.Rows[3].Cells[1].Style.BackColor = colorBody;
                    break;
                case 7:
                    dataGridView2.Rows[5].Cells[4].Style.BackColor = colorMain;

                    dataGridView2.Rows[5].Cells[1].Style.BackColor = colorHead;
                    dataGridView2.Rows[4].Cells[1].Style.BackColor = colorBody;
                    break;
                case 8:
                    dataGridView2.Rows[5].Cells[5].Style.BackColor = colorMain;

                    dataGridView2.Rows[5].Cells[2].Style.BackColor = colorHead;
                    dataGridView2.Rows[5].Cells[1].Style.BackColor = colorBody;
                    break;
                case 9:
                    dataGridView2.Rows[4].Cells[5].Style.BackColor = colorMain;

                    dataGridView2.Rows[5].Cells[3].Style.BackColor = colorHead;
                    dataGridView2.Rows[5].Cells[2].Style.BackColor = colorBody;
                    break;
                case 10:
                    dataGridView2.Rows[3].Cells[5].Style.BackColor = colorMain;

                    dataGridView2.Rows[5].Cells[4].Style.BackColor = colorHead;
                    dataGridView2.Rows[5].Cells[3].Style.BackColor = colorBody;
                    break;
                case 11:
                    dataGridView2.Rows[2].Cells[5].Style.BackColor = colorMain;

                    dataGridView2.Rows[5].Cells[5].Style.BackColor = colorHead;
                    dataGridView2.Rows[5].Cells[4].Style.BackColor = colorBody;
                    break;
                case 12:
                    dataGridView2.Rows[1].Cells[5].Style.BackColor = colorMain;

                    dataGridView2.Rows[4].Cells[5].Style.BackColor = colorHead;
                    dataGridView2.Rows[5].Cells[5].Style.BackColor = colorBody;
                    break;
                case 13:
                    dataGridView2.Rows[1].Cells[4].Style.BackColor = colorMain;

                    dataGridView2.Rows[3].Cells[5].Style.BackColor = colorHead;
                    dataGridView2.Rows[4].Cells[5].Style.BackColor = colorBody;
                    break;
                case 14:
                    dataGridView2.Rows[1].Cells[3].Style.BackColor = colorMain;

                    dataGridView2.Rows[2].Cells[5].Style.BackColor = colorHead;
                    dataGridView2.Rows[3].Cells[5].Style.BackColor = colorBody;
                    break;
                case 15:
                    dataGridView2.Rows[1].Cells[2].Style.BackColor = colorMain;

                    dataGridView2.Rows[1].Cells[5].Style.BackColor = colorHead;
                    dataGridView2.Rows[2].Cells[5].Style.BackColor = colorBody;
                    break;
                case 16:
                    dataGridView2.Rows[1].Cells[1].Style.BackColor = colorMain;

                    dataGridView2.Rows[1].Cells[4].Style.BackColor = colorHead;
                    dataGridView2.Rows[1].Cells[5].Style.BackColor = colorBody;
                    c = 0;
                    break;
            }
            c++;
        }
    }
}