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
        int clickCount,
            Point,
            way = 1,
            c,
            x,
            y;

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

        public void createApple()
        {
            Random rand = new Random();
            int x = rand.Next(1, 28),
                y = rand.Next(1, 28);
            if (dataGridView1.Rows[x].Cells[y].Style.BackColor == colorMain) dataGridView1.Rows[x].Cells[y].Style.BackColor = colorApple;
            else createApple();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (clickCount == 0)
            {
                button1.Visible = false;
                panel3.Visible = true;
                panel5.Visible = true;
                button2.Text = "Применить настройки";
                clickCount = 1;
            }
            else
            {
                button1.Visible = true;
                panel3.Visible = false;
                panel5.Visible = false;
                button2.Text = "Настройки";
                clickCount = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorHead = Color.FromArgb(42, 42, 42);
            colorBody = Color.FromArgb(64, 64, 64);
            colorApple = Color.FromArgb(158, 158, 158);
            timer3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorHead = Color.FromArgb(0, 130, 0);
            colorBody = Color.FromArgb(0, 200, 0);
            colorApple = Color.FromArgb(222, 4, 3);
            timer3.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            colorHead = Color.FromArgb(13, 13, 13);
            colorBody = Color.FromArgb(10, 10, 10);
            colorApple = Color.FromArgb(158, 158, 158);
            timer3.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            timer3.Enabled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fieldZeroPosition();
            int a = 0, b = 0;
            while (b < 29)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                b++;
            }
            b = 0;
            while (a < 29)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                a++;
            }
            a = 29;
            while (b < 29)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                b++;
            }
            a = 0;
            while (a < 30)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                a++;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            fieldZeroPosition();
            int a = 0, b = 0;
            while (b < 10)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                b++;
            }
            b = 20;
            while (b < 29)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                b++;
            }
            b = 0;
            while (a < 10)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                a++;
            }
            a = 20;
            while (a < 29)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                a++;
            }
            a = 29;
            while (b < 10)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                b++;
            }
            b = 20;
            while (b < 29)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                b++;
            }
            a = 0;
            while (a < 10)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                a++;
            }
            a = 20;
            while (a < 30)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                a++;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            fieldZeroPosition();
            int a = 5, b = 5;
            while (b < 20)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                b++;
            }
            b = 25;
            while (a < 20)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                a++;
            }
            a = 25;
            while (b > 10)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                b--;
            }
            b = 5;
            while (a > 10)
            {
                dataGridView1.Rows[a].Cells[b].Style.BackColor = colorBody;
                a--;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

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

        private void timer3_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int head = rand.Next(0, 255);
            int body = rand.Next(0, 255);
            colorHead = Color.FromArgb(head, head, head);
            colorBody = Color.FromArgb(body, body, body);
        }
    }
}