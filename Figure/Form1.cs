using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figure
{
    public partial class Form1 : Form
    {
        Graphics Graph;
        Point center;
        Pen color;
        const int SIZE = 150;
        public Form1()
        {
            InitializeComponent();
            Graph = pictureBox1.CreateGraphics();
            color = new Pen(Color.Black);
        }

        private void radioButtonBlack_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.White;
            if(radioButtonBlack.Checked)
            {
                color.Color = Color.Black;
            }
            else if(radioButtonBlue.Checked)
            {
                color.Color = Color.Blue;
            }
            else if (radioButtonGreen.Checked)
            {
                color.Color = Color.Green;
            }
            else if (radioButtonPink.Checked)
            {
                color.Color = Color.Pink;
            }
            else if (radioButtonRed.Checked)
            {
                color.Color = Color.Red;
            }
            else if (radioButtonWhite.Checked)
            {
                pictureBox1.BackColor = Color.Black;
                color.Color = Color.White;
            }
            else if (radioButtonYellow.Checked)
            {
                color.Color = Color.Yellow;
            }
            DrawFirg();
            
        }

        private void DrawFirg()
        {
            if (color.Color == Color.White)
                Graph.Clear(Color.Black);
            else
                Graph.Clear(BackColor);
            if (center != null)
            {
                if (radioButton1.Checked)
                {
                    Graph.DrawEllipse(color, center.X - SIZE / 2, center.Y - SIZE / 2, SIZE, SIZE);
                }
                else if (radioButton2.Checked)
                {
                    Graph.DrawRectangle(color, center.X - SIZE / 2, center.Y - SIZE / 2, SIZE, SIZE);
                }
                else
                {
                    Graph.DrawLine(color, center.X - 100, center.Y, center.X + 100, center.Y);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            DrawFirg();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Graph.Clear(BackColor);
            center = new Point(e.X, e.Y);
            DrawFirg();
        }
    }
}
