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
        int boardSize, circleDiameter, rowCount, colCount;         
        bool boardCreated;
        int aantal = 0;



        int[,] board;
     
        public Form1()
        {
            boardSize = 4;
            rowCount = boardSize;
            colCount = boardSize;
            board = new int[rowCount, colCount];
            
            
            
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if ((i == ((board.GetLength(0) /2)-1 )) && (j == ((board.GetLength(1) / 2)-1)))
                    
                    {
                        board[i, j] = 1;
                        board[i + 1, j] = 2;
                        board[i, j + 1] = 2;
                        board[i + 1, j + 1] = 1;
                    }
                }
            }
            boardCreated = false;
            InitializeComponent();            
            circleDiameter = panel1.Width / boardSize;
                     

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
            int g, h;
            g = e.X / circleDiameter;
            h = e.Y / circleDiameter;

            if (aantal % 2 == 0)
            {                        
                board[g, h] = 1;
                this.label1.Text = "Rood is aan zet";
                this.label1.ForeColor = System.Drawing.Color.Red;
            }  
            
            else if (aantal%2 != 0)
            {                             
                board[g, h] = 2;
                this.label1.Text = "Blauw is aan zet";
                this.label1.ForeColor = System.Drawing.Color.Blue;
            }
            aantal++;
            panel1.Invalidate();

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           

            if (!boardCreated)
            {
                for (int i = 0; i <= boardSize; i++)
                {
                    e.Graphics.DrawLine(Pens.White, new Point(0, circleDiameter * i), new Point(panel1.Width, circleDiameter * i));
                }

                for (int i = 0; i <= boardSize; i++)
                {
                    e.Graphics.DrawLine(Pens.White, new Point(circleDiameter * i, 0), new Point(circleDiameter * i, panel1.Height));
                }
            }

            for(int i = 0; i<boardSize; i++)
            {
                for(int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] != 0)
                    {
                        if(board[i,j]==1)
                            e.Graphics.FillEllipse(Brushes.Blue, circleDiameter * i, circleDiameter * j, circleDiameter, circleDiameter);
                        else if(board[i,j]==2)
                            e.Graphics.FillEllipse(Brushes.Red, circleDiameter * i, circleDiameter * j, circleDiameter, circleDiameter);

                    }
                }
            }

        }
    }
}
