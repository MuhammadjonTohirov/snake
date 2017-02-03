using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using med = System.Media;

namespace Snake2
{
    public partial class FormMain : Form
    {
        public void SaveSore(string message)
        {
            using(FileStream FS = new FileStream("Base.txt",FileMode.Append,FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(FS);
                sw.Write(message+",");
                sw.Flush();
                sw.Close();
                sw.Dispose();
            }
        }
        public int big;
        public bool GameOver;
        private void ReadData()
        {
            using (FileStream fs = new FileStream("Base.txt", FileMode.Open, FileAccess.Read))
            {
                string temp = "";
                string data;
                StreamReader sr = new StreamReader(fs);
                List<int> scores = new List<int>();
                data = sr.ReadToEnd();
                for (int i = 0; i < data.Length; i++)
                {
                    temp += data[i];
                    if (data[i] == ',')
                    {
                        temp = temp.Remove(temp.Length - 1);
                        scores.Add(Convert.ToInt32(temp));
                        temp = "";
                    }
                }
                big = Convert.ToInt32(scores[0]);
                for (int i = 0; i < scores.Count; i++)
                {
                    if(Convert.ToInt32(scores[i])>big)
                    {
                        big=Convert.ToInt32(scores[i]);
                    }
                }

            }
        }
        public FormMain()
        {
            PlayMusic();
            Length = 0;
            InitializeComponent();
            snake.Add(panelHead);
            snake.Add(panelD1);
            snake.Add(panelD2);
            snake.Add(panelD3);
            putFood();
            GameOver = false;
            ReadData();
            label6.Text = Convert.ToString(big);
            labelN.Text = SystemInformation.UserName;

        }
        private string CountLen()
        {
            return Convert.ToString(snake.Count);
        }
        public Panel panelF;
        private void PlayMusic()
        {
            {
                med.SoundPlayer sp = new med.SoundPlayer("onthe.wav");
                sp.Play();
                sp.Dispose();
            }
        }
        private void putFood()
        {
            Random col = new Random();
            panelF = new Panel();
            Random X1 = new Random();
            panelF.BackColor = System.Drawing.Color.FromArgb(col.Next(0, 200), col.Next(5, 150), col.Next(1, 200));
            panelF.Dock = System.Windows.Forms.DockStyle.Fill;
            panelF.Size = new System.Drawing.Size(14, 14);
            int X = X1.Next(0, tableLayoutPanel1.ColumnCount - 1), Y = X1.Next(0, tableLayoutPanel1.RowCount - 1);
            for (int i = 0; i < snake.Count; i++)
            {
                while(X == tableLayoutPanel1.GetColumn(snake[i]) && Y == tableLayoutPanel1.GetRow(snake[i]))
                {
                    X = X1.Next(0, tableLayoutPanel1.ColumnCount - 1); Y = X1.Next(0, tableLayoutPanel1.RowCount - 1);
                }
            }
            tableLayoutPanel1.Controls.Add(panelF, X, Y);
            label7.Text = (string.Format("{0}:{1}",Convert.ToString( X),Convert.ToString(Y)));
        }
        public  int defX = 3;
        public int defY = 1;
        //**************************************************
        private void FormMain_Load(object sender, EventArgs e)
        {
            
        }
        //**************************************************
        private void labelQ_Click(object sender, EventArgs e)
        {
            med.SoundPlayer sp = new med.SoundPlayer("Leave.wav");
            sp.Play();
            sp.Dispose();
            for (int i = 0; i < 209999999; i++) ;
                Application.Exit();
        }
        //**************************************************
        private void labelM_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void FormMain_Leave(object sender, EventArgs e)
        {
            panelF.Dispose();
        }
        public List<Panel> snake = new List<Panel>();
        public int KeyVal;
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            defX = tableLayoutPanel1.GetColumn(panelHead);
            defY = tableLayoutPanel1.GetRow(panelHead);
            if (e.KeyValue == 'D' || e.KeyValue == 'd')
            {
                KeyVal = e.KeyValue;
/*                if (tableLayoutPanel1.GetColumn(snake[0]) == tableLayoutPanel1.ColumnCount - 1)
                    newPos = new TableLayoutPanelCellPosition() { Column = 0, Row = defY };
                else
                    newPos = new TableLayoutPanelCellPosition() { Column = defX + 1, Row = defY };
                move(newPos);*/
            }
            if (e.KeyValue == 'S' || e.KeyValue == 's')
            {
                KeyVal = e.KeyValue;
            /*    if (tableLayoutPanel1.GetRow(snake[0]) == tableLayoutPanel1.RowCount - 1)
                    newPos = new TableLayoutPanelCellPosition() { Column = defX, Row = 0};
                else
                    newPos = new TableLayoutPanelCellPosition() { Column = defX, Row = defY + 1};
                move(newPos);*/
            }
            if (e.KeyValue == 'A' || e.KeyValue == 'a')
            {
                KeyVal = e.KeyValue;
                /*
                if (tableLayoutPanel1.GetColumn(snake[0]) == 0)
                    newPos = new TableLayoutPanelCellPosition() { Column = tableLayoutPanel1.ColumnCount - 1, Row = defY};
                else
                    newPos = new TableLayoutPanelCellPosition() { Column = defX - 1, Row = defY };
                move(newPos);
                 */
            }
            if (e.KeyValue == 'W' || e.KeyValue == 'w')
            {
                KeyVal = e.KeyValue;
                /*

                if (tableLayoutPanel1.GetRow(snake[0]) == 0)
                    newPos = new TableLayoutPanelCellPosition() { Column = defX, Row = tableLayoutPanel1.RowCount - 1 };
                else
                    newPos = new TableLayoutPanelCellPosition() { Column = defX, Row = defY - 1 };
                move(newPos);   
                 */
            }
            Length = 0;
            
        }

