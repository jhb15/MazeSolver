using System;

namespace MazeSolver2
{
    public class MazeSolver
    {
        private MazeValue[,] _solution;
        private bool[,] _visited;
        
        public Maze SolveMaze(Maze inputMaze)
        {
            _solution = new MazeValue[inputMaze.GetDimensions().x, inputMaze.GetDimensions().y];
            _visited = new bool[inputMaze.GetDimensions().x, inputMaze.GetDimensions().y];
            
            var output = SolveMazeRecursive(inputMaze);
            if (output == null) Console.WriteLine("Error Unable to Solve Maze!");
            output?.AddSolution(_solution);
            return output;
        }

        private Maze SolveMazeRecursive(Maze inputMaze)
        {
            if (inputMaze.IsSolved()) return inputMaze;
            if (inputMaze.HitWall() || _visited[inputMaze.GetCurrentPos().x, inputMaze.GetCurrentPos().y]) return null;
            _visited[inputMaze.GetCurrentPos().x, inputMaze.GetCurrentPos().y] = true;

            var moveNorth = inputMaze;
            moveNorth.MakeMove(0, -1);
            var northOut = SolveMazeRecursive(moveNorth);
            moveNorth.MakeMove(0, 1);
            if (northOut != null) //Move North
            {
                //Console.WriteLine("MoveNorth");
                _solution[inputMaze.GetCurrentPos().x, inputMaze.GetCurrentPos().y] = MazeValue.North;
                return northOut;
            }

            var moveEast = inputMaze;
            moveEast.MakeMove(1, 0);
            var eastOut = SolveMazeRecursive(moveEast);
            moveEast.MakeMove(-1, 0);
            if (eastOut != null) //Move East
            {
                //Console.WriteLine("MoveEast");
                _solution[inputMaze.GetCurrentPos().x, inputMaze.GetCurrentPos().y] = MazeValue.East;
                return eastOut;
            }

            var moveSouth = inputMaze;
            moveSouth.MakeMove(0, 1);
            var southOut = SolveMazeRecursive(moveSouth);
            moveSouth.MakeMove(0, -1);
            if (southOut != null) //Move South
            {
                //Console.WriteLine("MoveSouth");
                _solution[inputMaze.GetCurrentPos().x, inputMaze.GetCurrentPos().y] = MazeValue.South;
                return southOut;
            }

            var moveWest = inputMaze;
            moveWest.MakeMove(-1, 0);
            var westOut = SolveMazeRecursive(moveWest);
            moveWest.MakeMove(1, 0);
            if (westOut != null) //Move West
            {
                //Console.WriteLine("MoveWest");
                _solution[inputMaze.GetCurrentPos().x, inputMaze.GetCurrentPos().y] = MazeValue.West;
                return westOut;
            }

            return null;
        }

        public Maze SolveMazeShortestPath(Maze inputMaze)
        {
            //TODO Implement
            return inputMaze;
        }

    }
}