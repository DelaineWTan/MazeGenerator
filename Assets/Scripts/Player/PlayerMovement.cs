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

    [SerializeField] GameObject WallCollideSFX;
    [SerializeField] GameObject FootstepsSFX;
    [SerializeField] GameObject PlayerDeathSFX;
    private AudioSource footstepsAudioSrc;
    private bool IsDead;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startingPosition = new Vector3(Mathf.Floor(mazeGenerator.width / 2), 0, Mathf.Floor(mazeGenerator.height / 2));
        ResetPlayerPosition();

        // Initialize and configure the audio source for footsteps
        footstepsAudioSrc = PlaySfx.PlayWithLoop(FootstepsSFX, transform).GetComponent<AudioSource>();
        IsDead = false;
    }

    private void Update()
    {
        if (IsDead)
            return;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        // Toggle collision on keyboard "SPACE" key press or gamepad "B" button
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
            playerCapsuleCollider.enabled = !playerCapsuleCollider.enabled;

        // Check if player is moving horizontally and play footsteps sound on loop
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            footstepsAudioSrc.mute = false;
        }
        else
        {
            footstepsAudioSrc.mute = true; // Stop footsteps sound if not moving
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            PlaySfx.PlayThenDestroy(WallCollideSFX, transform);
        if (collision.gameObject.tag == "EnemyMesh" && !IsDead)
        {
            Invoke("Reset", 5);
            PlaySfx.PlayThenDestroy(PlayerDeathSFX, transform);
            IsDead = true;
        }
    }

    private void Reset()
    {
        ResetGame ResetGame = FindFirstObjectByType<ResetGame>();
        ResetGame.Reset();
        IsDead = false;
    }
}
