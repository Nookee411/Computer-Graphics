using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractals
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        Pen dot;
        List<Point> points;
        public Form1()
        {
            InitializeComponent();
            Graph = pictureBox1.CreateGraphics();
            points = new List<Point>();
            dot = new Pen(Color.Black);
        }

        private void buttonSerp_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            if (radioButton1.Checked)
            {
                points.Add(new Point(e.X, e.Y));
                if (points.Count == 3)
                {
                    Graph.DrawSerpi(dot, points[0], points[1], points[2]);
                    points.Clear();
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                Graph.Clear(BackColor);
                PointF start = new PointF(0, pictureBox1.Height / 5);
                PointF end = new PointF(pictureBox1.Width, pictureBox1.Height / 5 );
                Graph.DrawCoch(Pens.Red, start, end, 50);
            }
        }
    }

    public static class GrapgExtention
    { 
        public static void DrawSerpi(this Graphics Graph,Pen pen, Point A,Point B, Point C)
        {
            Random rand = new Random();
            const int RAD = 1;
            const int ITERATIONS = 10000;
            Graph.DrawEllipse(pen, A.X - RAD, A.Y - RAD, RAD * 2, RAD * 2);
            Graph.DrawEllipse(pen, B.X - RAD, B.Y - RAD, RAD * 2, RAD * 2);
            Graph.DrawEllipse(pen, C.X - RAD, C.Y - RAD, RAD * 2, RAD * 2);
            Point temp = A;
            for(int i=0;i<ITERATIONS;i++)
            {
                int seed = rand.Next(3);
                switch (seed)
                {
                    case 0:
                        {
                            temp = Center(temp, A);
                            break;
                        }
                    case 1:
                        {
                            temp = Center(temp, B);
                            break;
                        }
                    case 2:
                        {
                            temp = Center(temp, C);
                            break;
                        }

                }
                Graph.DrawEllipse(pen, temp.X - RAD, temp.Y - RAD, RAD * 2, RAD * 2);
            }
            
        }

        private static Point Center(Point a, Point b)
        {
            return new Point((a.X + b.X) / 2, (a.Y + b.Y) / 2);
        }

        public static void DrawCoch(this Graphics Graph, Pen line,PointF A,PointF B, int depth)
        {
            if (depth >= 0 &&Length(A,B)>1)
            {
                Pen white = new Pen(Color.White, 2);
                PointF oneThird = SplitOneTirds(A, B);
                PointF twoThird = SplitOneTirds(B, A);
                Graph.DrawLine(line, A, oneThird);
                Graph.DrawLine(line, B, twoThird);
                Graph.DrawLine(white, oneThird, twoThird);
                PointF thirdPoint = new PointF((float)((A.X + B.X) / 2 + ((A.Y - B.Y) / (2 * Math.Sqrt(3)))), (float)((A.Y + B.Y) / 2 + ((B.X - A.X) / (2 * Math.Sqrt(3)))));
                Graph.DrawLine(line, oneThird, thirdPoint);
                Graph.DrawLine(line, thirdPoint, twoThird);
                DrawCoch(Graph, line, oneThird, thirdPoint, depth-1);
                DrawCoch(Graph, line, thirdPoint, twoThird, depth-1);
                DrawCoch(Graph, line, A, oneThird, depth-1);
                DrawCoch(Graph, line, twoThird, B, depth-1);
            }
        }

        private static PointF SplitOneTirds(PointF start, PointF end)
        {
            double lambda = (double)1/3;
            return new PointF((float)((start.X + lambda * end.X) / (1 + lambda)), (float)((start.Y + lambda * end.Y) / (1 + lambda)));
        }

        private static int Length(PointF b1, PointF b2)
        {
            return (int)(Math.Sqrt(Math.Pow(b2.Y - b1.Y, 2) + Math.Pow(b2.X - b1.X, 2)));

        }
    }

}
