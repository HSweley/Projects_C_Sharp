namespace TicTacToe
{
    public class Game
    {
        static bool gameOver = false;
        static int turnCounter = 0;
        static string[,] board =
        {
            {"-", "-", "-"},
            {"-", "-", "-"},
            {"-", "-", "-"}
        };
        static void Main(string[] args)
        {
            while(!gameOver)
            {
                PrintBoard();

                try
                {
                    Console.Write("\nPlease enter a row (1-3): ");
                    int inputRow = Convert.ToInt16(Console.ReadLine());
                    Console.Write("Please enter a column (1-3): ");
                    int inputCol = Convert.ToInt16(Console.ReadLine());

                    if (turnCounter % 2 == 0)
                        OMove(inputRow - 1, inputCol - 1);
                    else
                        XMove(inputRow - 1, inputCol - 1);
                }
                catch (Exception)
                {
                    Console.WriteLine("\nThat is not a valid input.");
                }
            }


            Console.ReadLine();
        }

        static void PrintBoard()
        {
            int counter = 1;

            Console.WriteLine();
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(0); col++)
                {
                    if (counter % 3 == 0)
                        Console.WriteLine(board[row, col] + " " + (row + 1));
                    else
                        Console.Write(board[row, col] + " ");
                    counter++;
                }
            }
            Console.WriteLine("1 2 3");
        }

        static void OMove(int row, int col)
        {
            if (board[row, col].Equals("-"))
            {
                board[row, col] = "\u001b[31mO\u001b[0m";
                turnCounter++;
                CheckWin(row, col, "\u001b[31mO\u001b[0m");
            }
            else
                Console.WriteLine("\nThat spot is already taken!");
        }

        static void XMove(int row, int col)
        {
            if (board[row, col].Equals("-"))
            {
                board[row, col] = "\u001b[32mX\u001b[0m";
                turnCounter++;
                CheckWin(row, col, "\u001b[32mX\u001b[0m");
            }
            else
                Console.WriteLine("\nThat spot is already taken!");
        }

        static void CheckWin(int curRow, int curCol, string lastMove)
        {
            int numInARow = 0;
            
            for (int col = 0; col < 3; col++) //checks for horizontal win
            {
                if (board[curRow, col].Equals(lastMove))
                {
                    numInARow++;
                    if (numInARow == 3)
                    {
                        PrintBoard();
                        Console.WriteLine(lastMove + " wins!");
                        gameOver = true;
                        return;
                    }
                }
                else
                {
                    numInARow = 0;
                    break;
                }
            }

            for (int row = 0; row < 3; row++) //checks for vertical win
            {
                if (board[row, curCol].Equals(lastMove))
                {
                    numInARow++;
                    if (numInARow == 3)
                    {
                        PrintBoard();
                        Console.WriteLine(lastMove + " wins!");
                        gameOver = true;
                        return;
                    }
                }
                else
                {
                    numInARow = 0;
                    break;
                }
            }

            if (board[0, 0].Equals(lastMove) && board[1, 1].Equals(lastMove) && board[2, 2].Equals(lastMove)) //checks diagonal win
            {
                PrintBoard();
                Console.WriteLine(lastMove + " wins!");
                gameOver = true;
                return;
            }
            else if (board[2, 0].Equals(lastMove) && board[1, 1].Equals(lastMove) && board[0, 2].Equals(lastMove))
            {
                PrintBoard();
                Console.WriteLine(lastMove + " wins!");
                gameOver = true;
                return;
            }
        }
    }
}