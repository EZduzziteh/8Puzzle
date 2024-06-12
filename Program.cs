using System;
using System.Collections.Generic;

class Tile
{
    public int x;
    public int y;
    public Tile up;
    public Tile down;
    public Tile left;
    public Tile right;
    public int value;

    public void Swap(int direction, List<List<Tile>> board, int boardSize)
    {
        //for this i am just using an integer to represent directions
        //1: up
        //2: down
        //3: left
        //4: right

        switch (direction)
        {
            case 1:
                if (up != null)
                {
                    
                    //swap tile up

                    //store up tile
                    Tile temp = up;


                    //update board positions
                    //move up to this position
                    board[x][y] = up;

                    //put this in ups old position
                    board[temp.x][temp.y] = this;

                    board[x][y].CalculateAdjacents(boardSize, board);
                    board[temp.x][temp.y].CalculateAdjacents(boardSize, board);
                }

                break;
            case 2:
                if (down != null)
                {

                    //swap tile down

                    //store down tile
                    Tile temp = down;


                    //update board positions
                    //move up to this position
                    board[x][y] = down;

                    //put this in ups old position
                    board[temp.x][temp.y] = this;

                    board[x][y].CalculateAdjacents(boardSize, board);
                    board[temp.x][temp.y].CalculateAdjacents(boardSize, board);
                }
                break;
            case 3:
                if(left != null)
                {

                    //swap tile left

                    //store left tile
                    Tile temp = left;


                    //update board positions
                    //move up to this position
                    board[x][y] = left;

                    //put this in ups old position
                    board[temp.x][temp.y] = this;

                    board[x][y].CalculateAdjacents(boardSize, board);
                    board[temp.x][temp.y].CalculateAdjacents(boardSize, board);
                }
                break;
            case 4:
                if (right != null)
                {

                    //swap tile right

                    //store left tile
                    Tile temp = right;


                    //update board positions
                    //move up to this position
                    board[x][y] = right;

                    //put this in ups old position
                    board[temp.x][temp.y] = this;

                    board[x][y].CalculateAdjacents(boardSize, board);
                    board[temp.x][temp.y].CalculateAdjacents(boardSize, board);
                }
                break;
        }
    }
    public void PrintCoordinates()
    {
        Console.Write(x + ", " + y);
    }

    internal void PrintValues()
    {
        Console.Write(value);
    }

    internal void PrintAdjacent()
    {
        Console.WriteLine("Adjacencies for Tile " + value + ":");
        if (up != null)
        {
            Console.WriteLine("     Up: " + up.value);
        }
        else
        {
            Console.WriteLine("     Up: None");
        }
        if (down != null)
        {
            Console.WriteLine("     Down: " + down.value);
        }
        else
        {
            Console.WriteLine("     Down: None");
        }
        if (left != null)
        {
            Console.WriteLine("     Left: " + left.value);
        }
        else
        {
            Console.WriteLine("     Left: None");
        }
        if (right != null)
        {
            Console.WriteLine("     Right: " + right.value);
        }
        else
        {
            Console.WriteLine("     Right: None");
        }
    }

    internal void RandomizeTiles(int boardSize, List<List<Tile>> puzzleBoard)
    {
        Random random = new Random();
        int randomX = random.Next(0, boardSize);
        int randomY = random.Next(0, boardSize);
        Tile temp = puzzleBoard[randomX][randomY];
        puzzleBoard[randomX][randomY] = puzzleBoard[x][y];
        puzzleBoard[x][y] = temp;
    }

    internal void CalculateAdjacents(int boardSize, List<List<Tile>> puzzleBoard)
    {
        
            if (y > 0)
            {
                puzzleBoard[x][y].left = puzzleBoard[x][y - 1];
            }
            if (y < boardSize - 1)
            {
                puzzleBoard[x][y].right = puzzleBoard[x][y + 1];
            }
            if (x > 0)
            {
                puzzleBoard[x][y].up = puzzleBoard[x - 1][y];
            }
            if (x < boardSize - 1)
            {
                puzzleBoard[x][y].down = puzzleBoard[x + 1][y];
            }
        
    }
}



class Program
{

    public static bool CheckPuzzleComplete(List<List<Tile>> completedPuzzle, List<List<Tile>> testingPuzzle, int boardSize)
    {
        Console.WriteLine("check puzzle");
        //loop through pieces of each puzzle, "completedPuzzle" is what we define as the "solution" testing puzzle is our attempt at solving it.
        for (int i = 0; i < boardSize; i++)
        {
            for(int j = 0; j < boardSize; j++)
            {
                if (completedPuzzle[i][j].value != testingPuzzle[i][j].value)
                {
                    //return false as soon as we find a coordinate where we dont match.
                    return false;
                }
            }
        }

        return true;
    }

