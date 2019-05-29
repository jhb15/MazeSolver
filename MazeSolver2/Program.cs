using System;
using System.IO;

namespace MazeSolver2
{
    internal enum InputLine { DimLine = 0, StartLine = 1, EndLine = 2, MazeLine = 3 };

    internal static class Program
    {
        /**
         * Function used to print the user menu to the command line.
         */
        private static void PrintMenu()
        {
            Console.WriteLine("\rPlease choose from the following options:");
            Console.WriteLine("S. Solve a Maze");
            Console.WriteLine("H. Help");
            Console.WriteLine("Q. Exit Application");
        }
        static char GetChar()
        {
            int input = Console.ReadKey().KeyChar;
            try
            {
                return Convert.ToChar(input);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("{0} Value read = {1}.", e.Message, input);
                return char.MinValue;
            }
        }

        private static string GetFile()
        {
            Console.Write("Please enter file path for file: ");
            var input = Console.ReadLine();

            Console.WriteLine(input);
            return input;
        }

        private static int[] ConvertLine(string input)
        {
            var intStrings = input.Split(' ');
            //Console.WriteLine(intStrings.Length);
            var ints = new int[intStrings.Length];

            int i = 0;
            foreach(var intStr in intStrings)
            {
                if (intStr == "") continue; //TODO Temporary fix, should be a better way.
                ints[i] = int.Parse(intStr);
                i++;
            }
            return ints;
        }

        private static Maze GetMazeFromFile(string filePath)
        {
            try
            {
                var fileLines = File.ReadAllLines(filePath);
                
                if (fileLines.Length == 0)
                {
                    return null;
                }

                var inDims = ConvertLine(fileLines[0]);
                var inStart = ConvertLine(fileLines[1]);
                var intEnd = ConvertLine(fileLines[2]);

                var mazeVals = new MazeValue[inDims[0], inDims[1]];
                for (var y = 0; y < inDims[1]; y++)
                {
                    var yVals = ConvertLine(fileLines[y + 3]);
                    for (var x = 0; x < inDims[0]; x++)
                    {
                        mazeVals[x,y] = (MazeValue)yVals[x];
                    }
                }
                return new Maze(inDims[0], inDims[1], inStart[0], inStart[1], intEnd[0], intEnd[1], mazeVals);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong! msg: " + e.Message);
                return null;
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Maze Solver!");
            MazeSolver mazeSolver = new MazeSolver();
            char option;

            do
            {
                PrintMenu();
                option = GetChar();
                Console.Write('\r');
                option = Char.ToUpper(option);

                switch (option)
                {
                    case 'H':
                        Console.WriteLine("\n\nTo solve a selected maze use the 'S' menu option. Once you see a message" +
                                          "asking for a file path, input the path of the maze input.\n" +
                                          "Warning! You can only use a relative file paths.  \n\n");
                        break;
                    case 'S':
                        string filePath = GetFile();
                        Maze maze = GetMazeFromFile(filePath);
                        if (maze == null) goto default;
                        maze.PrintMaze();
                        Console.WriteLine("");
                        var solvedMaze = mazeSolver.SolveMaze(maze);
                        solvedMaze?.PrintMaze();
                        break;
                    default:
                        Console.WriteLine("Error Unrecognised Option!");
                        break;
                }
            } while (option != 'Q');
        }
    }
}
