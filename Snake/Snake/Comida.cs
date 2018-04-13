using System;
using System.Drawing;

namespace Snake
{
    public class Comida
    {
        public int x, y;
        public int size = 10;
        public Color color;

        public Comida(int a, int b, Random rand)
        {
            x = a;
            y = b;
            color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }
        public Comida(int a, int b, Color fix)
        {
            x = a;
            y = b;
            color = fix;
        }
        public void dibujar(Graphics papel)
        {
            papel.FillRectangle(new SolidBrush(color), x * size, y * size, size, size);
        }
        public void desdibuja(Graphics papel, Color back)
        {
            papel.FillRectangle(new SolidBrush(back), x * size, y * size, size, size);
        }

        public int X()
        {
            return x;
        }
        public int Y()
        {
            return y;
        }


    }
}