        private void move(TableLayoutPanelCellPosition newPos)
        {
            TableLayoutPanelCellPosition temp = tableLayoutPanel1.GetPositionFromControl(snake[0]);
            if (newPos == tableLayoutPanel1.GetPositionFromControl(panelF))
            {
                med.SoundPlayer sp = new med.SoundPlayer("add.WAV");
                sp.Play();
                panelF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), 
                    ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                snake.Add(panelF);
                putFood();
                tableLayoutPanel1.SetCellPosition(snake[0], newPos);
                movePos(temp);
                sp.Dispose();
            }

            else if (tableLayoutPanel1.GetControlFromPosition(newPos.Column, newPos.Row) == null)
            {
                tableLayoutPanel1.SetCellPosition(snake[0], newPos);
                movePos(temp);
                label2.Text = "Started"; 
            }
            else if (newPos == tableLayoutPanel1.GetPositionFromControl(snake[1]))
            { GameOver = false; label2.Text = "Paused"; }
            else GameOver = true;
            labelLen.Text = CountLen();
        }

        private void movePos(TableLayoutPanelCellPosition pos)
        {
            TableLayoutPanelCellPosition temp;
            for (int i = 1; i < snake.Count; i++)
            {
                temp = tableLayoutPanel1.GetPositionFromControl(snake[i]);
                tableLayoutPanel1.SetCellPosition(snake[i], pos);
                pos = temp;
            }
        }

        private void panelD2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelQ_MouseDown(object sender, MouseEventArgs e)
        {
            labelQ.Left += 1;
            labelQ.Top += 1;
        }

        private void labelQ_MouseUp(object sender, MouseEventArgs e)
        {
            labelQ.Left -= 1;
            labelQ.Top -= 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GameOver == true)
            {
                timer1.Enabled = false;
                DialogResult = MessageBox.Show(string.Format("Your Sore is {0}. Dou you want to restart the game ", snake.Count), "Game Over", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (DialogResult == DialogResult.Cancel)
                {
                    SaveSore(labelLen.Text);
                    Application.Exit();
                }
                else
                {
                    SaveSore(labelLen.Text);
                    Application.Restart();
                }
            }
            else
            {
                label4.Text = Convert.ToString(Convert.ToDouble((10000 / timer1.Interval)) + " mph");
                labelSpeed.Text = Convert.ToString(tableLayoutPanel1.GetPositionFromControl(panelHead));
                defX = tableLayoutPanel1.GetColumn(panelHead);
                defY = tableLayoutPanel1.GetRow(panelHead);
                TableLayoutPanelCellPosition newPos;
                if (KeyVal == 'D' || KeyVal == 'd')
                {
                    if (tableLayoutPanel1.GetColumn(snake[0]) == tableLayoutPanel1.ColumnCount - 1)
                        newPos = new TableLayoutPanelCellPosition() { Column = 0, Row = defY };
                    else
                        newPos = new TableLayoutPanelCellPosition() { Column = defX + 1, Row = defY };
                    move(newPos);
                }

                if (KeyVal == 'S' || KeyVal == 's')
                {
                    if (tableLayoutPanel1.GetRow(snake[0]) == tableLayoutPanel1.RowCount - 1)
                        newPos = new TableLayoutPanelCellPosition() { Column = defX, Row = 0 };
                    else
                        newPos = new TableLayoutPanelCellPosition() { Column = defX, Row = defY + 1 };
                    move(newPos);
                }

                if (KeyVal == 'A' || KeyVal == 'a')
                {
                    if (tableLayoutPanel1.GetColumn(snake[0]) == 0)
                        newPos = new TableLayoutPanelCellPosition() { Column = tableLayoutPanel1.ColumnCount - 1, Row = defY };
                    else
                        newPos = new TableLayoutPanelCellPosition() { Column = defX - 1, Row = defY };
                    move(newPos);
                }
                if (KeyVal == 'W' || KeyVal == 'w')
                {
                    if (tableLayoutPanel1.GetRow(snake[0]) == 0)
                        newPos = new TableLayoutPanelCellPosition() { Column = defX, Row = tableLayoutPanel1.RowCount - 1 };
                    else
                        newPos = new TableLayoutPanelCellPosition() { Column = defX, Row = defY - 1 };
                    move(newPos);
                }

                if (Convert.ToInt32(labelLen.Text) >= (25) && Convert.ToInt32(labelLen.Text) <= 45)
                {
                    timer1.Interval = 100;
                }
                else if (Convert.ToInt32(labelLen.Text) >= (45) && Convert.ToInt32(labelLen.Text) <= 65)
                {
                    timer1.Interval = 80;

                }
                else if (Convert.ToInt32(labelLen.Text) >= (65) && Convert.ToInt32(labelLen.Text) <= 90)
                {
                    timer1.Interval = 75;
                }
                else if (Convert.ToInt32(labelLen.Text) >= (90) && Convert.ToInt32(labelLen.Text) <= 120)
                {
                    timer1.Interval = 65;
                }
            }
        }
        public int Length;

        private void labelM_MouseDown(object sender, MouseEventArgs e)
        {
            labelM.Left += 1;
            labelM.Top += 1;
        }

        private void labelM_MouseUp(object sender, MouseEventArgs e)
        {
            labelM.Top -= 1;
            labelM.Top -= 1;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

    }
}
