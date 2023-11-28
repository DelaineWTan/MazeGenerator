using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5;
    [SerializeField] public Transform orientation;
    [SerializeField] public CapsuleCollider playerCapsuleCollider;
    [SerializeField] MazeGenerator mazeGenerator;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Vector3 startingPosition;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startingPosition = new Vector3(Mathf.Floor(mazeGenerator.width / 2), 0, Mathf.Floor(mazeGenerator.height / 2));
        ResetPlayerPosition();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // Toggle collision on keyboard "SPACE" key press or gamepad "B" button
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
            playerCapsuleCollider.enabled = !playerCapsuleCollider.enabled;
    }

    private void FixedUpdate()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    public void ResetPlayerPosition()
    {
        transform.position = startingPosition;
    }
}