    public static void Main(string[] args)
    {
        // create the goal tileset 
        // 0 1 2
        // 3 4 5
        // 6 7 8
        List<List<Tile>> completedBoard = new List<List<Tile>>();
        bool solved = false;
        int depthLimit = 50;
        int boardSize = 3;
        int count = 0;
        for (int i = 0; i < boardSize; i++)
        {
            List<Tile> tempList = new List<Tile>();
            for (int j = 0; j < boardSize; j++)
            {
                Tile temp = new Tile();
                temp.x = j;
                temp.y = i;
                temp.value = count;
                tempList.Add(temp);
                count++;
            }
            completedBoard.Add(tempList);
        }

        List<List<Tile>> puzzleBoard = new List<List<Tile>>();
        foreach (var row in completedBoard)
        {
            List<Tile> newRow = new List<Tile>();
            foreach (var tile in row)
            {
                Tile newTile = new Tile
                {
                    x = tile.x,
                    y = tile.y,
                    value = tile.value,
                    up = tile.up,
                    down = tile.down,
                    left = tile.left,
                    right = tile.right
                };
                newRow.Add(newTile);
            }
            puzzleBoard.Add(newRow);
        }
        //scramble board


        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
               puzzleBoard[i][j].RandomizeTiles(boardSize, puzzleBoard);
            }
        }

        

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                puzzleBoard[i][j].CalculateAdjacents(boardSize, puzzleBoard);
            }
        }

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                puzzleBoard[i][j].PrintAdjacent();
            }
        }

        Console.WriteLine("Completed:");

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                completedBoard[i][j].PrintValues();
            }
            Console.WriteLine();
        }


        Console.WriteLine("Scrambled:");

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                puzzleBoard[i][j].PrintValues();
            }
            Console.WriteLine();
        }



        solved = DLDFS(puzzleBoard, completedBoard, depthLimit, boardSize);

        if (solved)
        {
            Console.WriteLine("Solved!");
        }
        else
        {
            Console.WriteLine("Unable to solve puzzle");
        }
    }


    public static bool DLDFS(List<List<Tile>> puzzle, List<List<Tile>> solvedPuzzle, int depthLimit, int boardSize)
    {
        return RecursiveDLDFS(puzzle, solvedPuzzle, depthLimit,0, boardSize);
    }

    private static bool RecursiveDLDFS(List<List<Tile>> puzzle, List<List<Tile>> solvedPuzzle, int depthLimit, int depth, int boardSize)
    {
        Console.WriteLine("recursiveDLDFS");

        if (depth == depthLimit)
        {
            Console.WriteLine("Limit reached");
            return false;
        }

        if (CheckPuzzleComplete(solvedPuzzle, puzzle, boardSize))
        {
            Console.WriteLine("depth: "+depth);
            return true; 
        }

        List<List<List<Tile>>> successors = GenerateSuccessors(puzzle);

        foreach (var successor in successors)
        {
            if (RecursiveDLDFS(successor, solvedPuzzle, depthLimit, depth + 1, boardSize))
            {

                Console.WriteLine("depth: " + depth+1);
                return true; 
            }
        }

        return false; 
    }

    private static List<List<List<Tile>>> GenerateSuccessors(List<List<Tile>> puzzle)
    {
        List<List<List<Tile>>> successors = new List<List<List<Tile>>>();

        for (int i = 0; i < puzzle.Count; i++)
        {
            for (int j = 0; j < puzzle[i].Count; j++)
            {
                if (puzzle[i][j].value == 0)
                {
                    

                    if (i > 0)
                    {
                        List<List<Tile>> successorUp = CreateSuccessor(puzzle, i, j, i - 1, j);
                        successors.Add(successorUp);
                    }

                    if (i < puzzle.Count - 1)
                    {
                        List<List<Tile>> successorDown = CreateSuccessor(puzzle, i, j, i + 1, j);
                        successors.Add(successorDown);
                    }

                    if (j > 0)
                    {
                        List<List<Tile>> successorLeft = CreateSuccessor(puzzle, i, j, i, j - 1);
                        successors.Add(successorLeft);
                    }

                    if (j < puzzle[i].Count - 1)
                    {
                        List<List<Tile>> successorRight = CreateSuccessor(puzzle, i, j, i, j + 1);
                        successors.Add(successorRight);
                    }

                    break; 
                }
            }
        }


        return successors;
    }

    private static List<List<Tile>> CreateSuccessor(List<List<Tile>> puzzle, int row1, int col1, int row2, int col2)
    {
        // Create a copy of the puzzle
        List<List<Tile>> successor = new List<List<Tile>>();
        for (int i = 0; i < puzzle.Count; i++)
        {
            successor.Add(new List<Tile>());
            for (int j = 0; j < puzzle[i].Count; j++)
            {
                successor[i].Add(new Tile
                {
                    x = puzzle[i][j].x,
                    y = puzzle[i][j].y,
                    up = puzzle[i][j].up,
                    down = puzzle[i][j].down,
                    left = puzzle[i][j].left,
                    right = puzzle[i][j].right,
                    value = puzzle[i][j].value
                });
            }
        }

        // Swap tiles
        Tile temp = successor[row1][col1];
        successor[row1][col1] = successor[row2][col2];
        successor[row2][col2] = temp;

        return successor;
    }





}
