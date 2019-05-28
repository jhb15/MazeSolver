using System;

namespace MazeSolver2
{
    public class MazeSolver
    {
        private MazeValue[,] solution;
        private bool[,] visited;
        
        public Maze SolveMaze(Maze inputMaze)
        {
            solution = new MazeValue[inputMaze.getDimentions().x, inputMaze.getDimentions().y];
            visited = new bool[inputMaze.getDimentions().x, inputMaze.getDimentions().y];
            
            var output = SolveMazeRecursive(inputMaze);
            if (output == null) Console.WriteLine("Error Unable to Solve Maze!");
            output?.AddSolution(solution);
            return output;
        }

        public Maze SolveMazeRecursive(Maze inputMaze)
        {
            if (inputMaze.IsSolved()) return inputMaze;
            if (inputMaze.HitWall() || visited[inputMaze.getCurrentPos().x, inputMaze.getCurrentPos().y]) return null;
            visited[inputMaze.getCurrentPos().x, inputMaze.getCurrentPos().y] = true;

            var moveNorth = inputMaze;
            moveNorth.MakeMove(0, -1);
            var northOut = SolveMazeRecursive(moveNorth);
            moveNorth.MakeMove(0, 1);
            if (northOut != null) //Move North
            {
                //Console.WriteLine("MoveNorth");
                solution[inputMaze.getCurrentPos().x, inputMaze.getCurrentPos().y] = MazeValue.North;
                return northOut;
            }

            var moveEast = inputMaze;
            moveEast.MakeMove(1, 0);
            var eastOut = SolveMazeRecursive(moveEast);
            moveEast.MakeMove(-1, 0);
            if (eastOut != null) //Move East
            {
                //Console.WriteLine("MoveEast");
                solution[inputMaze.getCurrentPos().x, inputMaze.getCurrentPos().y] = MazeValue.East;
                return eastOut;
            }

            var moveSouth = inputMaze;
            moveSouth.MakeMove(0, 1);
            var southOut = SolveMazeRecursive(moveSouth);
            moveSouth.MakeMove(0, -1);
            if (southOut != null) //Move South
            {
                //Console.WriteLine("MoveSouth");
                solution[inputMaze.getCurrentPos().x, inputMaze.getCurrentPos().y] = MazeValue.South;
                return southOut;
            }

            var moveWest = inputMaze;
            moveWest.MakeMove(-1, 0);
            var westOut = SolveMazeRecursive(moveWest);
            moveWest.MakeMove(1, 0);
            if (westOut != null) //Move West
            {
                //Console.WriteLine("MoveWest");
                solution[inputMaze.getCurrentPos().x, inputMaze.getCurrentPos().y] = MazeValue.West;
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