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
        transform.position = startingPosition;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // Reset position on keyboard "HOME" key press or gamepad "Fire1" (A) button
        if (Input.GetKeyDown(KeyCode.Home) || Input.GetButtonDown("Fire1"))
            transform.position = startingPosition;
        // Toggle collision on keyboard "SPACE" key press or gamepad "Fire2" (B) button
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            playerCapsuleCollider.enabled = !playerCapsuleCollider.enabled;
    }

    private void FixedUpdate()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
