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
        List<List<Comida>> objetos = new List<List<Comida>>();
        Graphics papel;
        Color background = Color.Black;
        int dirx, diry, x, y,score, level, speed;
        bool gameOver, pausa, godmode;

        public Form1()
        {
            InitializeComponent();
            papel = pictureBox1.CreateGraphics();
            speed = Convert.ToInt32(textBox1.Text);
            timer1.Interval = speed;
            button_Pausa.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!gameOver)
            {
                label1.Text = "Score: " + Convert.ToString(score);
                x += dirx;
                y += diry;
                if (((x < 0 || x > 59) || (y < 0 || y > 44)) && !godmode) gameOver = true;
                if (check_collision(x, y, objetos) && !godmode) gameOver = true;
                objetos[0].Add(new Comida(x, y, rand));

                foreach (List<Comida> objeto in objetos)
                    foreach(Comida parte in objeto)
                        parte.dibujar(papel);

                bit.dibujar(papel);
                
                move();
                if (score >= 5)
                {
                    for (int i = 0; i < rand.Next(3, 4); i++)
                        objetos.Add(add_obstaculo());
                    level = 2;
                }
                if (score >= 10)
                {
                    level = 3;
                }

                if (score >= 15)
                {
                    level = 4;
                }
                
                if (level == 2)
                    timer1.Interval = speed - (score - 5) * 2;
                if (score >= 15 && level == 3)
                {
                }

            }
            else
            {
                timer1.Stop();
                button_Pausa.Hide();
                button_start.Show();
                checkBox1.Show();
                textBox1.Show();
                label2.Show();
                papel.DrawEllipse(new Pen(Color.Red, 2), x*bit.size - 7, y*bit.size - 7, bit.size + 15, bit.size + 15);
                papel.DrawString("Game Over", new Font("Arial", 50), new SolidBrush(Color.Yellow), new PointF(120, 180));
            }
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            papel.Clear(background);
            rand = new Random(rand.Next(0, 1024));
            gameOver = false;
            pausa = false;
            godmode = checkBox1.Checked;
            speed = Convert.ToInt32(textBox1.Text);
            timer1.Interval = speed;
            score = 0;
            level = 1;
            x = 30;
            y = 22;
            diry = -1;
            dirx = 0;
            objetos = new List<List<Comida>>();
            objetos.Add(new List<Comida>());
            bit = new Comida(rand.Next(0, 60), rand.Next(0, 44), Color.Yellow);
            for(int i  = 0; i < 4; i++)
                objetos[0].Add(new Comida(x, y+i, rand));
            foreach (List<Comida> objeto in objetos)
                foreach (Comida parte in objeto)
                    parte.dibujar(papel);
            button_start.Hide();
            checkBox1.Hide();
            button_Pausa.Show();
            textBox1.Hide();
            label2.Hide();
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

        private void button_Pausa_Click(object sender, EventArgs e)
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

        private bool check_collision(int cordx, int cordy, List<List<Comida>> objetos)
        {
            foreach(List<Comida> objeto in objetos)
                foreach(Comida a in objeto)
                    if (a.x == cordx && a.y == cordy) return true;
           return false;
        }

        private void move()
        {
            if (bit.x == x && bit.y == y)
            {
                bit = null;
                while (bit == null)
                {
                    int newx = rand.Next(0, 60);
                    int newy = rand.Next(0, 45);
                    if (check_collision(newx, newy, objetos) == false)
                        bit = new Comida(newx, newy, Color.Yellow);
                }
                score++;
            }
            else
            {
                objetos[0][0].desdibuja(papel, background);
                objetos[0].RemoveAt(0);
            }
        }

        private List<Comida> add_obstaculo()
        {
            List<Comida> obstaculo = new List<Comida>();
            bool collision = true;
            while (collision)
            {
                int newy = rand.Next(5, 40);
                for (int newx = 5; newx < 55; newx++)
                {
                    if (check_collision(newx, newy, objetos) == false)
                    {
                        collision = false;
                        if (rand.Next(0, 3) == 2)
                            obstaculo.Add(new Comida(newx, newy, Color.Red));
                    }
                }
            }
            return obstaculo;
        }

    }
}
