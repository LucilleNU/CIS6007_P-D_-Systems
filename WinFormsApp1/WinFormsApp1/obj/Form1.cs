using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly Random random = new Random();
        private int k = 5; //change this for number of threads to test with 5 / 10 / 20
        private int delay = 20;
        private int NumberOfCircles = 1200;
        private int i = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var watch = new Stopwatch();
            watch.Start();
            int Radius = 20;
            Random Cord = new Random();

            while (i < NumberOfCircles)
            {
                for (int j = 1; j <= k; j++)
                {
                    var Coordinate1 = Cord.Next(0, 600);
                    var Coordinate2 = Cord.Next(0, 800);
                    var g = e.Graphics;
                    var RandomColor = new Pen(Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));
                    var circle = new Rectangle(Coordinate1, Coordinate2, Radius * 2, Radius * 2);
                    g.DrawEllipse(RandomColor, circle);
                }
                i = i + k;
                System.Threading.Thread.Sleep(delay);
            }

            watch.Stop();
            if (i >= NumberOfCircles)
            {
                MessageBox.Show($"Execution of Program Completed, \n Execution Time: {watch.ElapsedMilliseconds} ms", "Execution Completed");
            }
        }
    }
}