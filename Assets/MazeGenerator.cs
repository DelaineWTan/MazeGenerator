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
            }
        }
    }

    private void RecursiveBacktracking(int x, int z)
    {
        grid[x, z].visited = true;
        List<Cell.Direction> directions = GetShuffledDirections();

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

    private List<Cell.Direction> GetShuffledDirections()
    {
        List<Cell.Direction> directions = new List<Cell.Direction>
        {
            Cell.Direction.Up,
            Cell.Direction.Right,
            Cell.Direction.Down,
            Cell.Direction.Left
        };
        int n = directions.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Cell.Direction temp = directions[k];
            directions[k] = directions[n];
            directions[n] = temp;
        }
        return directions;
    }

    private int DirectionToX(Cell.Direction direction)
    {
        switch (direction)
        {
            case Cell.Direction.Up:
                return 1;
            case Cell.Direction.Right:
                return 0;
            case Cell.Direction.Down:
                return -1;
            case Cell.Direction.Left:
                return 0;
            default:
                return 0;
        }
    }

    private int DirectionToZ(Cell.Direction direction)
    {
        switch (direction)
        {
            case Cell.Direction.Up:
                return 0;
            case Cell.Direction.Right:
                return -1;
            case Cell.Direction.Down:
                return 0;
            case Cell.Direction.Left:
                return 1;
            default:
                return 0;
        }
    }

    private Cell.Direction OppositeDirection(Cell.Direction direction)
    {
        switch (direction)
        {
            case Cell.Direction.Up:
                return Cell.Direction.Down;
            case Cell.Direction.Right:
                return Cell.Direction.Left;
            case Cell.Direction.Down:
                return Cell.Direction.Up;
            case Cell.Direction.Left:
                return Cell.Direction.Right;
            default:
                return Cell.Direction.Up;
        }
    }
}
