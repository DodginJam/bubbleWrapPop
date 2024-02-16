// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace bubbleWrapPop
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PlayBubbleBoard();
        }

        static string[,] CreateBoard(int width, int height)
        {
            // Returns a two-Dimensional array with given width and height.
            // The array generated represents the game board and is mutated via NewPoppedBoard method.

            string[,] boardArray = new string[height, width];
            for (int i = 0; i < boardArray.GetLength(0); i++)
            {
                for (int j = 0; j < boardArray.GetLength(1); j++)
                {
                    if (j == boardArray.GetLength(1) - 1)
                    {
                        boardArray[i, j] = "O\n"; // New Line characters are inserted on end-of-array elements to format the board when printed to console.
                    }
                    else
                    {
                        boardArray[i, j] = "O";
                    }

                }
            }
            return boardArray;
        }

        static void PrintBoard(string[,] board)
        {
            foreach (string element in board)
            {
                Console.Write(element);
            }
        }

        static string[,] NewPoppedBoard(string[,] currentBoard, int xCoord, int yCoord)
        {
            // Simply returns a new array with the chosen coord character mutated to different character.
            if (currentBoard[yCoord - 1, xCoord - 1].Contains("\n"))
            {
                currentBoard[yCoord - 1, xCoord - 1] = "X\n"; // Intended to keep array formatted when printed, when mutating end of array characters.
            }
            else
            {
                currentBoard[yCoord - 1, xCoord - 1] = "X";
            }

            return currentBoard;
        }

        static void PlayBubbleBoard()
        {
            GrabDimensions(out int width, out int height); // Userinputs height and width.
            Console.WriteLine("\nCurrent bubble board:");
            string[,] currentBoard = CreateBoard(width, height); // New board generated.
            PrintBoard(currentBoard); // New board is printed to console.
            // width: {currentBoard.GetLength(1)} and height: {currentBoard.GetLength(0)}"); // --------------------------------------------------------EDIT POINT
            Console.WriteLine();
            if (IsBubbleBoardFinishedLoop(currentBoard))
            { // Waiting for true to be returned from loop in method before game completion announced.
                Console.WriteLine("\nAll bubbles popped!");
            }

        }
        static void GrabDimensions(out int width, out int height)
        {
            // Method for interacting with user via console and grabbing intergers for initial board creation.
            // The out intergers are intended to be used as arguments for the CreateBoard method.

            Console.WriteLine("\nWelcome to the bubbleWrap pop game!\nChoose the dimensions of your board.");
            Console.Write("Width: ");
            bool widthIsInt = Int32.TryParse(Console.ReadLine(), out width);
            while (!widthIsInt)
            {
                Console.Write("Enter a valid interger.\nWidth: ");
                widthIsInt = Int32.TryParse(Console.ReadLine(), out width);
            }
            Console.Write("Height: ");
            bool heightIsInt = Int32.TryParse(Console.ReadLine(), out height);
            while (!heightIsInt)
            {
                Console.Write("Enter a valid interger.\nHeight: ");
                heightIsInt = Int32.TryParse(Console.ReadLine(), out height);
            }
        }

        static bool IsBubbleBoardFinishedLoop(string[,] currentBoard)
        {
            // Method initiates game literal game loop - takes user coordinates and makes a pop - loop only ends when all elements of array are popped.
            bool isCompletedBoard = false; // While loop will only exit when this is true.
            while (!isCompletedBoard)
            {
                PopBubblesCoord(currentBoard, out int xCoord, out int yCoord);
                currentBoard = NewPoppedBoard(currentBoard, xCoord, yCoord);
                Console.WriteLine();
                PrintBoard(currentBoard);
                Console.WriteLine();
                bool containsUnpopped = false; // This gets flagged as true if a unpopped element is found in array during nested for loops below. 
                                               // If it remains false, allows while to end and method returns true.
                for (int i = 0; i < currentBoard.GetLength(0); i++)
                {
                    for (int j = 0; j < currentBoard.GetLength(1); j++)
                    {
                        if (currentBoard[i, j].Contains("O"))
                        {
                            containsUnpopped = true;
                            break;
                        }
                    }
                    if (containsUnpopped == true)
                    {
                        break;
                    }
                }
                if (containsUnpopped == false)
                {
                    isCompletedBoard = true;
                }
            }

            return isCompletedBoard;
        }

        static void PopBubblesCoord(string[,] gameBoard, out int xCoord, out int yCoord)
        {
            // Method for interacting with user via console and grabbing coordinates for bubble to be popped.
            // The out intergers are intended to be used as arguments for the NewPoppedBoard method.
            // Uses validation methods to ensure user numbers are valid integers and within range of gameboard X and Y length.

            Console.WriteLine("Enter coordinates for bubble to pop!");

            Console.Write("X-Coord: ");
            int validatedXAsNum = ValidateUserInputAsNum("Enter a valid interger.\nX-Coord: ");
            int validatedXInRange = ValidateNumAsWithinRange(validatedXAsNum, gameBoard.GetLength(1));
            xCoord = validatedXInRange;

            Console.Write("Y-Coord: ");
            int validatedYAsNum = ValidateUserInputAsNum("Enter a valid interger.\nY-Coord: ");
            int validatedYInRange = ValidateNumAsWithinRange(validatedYAsNum, gameBoard.GetLength(0));
            yCoord = validatedYInRange;

        }

        static int ValidateUserInputAsNum(string errorText)
        {
            bool isNumInt = Int32.TryParse(Console.ReadLine(), out int outNum);
            while (!isNumInt)
            {
                Console.WriteLine(errorText);
                isNumInt = Int32.TryParse(Console.ReadLine(), out outNum);
            }
            return outNum;
        }
        static int ValidateNumAsWithinRange(int coordNum, int arrayLength)
        {
            bool isCoordInRange = coordNum > 0 && coordNum <= arrayLength;
            while (!isCoordInRange)
            {
                Console.WriteLine($"Enter a number between {1} and {arrayLength}");
                coordNum = ValidateUserInputAsNum("Enter a valid interger: ");
                isCoordInRange = coordNum > 0 && coordNum <= arrayLength;
            }
            return coordNum;
        }
    }
}