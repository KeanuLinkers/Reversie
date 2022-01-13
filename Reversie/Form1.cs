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
        int boardSize, circleWidth, circleHeight, rowCount, colCount;
        bool boardCreated, blueTurn, help;


        // the board is a 2D array of integers, where 0 represents an empty space, 1 represents taken by blue
        // 2 represents taken by red, and 3 represents a possible move for the current player
        int[,] board;

        public Form1()
        {
            boardSize = 6;
            colCount = boardSize;
            rowCount = boardSize;
            board = new int[colCount, rowCount];
            blueTurn = true;
            help = false;

            //In the following double for-loop, the initial state of the board is defined
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if ((i == ((board.GetLength(0) / 2) - 1)) && (j == ((board.GetLength(1) / 2) - 1)))

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
            circleWidth = panel1.Width / colCount;
            circleHeight = panel1.Height / rowCount;

        }

        private void MakeMove(int i, int j) // This 'MakeMove' method is responsible for the flip of the opponents coloured circle.
        {
            if (blueTurn) // The following code is responsible for the flip when it's blue's turn
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        if (FindAdjacentRuns(i, j, k, l))
                        {
                            int m, p;
                            m = i;
                            p = j;
                            while (board[m + k, p + l] == 2)
                            {
                                if (IsInsideBoard(i + k, j + l))
                                {
                                    if (board[m + k, p + l] == 2)
                                        board[m + k, p + l] = 1;
                                }
                                if (k > 0)
                                    m++;
                                if (k < 0)
                                    m--;
                                if (l > 0)
                                    p++;
                                if (l < 0)
                                    p--;
                            }
                        }
                    }
                }
            }

            else // The following code is responsible for the flip when it's red's turn
            {
                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        if (FindAdjacentRuns(i, j, k, l))
                        {
                            int m, p;
                            m = i;
                            p = j;
                            while (board[m + k, p + l] == 1)
                            {
                                if (IsInsideBoard(i + k, j + l))
                                {
                                    if (board[m + k, p + l] == 1)
                                        board[m + k, p + l] = 2;
                                }
                                if (k > 0)
                                    m++;
                                if (k < 0)
                                    m--;
                                if (l > 0)
                                    p++;
                                if (l < 0)
                                    p--;
                            }
                        }
                    }
                }
            }
        }

        private bool IsEmptySpace(int i, int j) // This method is made to check if a certain place on the board, is an empty space.
        {
            return (board[i, j] == 0);
        }

        private bool IsInsideBoard(int i, int j) // This method checks whether a certain place is inside of the board.
        {
            return ((i >= 0) && (j >= 0) && (i < board.GetLength(0)) && (j < board.GetLength(1)));
        }

        private bool FindInDirection(int i, int j, int k, int l) // The 'FindInDirection' methode looks in all directions for legal spaces.
        {
            if (blueTurn)
            {
                while (board[i + k, j + l] == 2)
                {
                    if (k > 0)
                        i++;
                    if (k < 0)
                        i--;
                    if (l > 0)
                        j++;
                    if (l < 0)
                        j--;
                    if (IsInsideBoard(i + k, j + l))
                    {
                        if (board[i + k, j + l] == 1)
                            return true;
                    }
                    else
                        break;
                }
                return false;
            }
            else
            {
                while (board[i + k, j + l] == 1)
                {
                    if (k > 0)
                        i++;
                    if (k < 0)
                        i--;
                    if (l > 0)
                        j++;
                    if (l < 0)
                        j--;
                    if (IsInsideBoard(i + k, j + l))
                    {
                        if (board[i + k, j + l] == 2)
                            return true;
                    }
                    else
                        break;
                }
                return false;
            }
        }

        private bool FindAdjacentRuns(int i, int j, int k, int l) // This method
        {
            if (blueTurn)
            {
                // use IsInsideBoard method to check whether position is on the board
                if (IsInsideBoard(i + k, j + l))
                {
                    // from the empty space, see if there's a red space to the right
                    if (board[i + k, j + l] == 2)
                        // if so, start looking in the right direction for a blue
                        if (FindInDirection(i, j, k, l))
                            return true;

                }
                return false;
            }

            else // For the redturn
            {
                // use IsInsideBoard method to check whether position is on the board
                if (IsInsideBoard(i + k, j + l))
                {
                    if (board[i + k, j + l] == 1)
                        if (FindInDirection(i, j, k, l))
                            return true;

                }
                return false;
            }

        }

        private void button1_Click(object sender, EventArgs e) // This button click eventhandler is responsible to clear the board to its initial state
        {
            int[,] newboard = new int[colCount, rowCount];
            board = newboard;

            //In the following double for-loop, the initial state of the board is defined
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if ((i == ((board.GetLength(0) / 2) - 1)) && (j == ((board.GetLength(1) / 2) - 1)))

                    {
                        board[i, j] = 1;
                        board[i + 1, j] = 2;
                        board[i, j + 1] = 2;
                        board[i + 1, j + 1] = 1;
                    }
                }
            }

            blueTurn = true;
            help = false;

            panel1.Invalidate();
        }

        private bool CheckLegalMove(int i, int j)
        {
            // if current space is empty

            if (IsEmptySpace(i, j))
            {
                // look for runs in the adjacent spaces (up, down, left, right and diagonals)
                if (FindAdjacentRuns(i, j, 1, 0)) // right
                    return true;
                if (FindAdjacentRuns(i, j, 1, 1)) // down-right
                    return true;
                if (FindAdjacentRuns(i, j, 0, 1)) // down
                    return true;
                if (FindAdjacentRuns(i, j, -1, 1)) // down-left
                    return true;
                if (FindAdjacentRuns(i, j, -1, 0)) // left
                    return true;
                if (FindAdjacentRuns(i, j, -1, -1)) // up-left
                    return true;
                if (FindAdjacentRuns(i, j, 0, -1)) // up
                    return true;
                if (FindAdjacentRuns(i, j, 1, -1)) // up-right
                    return true;
                else
                    return false;

            }
            else
                return false;
        }

        private void ResetLegalMoves(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 3)
                        board[i, j] = 0;
                }
            }
        }

        private void MarkLegalMoves(int[,] board)
        {
            ResetLegalMoves(board);
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (CheckLegalMove(i, j))
                        board[i, j] = 3;
                }
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e) // This eventhandler is responsible for draw the circles when there is a click on the panel.
        {

            int g, h;
            g = e.X / circleWidth;
            h = e.Y / circleHeight;

            if (board[g, h] == 3)
            {
                help = false;
                if (blueTurn)
                {
                    board[g, h] = 1;
                    MakeMove(g, h);
                    this.label1.Text = "Rood is aan zet";
                    this.label1.ForeColor = System.Drawing.Color.Red;
                }

                else
                {
                    board[g, h] = 2;
                    MakeMove(g, h);
                    this.label1.Text = "Blauw is aan zet";
                    this.label1.ForeColor = System.Drawing.Color.Blue;
                }
                blueTurn = !blueTurn;
                MarkLegalMoves(board);
                panel1.Invalidate();
            }

        }

        private void Help_Click(object sender, EventArgs e) //This is the click eventhandler of the 'help' button. It is responsible for showing the little green circles that stand for the legal spaces, when clicked on the button.
        {
            help = !help;
            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) // This is the paint eventhandler. This eventhandler is responsible for drawing on the panel.
        {


            if (!boardCreated) // The following two for-assignments are responsible for drawing the white lines that makes the board.
            {
                for (int i = 0; i <= rowCount; i++)
                {
                    e.Graphics.DrawLine(Pens.White, new Point(0, circleHeight * i), new Point(panel1.Width, circleHeight * i));
                }

                for (int i = 0; i <= colCount; i++)
                {
                    e.Graphics.DrawLine(Pens.White, new Point(circleWidth * i, 0), new Point(circleWidth * i, panel1.Height));
                }
            }

            for (int i = 0; i < colCount; i++) // This last following part of code, makes sure that all the different circles can be and will be drawn.
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (board[i, j] != 0)
                    {
                        if (board[i, j] == 1)
                            e.Graphics.FillEllipse(Brushes.Blue, circleWidth * i, circleHeight * j, circleWidth, circleHeight);
                        else if (board[i, j] == 2)
                            e.Graphics.FillEllipse(Brushes.Red, circleWidth * i, circleHeight * j, circleWidth, circleHeight);
                        else if (board[i, j] == 3 && help)
                            e.Graphics.DrawEllipse(Pens.Green, circleWidth * i, circleHeight * j, circleWidth, circleHeight);

                    }
                }
            }

        }
    }
}
