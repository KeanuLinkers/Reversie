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
        int boardSize, rowCount, colCount, circleDiameter;
        int[,] board;
        bool boardCreated;
        public Form1()
        {
            boardSize = 6;
            rowCount = boardSize;
            colCount = boardSize;

            board = new int[rowCount, colCount];
            //board[2, 2] = 1;
            //board[3, 2] = 2;
            //board[2, 3] = 2;
            //board[3, 3] = 1;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if ((i == (board.GetLength(0) / 3)) && (j == (board.GetLength(1) / 3)))
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
            PrintArray(board);
        }

        private void PrintArray(int[,] arr)
        {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", arr[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ReadLine();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            // create board if it hasn't been created yet
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
                boardCreated = !boardCreated;
            }


            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] != 0)
                    {
                        if (board[i, j] == 1)
                            e.Graphics.FillEllipse(Brushes.Blue, circleDiameter * i, circleDiameter * j, circleDiameter, circleDiameter);
                        else if (board[i, j] == 2)
                            e.Graphics.FillEllipse(Brushes.Red, circleDiameter * i, circleDiameter * j, circleDiameter, circleDiameter);
                    }
                }
            }
        }
    }
}
