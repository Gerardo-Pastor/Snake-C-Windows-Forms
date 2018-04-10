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
        Random rand = new Random();
        Comida bit;
        List<Comida> snake = new List<Comida>();
        Graphics papel;
        Color background = Color.Black;
        int dirx, diry, x, y,score;
        bool gameOver, pausa;

        public Form1()
        {
            InitializeComponent();
            papel = pictureBox1.CreateGraphics();
            timer1.Interval = 50;
            button2.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                snake.Add(new Comida(x, y, rand));
                if ((bit.x == snake[snake.Count - 1].x) && (bit.y == snake[snake.Count - 1].y))
                {
                    bit = new Comida(rand.Next(0, 60), rand.Next(0, 45), rand);
                    score++;
                }
                else
                {
                    snake[0].desdibuja(papel, background);
                    snake.RemoveAt(0);
                }

                label1.Text = "Score: " + Convert.ToString(score);
                x += dirx;
                y += diry;
                foreach (Comida parte in snake)
                {
                    parte.dibujar(papel);
                    if ((parte.x == x*bit.size) && (parte.y == y*bit.size)) gameOver = true;

                    if ((x < 0 || x > 59) || (y < 0 || y > 44)) gameOver = true;
                }
                bit.dibujar(papel);
            }
            else
            {
                timer1.Stop();
                button2.Hide();
                button1.Show();
                papel.DrawEllipse(new Pen(Color.Red, 2), x*bit.size - 7, y*bit.size - 7, bit.size + 15, bit.size + 15);
                papel.DrawString("Game Over", new Font("Arial", 50), new SolidBrush(Color.Yellow), new PointF(120, 180));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            papel.Clear(background);
            rand = new Random(rand.Next(0, 1024));
            gameOver = false;
            pausa = false;
            score = 0;
            x = 30;
            y = 22;
            diry = -1;
            dirx = 0;
            snake = new List<Comida>();
            bit = new Comida(rand.Next(0, 60), rand.Next(0, 44), rand);
            for(int i  = 0; i < 4; i++)
                snake.Add(new Comida(x, y+i, rand));
            button1.Hide();
            button2.Show();
            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(dirx != 0)
            {
                if(e.KeyCode == Keys.W)
                {
                    diry = -1;
                    dirx = 0;
                }
                if (e.KeyCode == Keys.S)
                {
                    diry = 1;
                    dirx = 0;
                }
            }
            if (diry != 0)
            {
                if (e.KeyCode == Keys.A)
                {
                    diry = 0;
                    dirx = -1;
                }
                if (e.KeyCode == Keys.D)
                {
                    diry = 0;
                    dirx = 1;
                }
            }


        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (pausa)
            {
                timer1.Start();
                papel.Clear(background);
                pausa = false;
            }
            else
            {
                timer1.Stop();
                papel.DrawString("Pausaa", new Font("Arial", 30), new SolidBrush(Color.Yellow), new PointF(220, 200));
                pausa = true;
            }
        }
    }
}
