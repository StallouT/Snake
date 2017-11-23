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
        int Point, way = 1, x, y;

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

            dataGridView1.ClearSelection();
            fieldZeroPosition();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        public void clearBody()
        {
            bodyX.Clear();
            bodyY.Clear();
            label1.Text = String.Format("Ваш счёт: 0");
        }

        public void addBody()
        {
            bodyX.AddRange(new int[] { 16, 15, 14 });
            bodyY.AddRange(new int[] { 14, 14, 14 });
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
            int x = rand.Next(0, 29),
                y = rand.Next(0, 29);
            if (dataGridView1.Rows[x].Cells[y].Style.BackColor == colorMain && x!= bodyX[0] && y!= bodyY[0])
            {
                dataGridView1.Rows[x].Cells[y].Style.BackColor = colorApple;
            }
            else createApple();
        }

        public void gameOver()
        {
            fieldZeroPosition();
            clearBody();
            Point = 0;

            SnakeMove.Enabled = false;
            label1.Visible = false;
            panel1.Visible = true;

            button1.Focus();
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

        protected virtual void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (SnakeMove.Enabled == false)
            {
                SnakeMove.Enabled = true;
            }
            switch (e.KeyCode)
            {
                case Keys.Left:
                    bodyX.Add(x);
                    bodyY.Add(y - 1);
                    if (bodyY[bodyY.Count - 1] == -1)
                    {
                        bodyY[bodyY.Count - 1] = 29;
                    }
                    way = 2;
                    test(bodyX, bodyY);
                    break;
                case Keys.Right:
                    bodyX.Add(x);
                    bodyY.Add(y + 1);
                    if (bodyY[bodyY.Count - 1] == 30)
                    {
                        bodyY[bodyY.Count - 1] = 0;
                    }
                    way = 4;
                    test(bodyX, bodyY);
                    break;
                case Keys.Up:
                    bodyX.Add(x - 1);
                    bodyY.Add(y);
                    if (bodyX[bodyX.Count - 1] == -1)
                    {
                        bodyX[bodyX.Count - 1] = 29;
                    }
                    way = 1;
                    test(bodyX, bodyY);
                    break;
                case Keys.Down:
                    bodyX.Add(x + 1);
                    bodyY.Add(y);
                    if (bodyX[bodyX.Count - 1] == 30)
                    {
                        bodyX[bodyX.Count - 1] = 0;
                    }
                    way = 3;
                    test(bodyX, bodyY);
                    break;
                case Keys.Escape:
                    SnakeMove.Enabled = false;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addBody();
            way = 1;
            moveRedraw(this.bodyX, this.bodyY, true);
            createApple();
            dataGridView1.Focus();

            label1.Visible = true;
            panel1.Visible = false;
            SnakeMove.Enabled = true;
        }         

        private void SnakeMove_Tick(object sender, EventArgs e)
        {
            switch (way)
            {
                case 1:
                    bodyX.Add(x - 1);
                    bodyY.Add(y);
                    if (bodyX[bodyX.Count - 1] == -1)
                    {
                        bodyX[bodyX.Count - 1] = 29;
                    }
                    test(bodyX, bodyY);
                    break;
                case 2:
                    bodyX.Add(x);
                    bodyY.Add(y - 1);
                    if (bodyY[bodyY.Count - 1] == -1)
                    {
                        bodyY[bodyY.Count - 1] = 29;
                    }
                    test(bodyX, bodyY);
                    break;
                case 3:
                    bodyX.Add(x + 1);
                    bodyY.Add(y);
                    if (bodyX[bodyX.Count - 1] == 30)
                    {
                        bodyX[bodyX.Count - 1] = 0;
                    }
                    test(bodyX, bodyY);
                    break;
                case 4:
                    bodyX.Add(x);
                    bodyY.Add(y + 1);
                    if (bodyY[bodyY.Count - 1] == 30)
                    {
                        bodyY[bodyY.Count - 1] = 0;
                    }
                    test(bodyX, bodyY);
                    break;
            }
        }
    }
}