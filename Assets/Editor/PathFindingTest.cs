using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;

[TestFixture]
public class PathFindingTest
{
    [Test]
    public void PathFinding_LevelFirst_FindShortestPath()
    {
        int[,] test = new int[,]
        {
            {1,0,0,0 },
            {0,0,0,1 },
            {0,1,0,0 },
            {0,0,0,0 },
        };

        PathFinding pathfinder = new PathFinding(test);
        CellMapBFS start = new CellMapBFS(4,0,0);
        CellMapBFS end = new CellMapBFS(6,5,0);
        List<CellMapBFS> shortestPath = new List<CellMapBFS>();
        List<Vector2> path = new();


        shortestPath = pathfinder.FindShortestPath(start, end);
        for (int i = 0; i < shortestPath.Count; i++)
            path.Add(new Vector2(shortestPath[i].Row, shortestPath[i].Col));


        Assert.AreEqual(shortestPath.Count, 3);
        Assert.AreEqual(path[0], new Vector2(4, 0));
        Assert.AreEqual(path[1], new Vector2(4, 2));
        Assert.AreEqual(path[2], new Vector2(6, 5));
    }
}