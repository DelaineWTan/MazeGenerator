using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivityX = 400f;
    public float sensitivityY = 400f;

    public Transform orientation;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Check if the reset key is pressed on the keyboard (Home key) or gamepad (Fire1 button, e.g., A on Xbox controller)
        if (Input.GetKeyDown(KeyCode.Home) || Input.GetButtonDown("Fire1"))
        {
            // Reset camera rotation
            xRotation = 0f;
            yRotation = 0f;
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            // Reset the mouse input axes to zero
            Input.ResetInputAxes();
        }

        // Process mouse input for rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;
        yRotation += mouseX;
        xRotation -= mouseY;

        // Camera rotation and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
