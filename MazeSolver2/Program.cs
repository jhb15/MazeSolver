using System;
using System.IO;

namespace MazeSolver2
{
    enum InputLine { DimLine = 0, StartLine = 1, EndLine = 2, MazeLine = 3 };
    class Program
    {
        static void PrintMenu()
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
                return Char.MinValue;
            }
        }
        static string GetFile()
        {
            Console.Write("Please enter file path for file: ");
            string input = Console.ReadLine();

            Console.WriteLine(input);
            return input;
        }
        static int[] ConvertLine(string input)
        {
            string[] intStrings = input.Split(' ');
            //Console.WriteLine(intStrings.Length);
            int[] ints = new int[intStrings.Length];

            int i = 0;
            foreach(string intStr in intStrings)
            {
                if (intStr == "") continue; //TODO Temporary fix, should be a better way.
                ints[i] = int.Parse(intStr);
                i++;
            }
            return ints;
        }
        static Maze GetMazeFromFile(string filePath)
        {
            string[] fileLines;
            try
            {
                fileLines = File.ReadAllLines(@filePath);
                
                if (fileLines.Length == 0)
                {
                    return null;
                }

                var inDims = ConvertLine(fileLines[0]);
                var inStart = ConvertLine(fileLines[1]);
                var intEnd = ConvertLine(fileLines[2]);

                MazeValue[,] mazeVals = new MazeValue[inDims[0], inDims[1]];
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
        
        static void Main(string[] args)
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
                        Console.WriteLine("Help requested, not yet implemented!");
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
