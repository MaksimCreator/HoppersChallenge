using System;
using System.Collections.Generic;
using System.Numerics;

public class PathFinding
{
    private const int START_JUMP = 1;
    private const int LENGHT_ARRAY_DIRECTION = 8;

    private int[,] _grid;
    private int _rows;
    private int _cols;

    private static readonly int[] dRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
    private static readonly int[] dCol = { -1, 0, 1, -1, 1, -1, 0, 1 };

    public PathFinding(int[,] grid)
    {
        _grid = grid;
        _rows = grid.GetLength(0);
        _cols = grid.GetLength(1);
    }

    public List<CellMapBFS> FindShortestPath(CellMapBFS start, CellMapBFS end)
    {
        if (!IsValidCell(start.Row, start.Col) || _grid[start.Row, start.Col] == 0)
            return null;
        if (!IsValidCell(end.Row, end.Col) || _grid[end.Row, end.Col] == 0)
            return null;

        Queue<CellMapBFS>queue = new Queue<CellMapBFS>();
        HashSet<(int, int)>visited = new HashSet<(int, int)>();

        start = new CellMapBFS(start.Row, start.Col, 0, null);
        queue.Enqueue(start);
        visited.Add((start.Row, start.Col));

        while (queue.Count > 0)
        {
            CellMapBFS current = queue.Dequeue();

            if (current.Row == end.Row && current.Col == end.Col)
                return ReconstructPath(current);


            for (int i = 0; i < LENGHT_ARRAY_DIRECTION; i++)
            {
                int nextRow = current.Row + dRow[i];
                int nextCol = current.Col + dCol[i];
                if(IsValidCell(nextRow, nextCol) && _grid[nextRow, nextCol] == 1 && !visited.Contains((nextRow, nextCol)))
                {
                    visited.Add((nextRow, nextCol));
                    queue.Enqueue(new CellMapBFS(nextRow, nextCol, current.Distance + 1, current));
                }
            }

            for (int j = START_JUMP; j <= Config.MAX_JUMP_DEPTH; j++)
            {
                for (int d = 0; d < LENGHT_ARRAY_DIRECTION; d++)
                    CheckJump(current, queue, visited, current.Row + dRow[d] * j, current.Col + dCol[d] * j, dRow[d], dCol[d]);
            }
        }

        return null;
    }

    private void CheckJump(CellMapBFS current, Queue<CellMapBFS> queue, HashSet<(int, int)> visited, int positionJumpRow,int positionJumpCol,int directionRow,int directionCol)
    {
        for (int i = 0; i < LENGHT_ARRAY_DIRECTION; i++)
        {
            if (dCol[i] == Inverse(directionCol) && Inverse(directionRow) == dRow[i])
                continue;

            int nextRow = positionJumpRow + dRow[i];
            int nextCol = positionJumpCol + dCol[i];

            if (IsValidCell(nextRow, nextCol) && _grid[nextRow, nextCol] == 1 && !visited.Contains((nextRow, nextCol)))
            {
                visited.Add((nextRow, nextCol));
                queue.Enqueue(new CellMapBFS(nextRow, nextCol, current.Distance + 1, current));
            }
        }
    }

    private bool IsValidCell(int row, int col)
        => row >= 0 && row < _rows && col >= 0 && col < _cols;

    private List<CellMapBFS> ReconstructPath(CellMapBFS end)
    {
        List<CellMapBFS> path = new List<CellMapBFS>();
        CellMapBFS current = end;

        while (current != null)
        {
            path.Add(current);
            current = current.Parent;
        }
        
        path.Reverse();
        return path;
    }

    private int Inverse(int number)
    => number * -1;
}