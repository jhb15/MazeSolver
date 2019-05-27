using System;
using System.Diagnostics;

namespace MazeSolver2
{
    public struct Coords
    {
        public int x, y;

        public Coords(int ix, int iy)
        {
            x = ix;
            y = iy;
        }
    }

    /*
    enum North { X = 0, Y = 1 };
    enum East { X = 1, Y = 0 };
    enum South { X = 0, Y = -1 };
    enum West { X = -1, Y = 0 };
    */
    public enum MazeValue {Empty, Wall, Start, Finish, WasHere, North, East, South, West}

    public class Maze
    {
        readonly Coords dimentions;
        public Coords start_pos;
        readonly Coords end_pos;
        private Coords current_pos;
        private MazeValue[,] maze_state;

        public Maze(int xLen, int yLen, int startX, int startY, int endX, int endY, MazeValue[,] input)
        {
            dimentions = new Coords(xLen, yLen);
            start_pos = new Coords(startX, startY);
            end_pos = new Coords(endX, endY);
            InitialiseMaze();
            PopulateMaze(input);
        }

        /**
         * Initialises the current position vars and also inializes the array of 
         */
        private void InitialiseMaze()
        {
            current_pos = start_pos;
            maze_state = new MazeValue[dimentions.x, dimentions.y];
        }

        private void PopulateMaze(MazeValue[,] input)
        {
            maze_state = input;
            maze_state[start_pos.x, start_pos.y] = MazeValue.Start;
            maze_state[end_pos.x, end_pos.y] = MazeValue.Finish;
        }
        
        public void MakeMove(int x, int y)
        {
            current_pos.x += x;
            current_pos.y += y;
            //Wrap round functionality.
            if (current_pos.x >= dimentions.x) current_pos.x = 0;
            if (current_pos.x < 0) current_pos.x = dimentions.x - 1;
            if (current_pos.y >= dimentions.y) current_pos.y = 0;
            if (current_pos.y < 0) current_pos.y = dimentions.y - 1;
        }

        public void AddSolution(MazeValue[,] solution)
        {
            for (var y = 0; y < dimentions.y; y++)
            {
                for (var x = 0; x < dimentions.x; x++)
                {
                    //if (solution[x, y])  maze_state[x, y] = MazeValue.Move;
                    var sv = solution[x, y];
                    if ((sv == MazeValue.North) || (sv == MazeValue.East) ||
                        (sv == MazeValue.South) || (sv == MazeValue.West))
                    {
                        maze_state[x, y] = sv;
                    }
                }
            }
            maze_state[start_pos.x, start_pos.y] = MazeValue.Start;
            maze_state[end_pos.x, end_pos.y] = MazeValue.Finish;
        }
        
        public Coords getDimentions()
        {
            return dimentions;
        }

        public Coords getStart()
        {
            return start_pos;
        }

        public Coords getEnd()
        {
            return end_pos;
        }

        public Coords getCurrentPos()
        {
            return current_pos;
        }

        public bool HitWall()
        {
            return (maze_state[current_pos.x, current_pos.y] == MazeValue.Wall);
        }
        
        public bool WasHere()
        {
            return (maze_state[current_pos.x, current_pos.y] == MazeValue.WasHere);
        }

        public bool IsSolved()
        {
            return (current_pos.x == end_pos.x) && (current_pos.y == end_pos.y);
        }

        public void PrintMaze()
        {
            for(var i = 0; i < dimentions.y; i++)
            {
                for(var j = 0; j < dimentions.x; j++)
                {
                    //Console.Write(maze_state[j, i] ? '#' : ' ');
                    switch (maze_state[j,i])
                    {
                        case MazeValue.Empty:
                            Console.Write(' ');
                            break;
                        case MazeValue.Wall:
                            Console.Write('#');
                            break;
                        case MazeValue.Start:
                            Console.Write('B');
                            break;
                        case MazeValue.North:
                            Console.Write('n');
                            break;
                        case MazeValue.East:
                            Console.Write('e');
                            break;
                        case MazeValue.South:
                            Console.Write('s');
                            break;
                        case MazeValue.West:
                            Console.Write('w');
                            break;
                        case MazeValue.Finish:
                            Console.Write('F');
                            break;
                        case MazeValue.WasHere:
                            Console.Write('_');
                            break;
                        default:
                            Console.WriteLine("Error unexpected value stored in Maze!, Value Was: " + maze_state[j,i]);
                            return;
                    }
                }
                Console.Write('\n');
            }
        }
    }
}