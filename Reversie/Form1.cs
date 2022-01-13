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
        int boardSize, circleWidth, circleHeight, rowCount, 
            colCount, blueScore, redScore;
        bool blueTurn, help;


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
            InitializeComponent();
            CountScore();
            circleWidth = panel1.Width / colCount;
            circleHeight = panel1.Height / rowCount;

        }

        // End of the game is represented explicitly by the big label
        private void EndGame()
        {
            if (blueScore == redScore)
            {
                label1.Text = "Remise";
                label1.ForeColor = Color.Purple;
            }
            else if (blueScore > redScore)
            {
                label1.Text = "Blauw wint!";
                label1.ForeColor = Color.Blue;
            }
            else
            {
                label1.Text = "Rood wint!";
                label1.ForeColor = Color.Red;
            }
        }

        // Counts legal moves and returns true if there are none
        private bool NoLegalMove()
        {
            int legalMoves = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 3)
                        legalMoves++;
                }
            }
            return legalMoves == 0;
        }

        // This method will keep the score values updated
        private void CountScore()
        {
            blueScore = 0;
            redScore = 0;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 1)
                        blueScore++;
                    else if (board[i, j] == 2)
                        redScore++;
                }
            }
            label2.Text = "Punten blauw: " + blueScore.ToString();
            label3.Text = "Punten rood: " + redScore.ToString();
        }

        // This 'MakeMove' method is responsible for the flip of the opponents coloured circle
        private void MakeMove(int i, int j)
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

        // This method is made to check if a certain place on the board, is an empty space
        private bool IsEmptySpace(int i, int j)
        {
            return (board[i, j] == 0);
        }

        // This method checks whether a certain place is inside of the board
        private bool IsInsideBoard(int i, int j)
        {
            return ((i >= 0) && (j >= 0) && (i < board.GetLength(0)) && (j < board.GetLength(1)));
        }

        // The 'FindInDirection' method looks in the direction given by 'FindAdjacentRuns' for captures
        private bool FindInDirection(int i, int j, int k, int l)
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

        // This method will look in all directions to find the opponents color
        // if found, look in that direction to see if you can capture your opponents color
        // i and j represent a space on the board, while k and l represent horizontal and vertical components of the direction we're looking in
        private bool FindAdjacentRuns(int i, int j, int k, int l) 
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

        // This method will first check whether a space is empty, if so it will look in all directions from that space
        // by calling 'FindAdjacentRuns' in all directions (represented by the third and fourth arguments in the method calls)
        private bool CheckLegalMove(int i, int j)
        {
            if (IsEmptySpace(i, j))
            {
                // look for runs in the adjacent spaces
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

        // Before marking new legal moves, we remove all legal moves from the previous turn
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

        // Input a 3 in the array when the space is a legal move for the current player
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

        // This eventhandler is responsible for drawing the circles when there is a click on the panel.
        private void panel1_MouseClick(object sender, MouseEventArgs e) 
        {
            // convert clicked coordinates in the panel to coordinates in the 2D board array
            int g, h;
            g = e.X / circleWidth;
            h = e.Y / circleHeight;

            // if a legal space is clicked, everything happens
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
                CountScore();
                MarkLegalMoves(board);

                
                if (NoLegalMove())
                {
                    // if there is no legal move for the current player, they pass
                    blueTurn = !blueTurn;
                    MarkLegalMoves(board);
                    // if there is no legal move twice in a row (so for neither player) the game is ended
                    if (NoLegalMove())
                    {
                        EndGame();
                    }
                }
                panel1.Invalidate();
            }

        }

        // This eventhandler toggles visibility for the little green circles that represent legal spaces.
        private void Help_Click(object sender, EventArgs e) 
        {
            help = !help;
            panel1.Invalidate();
        }

        // This button click eventhandler is responsible for clearing the board to its initial state
        private void button1_Click(object sender, EventArgs e)
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

            CountScore();
            MarkLegalMoves(board);
            panel1.Invalidate();
        }

        // The paint eventhandler is responsible for drawing on the panel.
        private void panel1_Paint(object sender, PaintEventArgs e) 
        {
            // The following two for-assignments are responsible for drawing the white lines that makes the board.
            for (int i = 0; i <= rowCount; i++)
            {
                e.Graphics.DrawLine(Pens.White, new Point(0, circleHeight * i), new Point(panel1.Width, circleHeight * i));
            }

            for (int i = 0; i <= colCount; i++)
            {
                e.Graphics.DrawLine(Pens.White, new Point(circleWidth * i, 0), new Point(circleWidth * i, panel1.Height));
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
