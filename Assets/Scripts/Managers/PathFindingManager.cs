using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AStar;
using UnityEngine.Tilemaps;

public class PathFindingManager : MonoBehaviour {

    public Tilemap map;
    public Tilemap obstacleMap;
    private SquareGrid grid;

    private void Start()
    {
        this.grid = new SquareGrid(100, 100);
        this.grid.tiles = GenerateTileMapLocations();
        this.grid.walls = GenerateObstaclesLocations();
    }

    public List<Vector3> GetCalculatedMapPath(Vector2Int start, Vector2Int end) {
        // Run A*
        AStarSearch astar = new AStarSearch(this.grid, new Location(start.x, start.y), new Location(end.x, end.y));
        List<Location> path = DeterminePathFromAStar(astar, new Location(start.x, start.y), new Location(end.x, end.y));
        List<Vector3> mapPositions = new List<Vector3>();
        foreach (Location l in path) {
            if (this.grid.tiles.Contains(l)) { // Did we select inside the known grid?
                Vector3 position = map.CellToWorld(new Vector3Int(l.x, l.y, 0));
                mapPositions.Add(position);
            }
        }
        
        return mapPositions;
    }

    private HashSet<Location> GenerateObstaclesLocations()
    {

        HashSet<Location> blockMap2D = new HashSet<Location>();

        //Iterate through all of the indices
        // get the highest non-empty block in each column.
        for (int collumn = obstacleMap.cellBounds.xMin; collumn < obstacleMap.cellBounds.xMax; collumn++)
        {
            for (int row = obstacleMap.cellBounds.yMin; row < obstacleMap.cellBounds.yMax; row++)
            {
                if (obstacleMap.GetTile(new Vector3Int(collumn, row, 0)) != null)
                {
                    Location l = new Location(collumn, row);
                    if (!blockMap2D.Contains(l))
                    {
                        blockMap2D.Add(new Location(collumn, row));
                    }
                }
            }
        }
        return blockMap2D;
    }
    private HashSet<Location> GenerateTileMapLocations()
    {

        HashSet<Location> blockMap2D = new HashSet<Location>();

        //Iterate through all of the indices
        // get the highest non-empty block in each column.
        for (int collumn = map.cellBounds.xMin; collumn < map.cellBounds.xMax; collumn++)
        {
            for (int row = map.cellBounds.yMin; row < map.cellBounds.yMax; row++)
            {
                if (map.GetTile(new Vector3Int(collumn, row, (int)map.transform.position.y)) != null)
                {
                    Location l = new Location(collumn, row);
                    if (!blockMap2D.Contains(l))
                    {
                        blockMap2D.Add(new Location(collumn, row));
                    }
                }
            }
        }
        return blockMap2D;
    }

    private List<Location> DeterminePathFromAStar(AStarSearch search, Location start, Location end) {
        List<Location> path = new List<Location>();
        Location current = start;
        Location next = end;
        path.Add(end);
        while (!next.Equals(current)) {
            current = next;
            if (search.cameFrom.ContainsKey(next)) {
                next = search.cameFrom[next];
                if (!current.Equals(next)) {
                    path.Add(next);
                }
            }
        }

        path.Reverse();
        return path;
    }

}
