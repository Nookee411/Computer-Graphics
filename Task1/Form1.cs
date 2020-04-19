using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        Point start, end;
        Pen p1, p2;

        public Form1()
        {
            InitializeComponent();
            Graph = pictureBox1.CreateGraphics();
            p1 = new Pen(Color.Black);
            p2 = new Pen(Color.Red);
            DoubleBuffered = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            start = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Graph.Clear(BackColor);
            end = new Point(e.X, e.Y);
            Graph.DrawImpulse(p1, p2, start, end);
        }
    }

    public static class GraphExtention
    {
        static public void DrawImpulse(this Graphics Graph, Pen color1, Pen color2, Point c1, Point c2)
        {
            Ball ball1 = new Ball(c1, c2);
            Ball ball2 = new Ball(c2, c1);
            for(int i =0;i<20;i++)
            {
                Graph.DrawEllipse(new Pen(Color.White), ball1.GetUpperX, ball1.GetUpperY, Ball.SIZE, Ball.SIZE);
                Graph.DrawEllipse(new Pen(Color.White), ball2.GetUpperX, ball2.GetUpperY, Ball.SIZE, Ball.SIZE);

                if (Ball.Intersect(ball1, ball2))
                {
                    Ball.Hit(ball1, ball2);
                }
                ball1.Step();
                ball2.Step();
                Graph.DrawEllipse(color1, ball1.GetUpperX, ball1.GetUpperY, Ball.SIZE, Ball.SIZE);
                Graph.DrawEllipse(color2, ball2.GetUpperX, ball2.GetUpperY, Ball.SIZE, Ball.SIZE);
                
                Thread.Sleep(200);
                
            }
            
        }
    }

    public class Ball
    {
        const int STEP = 40;
        public const int SIZE = 40;
        public Point coordinates;
        int dx, dy;
        public Ball(Point c1, Point c2)
        {
            coordinates = new Point(c1.X/SIZE*SIZE,(c1.Y/SIZE)*SIZE);
            dx = (c2.X/SIZE*SIZE - coordinates.X) / STEP;
            dy = (c2.Y/SIZE*SIZE - coordinates.Y) / STEP;
        }

        public Point GetUpperPoint => new Point(coordinates.X - SIZE / 2, coordinates.Y - SIZE / 2);
        public int GetUpperX => coordinates.X - SIZE / 2;
        public int GetUpperY => coordinates.Y - SIZE / 2;

        public void Step()
        {
            coordinates.X += dx;
            coordinates.Y += dy;
        }

        public static bool Intersect(Ball b1, Ball b2)
        {
            int length = (int)(Math.Sqrt(Math.Pow(b2.coordinates.Y - b1.coordinates.Y, 2) + Math.Pow(b2.coordinates.X - b1.coordinates.X, 2)));
            return (length <= Ball.SIZE+10);
        }
        public static void Hit(Ball b1, Ball b2)
        {
            Swap(ref b1.dx, ref b2.dx);
            Swap(ref b1.dy, ref b2.dy);
        }

        public static Ball NextStep(Ball b1,Ball b2)
        {
            return new Ball(new Point(b1.coordinates.X + b1.dx, b1.coordinates.Y + b1.dy), new Point(b2.coordinates.X, b2.coordinates.Y));
        }

        private static void Swap(ref int a,ref int b)
        {
            int t = a;
            a = b;
            b = t;
        }

    }
}
