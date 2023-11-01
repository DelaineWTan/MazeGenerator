using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x;
    public int z;
    public bool visited;

    public GameObject wallTop;
    public GameObject wallRight;
    public GameObject wallBottom;
    public GameObject wallLeft;

    public void RemoveWall(CellDirection direction)
    {
        GameObject wallObject = GetWall(direction);
        if (wallObject != null)
        {
            Destroy(wallObject); // Destroy the wall GameObject.
        }
    }

    private GameObject GetWall(CellDirection direction)
    {
        switch (direction)
        {
            case CellDirection.Up:
                return wallTop;
            case CellDirection.Right:
                return wallRight;
            case CellDirection.Down:
                return wallBottom;
            case CellDirection.Left:
                return wallLeft;
            default:
                return null;
        }
    }
}
