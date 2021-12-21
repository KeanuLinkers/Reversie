using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversie
{
    
    public partial class Form1 : Form
    {
        int boardSize, circleDiameter;
        int[,] board;
        public Form1()
        {
            boardSize = 6;
            board = new int[boardSize, boardSize];
            InitializeComponent();
            circleDiameter = panel1.Width / boardSize;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //Brush bordkleur = new SolidBrush(Color.FromArgb(139, 69, 19));
            //Brush lijnkleur = new SolidBrush(Color.Black);
            //int xpositie, ypositie, breedte, hoogte;
            //xpositie = panel1.Location.X;
            //ypositie = panel1.Location.Y;
            //breedte = panel1.Width;
            //hoogte = panel1.Height;
            //e.Graphics.FillRectangle(bordkleur, xpositie, ypositie, breedte, hoogte);

            //e.Graphics.FillRectangle(lijnkleur, 35, 175, panel1.Width, 10);
            //e.Graphics.FillRectangle(lijnkleur, 35, 350, panel1.Width, 10);
            //e.Graphics.FillRectangle(lijnkleur, 35, 525, panel1.Width, 10);
            //e.Graphics.FillRectangle(lijnkleur, 233, 55, 10, panel1.Height);
            //e.Graphics.FillRectangle(lijnkleur, 467, 55, 10, panel1.Height);

            

            for (int i = 0; i <= boardSize; i++)
            {
                for (int j = 0; j <= boardSize; j++)
                {
                    e.Graphics.DrawLine(Pens.White, new Point(0, circleDiameter * j), new Point(panel1.Width, circleDiameter * j));
                    e.Graphics.DrawLine(Pens.White, new Point(circleDiameter * i, 0), new Point(circleDiameter * i, panel1.Height));
                }
            }
        }
    }
}
