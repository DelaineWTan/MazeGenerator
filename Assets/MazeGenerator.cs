using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject cellPrefab;

    private Cell[,] grid;

    private void Start()
    {
        grid = new Cell[width, height];
        InitializeGrid();
        RecursiveBacktracking(0, 0);
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject cellObject = Instantiate(cellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                Cell cell = cellObject.GetComponent<Cell>();
                grid[x, z] = cell;
                cell.x = x;
                cell.z = z;
            }
        }
    }

    private void RecursiveBacktracking(int x, int z)
    {
        grid[x, z].visited = true;
        List<CellDirection> directions = GetShuffledDirections();

        foreach (var direction in directions)
        {
            int newX = x + DirectionToX(direction);
            int newZ = z + DirectionToZ(direction);

            if (newX >= 0 && newX < width && newZ >= 0 && newZ < height && !grid[newX, newZ].visited)
            {
                grid[x, z].RemoveWall(direction);
                grid[newX, newZ].RemoveWall(OppositeDirection(direction));
                RecursiveBacktracking(newX, newZ);
            }
        }
    }

    private List<CellDirection> GetShuffledDirections()
    {
        List<CellDirection> directions = new List<CellDirection>
        {
            CellDirection.Up,
            CellDirection.Right,
            CellDirection.Down,
            CellDirection.Left
        };
        int n = directions.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            CellDirection temp = directions[k];
            directions[k] = directions[n];
            directions[n] = temp;
        }
        return directions;
    }

    private int DirectionToX(CellDirection direction)
    {
        switch (direction)
        {
            case CellDirection.Up:
                return 1;
            case CellDirection.Right:
                return 0;
            case CellDirection.Down:
                return -1;
            case CellDirection.Left:
                return 0;
            default:
                return 0;
        }
    }

    private int DirectionToZ(CellDirection direction)
    {
        switch (direction)
        {
            case CellDirection.Up:
                return 0;
            case CellDirection.Right:
                return -1;
            case CellDirection.Down:
                return 0;
            case CellDirection.Left:
                return 1;
            default:
                return 0;
        }
    }

    private CellDirection OppositeDirection(CellDirection direction)
    {
        switch (direction)
        {
            case CellDirection.Up:
                return CellDirection.Down;
            case CellDirection.Right:
                return CellDirection.Left;
            case CellDirection.Down:
                return CellDirection.Up;
            case CellDirection.Left:
                return CellDirection.Right;
            default:
                return CellDirection.Up;
        }
    }
}
