namespace ConnectFour
{
    public class Game
    {
        static bool gameOver = false;
        static int turnCounter = 0;
        static string[,] board =
        {
            {"-", "-", "-", "-", "-", "-", "-"},
            {"-", "-", "-", "-", "-", "-", "-"},
            {"-", "-", "-", "-", "-", "-", "-"},
            {"-", "-", "-", "-", "-", "-", "-"},
            {"-", "-", "-", "-", "-", "-", "-"},
            {"-", "-", "-", "-", "-", "-", "-"}
        };
        static void Main(string[] args)
        {
            while (!gameOver) //allows the game to continue until a winner or a draw is declared
            {
                PrintBoard();

                try
                {
                    Console.Write("Please enter a column (1-7): ");
                    int inputCol = Convert.ToInt16(Console.ReadLine());

                    if (turnCounter % 2 == 0) //changes the turn every time a valid column is inputted
                        RedMove(inputCol - 1);
                    else
                        YellowMove(inputCol - 1);
                }
                catch (Exception ex) //prevents IndexOutOfRangeException and FormatException from bad user inputs
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
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (counter % 7 == 0)
                        Console.WriteLine(board[row, col] + " " + (row + 1));
                    else
                        Console.Write(board[row, col] + "  ");
                    counter++;
                }
            }
            Console.WriteLine("1  2  3  4  5  6  7");
        }

        static void RedMove(int col)
        {
            for (int row = 5; row > -1; row--) //starts from the bottom and checks for an empty space ("dropping a chip")
            {
                if (board[row, col].Equals("-"))
                {
                    board[row, col] = "\u001b[31mO\u001b[0m";
                    turnCounter++;
                    CheckWin(row, col, "\u001b[31mO\u001b[0m");
                    return;
                }
            }
            Console.WriteLine("\nThat column is full!"); //for no empty spaces in the selected column
        }

        static void YellowMove(int col)
        {
            for (int row = 5; row > -1; row--) //starts from the bottom and checks for an empty space ("dropping a chip")
            {
                if (board[row, col].Equals("-"))
                {
                    board[row, col] = "\u001b[33mO\u001b[0m";
                    turnCounter++;
                    CheckWin(row, col, "\u001b[33mO\u001b[0m");
                    return;
                }
            }
            Console.WriteLine("\nThat column is full!"); //for no empty spaces in the selected column
        }

        static void CheckWin(int curRow, int curCol, string lastMove) //curRow, curCol is the index for the last move
        {
            int numOfChipsInARow = 0;

            for (int col = 0; col < 7; col++) //checks for horizontal win by staying in the inputted row "curRow" and changing col
            {
                if (board[curRow, col].Equals(lastMove))
                {
                    numOfChipsInARow++;
                    if (numOfChipsInARow >= 4)
                    {
                        PrintBoard();
                        Console.WriteLine(lastMove + " wins!");
                        gameOver = true;
                        return;
                    }
                }
                else
                    numOfChipsInARow = 0;
            }
            numOfChipsInARow = 0;

            for (int row = 5; row > -1; row--) //checks for vertical win by staying in the inputted column "curCol" and changing row
            {
                if (board[row, curCol].Equals(lastMove))
                {
                    numOfChipsInARow++;
                    if (numOfChipsInARow >= 4)
                    {
                        PrintBoard();
                        Console.WriteLine(lastMove + " wins!");
                        gameOver = true;
                        return;
                    }
                }
                else
                    numOfChipsInARow = 0;
            }
            numOfChipsInARow = 0;

            //these 2 while loops check for northwest-southeast wins
            int counter = 0;
            while (curRow + counter < 6 && curCol + counter < 7)
            {
                if (board[curRow + counter, curCol + counter].Equals(lastMove))
                {
                    counter++;
                    numOfChipsInARow++;
                }
                else
                    break;
            }
            counter = 1;

            while (curRow - counter > -1 && curCol - counter > -1)
            {
                if (board[curRow - counter, curCol - counter].Equals(lastMove))
                {
                    counter++;
                    numOfChipsInARow++;
                }
                else
                    break;
            }

            if (numOfChipsInARow >= 4)
            {
                PrintBoard();
                Console.WriteLine(lastMove + " wins!");
                gameOver = true;
                return;
            }
            else
            {
                numOfChipsInARow = 0;
                counter = 1;
            }

            //these 2 while loops check for southwest-northeast wins
            while (curRow - counter > -1 && curCol + counter < 7)
            {
                if (board[curRow - counter, curCol + counter].Equals(lastMove))
                {
                    counter++;
                    numOfChipsInARow++;
                }
                else
                    break;
            }
            counter = 1;

            while (curRow + counter < 6 && curCol - counter > -1)
            {
                if (board[curRow + counter, curCol - counter].Equals(lastMove))
                {
                    counter++;
                    numOfChipsInARow++;
                }
                else
                    break;
            }

            if (numOfChipsInARow >= 4)
            {
                PrintBoard();
                Console.WriteLine(lastMove + " wins!");
                gameOver = true;
                return;
            }
            else if (turnCounter >= 42) //if the board is full but no winner was found
            {
                PrintBoard();
                Console.WriteLine("It's a draw...");
                gameOver = true;
                return;
            }
        }
    }
}