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

    /**
     * Maze class, used to represent a maze and store all of its properties.
     */
    public class Maze
    {
        private readonly Coords _dimensions;
        private readonly Coords _startPos;
        private readonly Coords _endPos;
        private Coords _currentPos;
        private MazeValue[,] _mazeState;

        public Maze(int xLen, int yLen, int startX, int startY, int endX, int endY, MazeValue[,] input)
        {
            _dimensions = new Coords(xLen, yLen);
            _startPos = new Coords(startX, startY);
            _endPos = new Coords(endX, endY);
            InitialiseMaze();
            PopulateMaze(input);
        }

        /**
         * Initialises the current position vars and also initializes the array of 
         */
        private void InitialiseMaze()
        {
            _currentPos = _startPos;
            _mazeState = new MazeValue[_dimensions.x, _dimensions.y];
        }

        /**
         * Uses a 2D input array to populate the constructed objects _maze_state instance variable
         */
        private void PopulateMaze(MazeValue[,] input)
        {
            _mazeState = input;
            _mazeState[_startPos.x, _startPos.y] = MazeValue.Start;
            _mazeState[_endPos.x, _endPos.y] = MazeValue.Finish;
        }
        
        /**
         * Used to move the current location of an actor on the maze, take and x and y value as input to denote the
         * change in x and y on the maze.
         */
        public void MakeMove(int x, int y)
        {
            _currentPos.x += x;
            _currentPos.y += y;
            //Wrap round functionality.
            if (_currentPos.x >= _dimensions.x) _currentPos.x = 0;
            if (_currentPos.x < 0) _currentPos.x = _dimensions.x - 1;
            if (_currentPos.y >= _dimensions.y) _currentPos.y = 0;
            if (_currentPos.y < 0) _currentPos.y = _dimensions.y - 1;
        }

        /**
         * Used once a solution to the maze has been found to add this silution to the maze using and input 2D solution
         * array
         */
        public void AddSolution(MazeValue[,] solution)
        {
            for (var y = 0; y < _dimensions.y; y++)
            {
                for (var x = 0; x < _dimensions.x; x++)
                {
                    //if (solution[x, y])  maze_state[x, y] = MazeValue.Move;
                    var sv = solution[x, y];
                    if ((sv == MazeValue.North) || (sv == MazeValue.East) ||
                        (sv == MazeValue.South) || (sv == MazeValue.West))
                    {
                        _mazeState[x, y] = sv;
                    }
                }
            }
            _mazeState[_startPos.x, _startPos.y] = MazeValue.Start;
            _mazeState[_endPos.x, _endPos.y] = MazeValue.Finish;
        }
        
        public Coords GetDimensions()
        {
            return _dimensions;
        }

        public Coords GetStart()
        {
            return _startPos;
        }

        public Coords GetEnd()
        {
            return _endPos;
        }

        public Coords GetCurrentPos()
        {
            return _currentPos;
        }

        public bool HitWall()
        {
            return (_mazeState[_currentPos.x, _currentPos.y] == MazeValue.Wall);
        }
        
        public bool WasHere()
        {
            return (_mazeState[_currentPos.x, _currentPos.y] == MazeValue.WasHere);
        }

        public bool IsSolved()
        {
            return (_currentPos.x == _endPos.x) && (_currentPos.y == _endPos.y);
        }

        /**
         * Function used to print the current maze state to the console
         */
        public void PrintMaze()
        {
            for(var i = 0; i < _dimensions.y; i++)
            {
                for(var j = 0; j < _dimensions.x; j++)
                {
                    //Console.Write(maze_state[j, i] ? '#' : ' ');
                    switch (_mazeState[j,i])
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
                            Console.WriteLine("Error unexpected value stored in Maze!, Value Was: " + _mazeState[j,i]);
                            return;
                    }
                }
                Console.Write('\n');
            }
        }
    }
}