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
        bool boardCreated, blueTurn;
        int aantal = 0;



        int[,] board;
     
        public Form1()
        {
            boardSize = 6;
            rowCount = boardSize;
            colCount = boardSize;
            board = new int[rowCount, colCount];
            blueTurn = true;
            
            
            
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

            MarkLegalMoves(board);       
            boardCreated = false;
            InitializeComponent();            
            circleDiameter = panel1.Width / boardSize;  

        }

        private bool IsEmptySpace(int i, int j)
        {
            return (board[i, j] == 0);
        }

        private bool CheckRow(int i, int j)
        {
            int k;
            if ( i == 0 )
            {
                k = 0;
                while (k < board.GetLength(0))
                {

                }
            }
        }

        // look at adjacent spaces to find opponents color.
        // if found, look in the direction of the adjacent space for your own color, to terminate the run mark space as a legal move.
        private bool FindAdjacentRuns(int i, int j)
        {
            // several if-statements need to be inserted to check if we're on the edges of the board
            // we shouldn't look past the board, there's probably a cleaner solution than a different if-statement for each corner/edge
            if (i == 0)
            {
                if(blueTurn)
                {

                }
            }
        }

        
        private bool CheckLegalMove(int i, int j)
        {
            // if current space is empty
            if (IsEmptySpace(i, j))
            {
                // look for runs in the adjacent spaces (up, down, left, right and diagonals)
                FindAdjacentRuns(i, j);
            }
        }
        
        // if a space is a legal move for the current player, it is marked with a 3 in the array
        private void MarkLegalMoves(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (CheckLegalMove(i, j))
                        board[i, j] = 3;
                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
            int g, h;
            g = e.X / circleDiameter;
            h = e.Y / circleDiameter;

            if (blueTurn)
            {                        
                board[g, h] = 1;
                this.label1.Text = "Rood is aan zet";
                this.label1.ForeColor = System.Drawing.Color.Red;
            }  
            
            else
            {                             
                board[g, h] = 2;
                this.label1.Text = "Blauw is aan zet";
                this.label1.ForeColor = System.Drawing.Color.Blue;
            }
            blueTurn = !blueTurn;
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
