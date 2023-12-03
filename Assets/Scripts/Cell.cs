using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool visited;

    public GameObject wallUp;
    public GameObject wallRight;
    public GameObject wallDown;
    public GameObject wallLeft;


    // Enum for cell directions
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public void RemoveWall(Direction direction)
    {
        GameObject wallObject = GetWall(direction);
        if (wallObject != null)
            wallObject.SetActive(false);
    }

    private GameObject GetWall(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return wallUp;
            case Direction.Right:
                return wallRight;
            case Direction.Down:
                return wallDown;
            case Direction.Left:
                return wallLeft;
            default:
                return null;
        }
    }
}
