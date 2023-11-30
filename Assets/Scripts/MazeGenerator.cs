using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.AI.Navigation;

public class MazeGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public GameObject cellPrefab;

    public GameObject exitTrigger;
    private Cell[,] grid;

    public NavMeshSurface surface;

    public Material pongDoorMat;

    private void Start()
    {
        grid = new Cell[width, height];
        InitializeGrid();
        RecursiveBacktracking(0, 0);
        surface.BuildNavMesh();
        CreateExit();
        CreatePongDoor();
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject cellObject = Instantiate(cellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                cellObject.transform.parent = transform;
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

    private void CreateExit()
    {
        // Choose a random border position for the exit
        int borderPosition = Random.Range(0, 4); // 0: Top, 1: Right, 2: Bottom, 3: Left
        int exitX = 0, exitZ = 0;
        float exitXOffset = 0, exitZOffset = 0, exitXScale = 1, exitZScale = 1;

        switch (borderPosition)
        {
            case 0: // Top border
                exitX = width - 1;
                exitZ = Random.Range(0, width);
                grid[exitX, exitZ].RemoveWall(Cell.Direction.Up);
                exitXOffset = 0.5f;
                exitXScale = 0.1f;
                break;
            case 1: // Right border
                exitX = Random.Range(0, width);
                exitZ = 0;
                grid[exitX, exitZ].RemoveWall(Cell.Direction.Right);
                exitZOffset = -0.5f;
                exitZScale = 0.1f;
                break;
            case 2: // Bottom border
                exitX = 0;
                exitZ = Random.Range(0, width);
                grid[exitX, exitZ].RemoveWall(Cell.Direction.Down);
                exitXOffset = -0.5f;
                exitXScale = 0.1f;
                break;
            case 3: // Left border
                exitX = Random.Range(0, width);
                exitZ = height - 1;
                grid[exitX, exitZ].RemoveWall(Cell.Direction.Left);
                exitZOffset = 0.5f;
                exitZScale = 0.1f;
                break;
        }

        // Instantiate the exit trigger prefab at the determined exitX and exitZ
        exitTrigger.transform.SetPositionAndRotation(new Vector3(exitX + exitXOffset, -0.5f, exitZ + exitZOffset), Quaternion.identity);
        exitTrigger.transform.localScale = new Vector3(exitXScale, 0.1f, exitZScale);
    }

    private void CreatePongDoor() {
        System.Random rng = new System.Random();
        int x = rng.Next(0, width);
        int y = rng.Next(0, height);

        Cell cell = grid[x, y];
        Transform[] walls = cell.transform.GetComponentsInChildren<Transform>(false);

        int wallIndex = rng.Next(1, walls.Length - 1); // 1 start to exclude parent, -1 boundary to exclude ground.
        GameObject randomWall = walls[wallIndex].gameObject;
        randomWall.name = "PongDoor"; // remove this after
        randomWall.GetComponent<Renderer>().material = pongDoorMat;
        randomWall.AddComponent<PongDoor>();

    }

}
