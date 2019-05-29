Maze Solver
===========
###### by James H Britton (jhbritton96@icloud.com)

 This is my solution for the Gentrack Maze Solver Technical test, to solve this problem I used a 
 recursive backtrack algorithm. There are probably better solutions using other algoritms but this was
 the simplest and quickest to implement and does this job. I also wrote a C# console application to
 utilise this recursive application.
  
#### How to run the app
> ###### Windows (Built for Windows 10)
>
>> Step 1: Start application using the '/MazeSolver/MazeSolver2/bin/Release/netcoreapp2.1/win10-x64/MazeSolver2.exe'
file found in the submitted .zip file.
> 
>> Step 2: Once you see the menu listing the options press the 'S' key on you keyboard
>
>> Step 3: When you are asked for a file path enter the path to the input file you wish to use and peress enter
>
>> Step 4: You should now see the maze outputted followed by the solution to the maze
>
> ###### Linux (Built for Ubuntu 16.10 x64)
> 
>> Step 1: Start the application using the '/MazeSolver/MazeSolver2/bin/Release/netcoreapp2.1/ubuntu.16.10-x64/MazeSolver2'
file found in the submitted .zip file.
>
>> Step 2: Once you see the menu listing the options press the 'S' key on you keyboard
>
>> Step 3: When you are asked for a file path enter the path to the input file you wish to use and peress enter
>
>> Step 4: You should now see the maze outputted followed by the solution to the maze
>

#### Known Issues

>
>> This solution will not find the shortest/best path for solving a maze but it will find a path for
solving the maze. An example of this would be when you input the sparse_large.txt sample maze, instead
of taking the most direct path the solver decides to take a more covoluted path toward the destination
pint.
>
>> Also this application requires that you use a relative path towards the destination file instead of
being able to use an absolute or relative path. Probably a simple change.
>