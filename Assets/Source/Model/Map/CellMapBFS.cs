using System;

public class CellMapBFS
{
    public readonly int Row;
    public readonly int Col;
    public readonly int Distance;

    public readonly CellMapBFS Parent;

    public CellMapBFS(int row, int col, int distance, CellMapBFS parent = null)
    {
        Row = row;
        Col = col;
        Distance = distance;
        Parent = parent;
    }

    public override bool Equals(object obj)
    {
        if (obj is CellMapBFS other)
            return Row == other.Row && Col == other.Col;

        return false;
    }

    public override int GetHashCode()
    =>  HashCode.Combine(Row, Col);
}