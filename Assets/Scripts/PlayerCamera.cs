using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivityX = 200f;
    public float sensitivityY = 200f;

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
        // Process mouse input for rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;
        float joystickX = Input.GetAxisRaw("Joystick X") * Time.deltaTime * sensitivityX * 2;
        float joystickY = Input.GetAxisRaw("Joystick Y") * Time.deltaTime * sensitivityY * 2;
        yRotation += mouseX + joystickX;
        xRotation -= mouseY + joystickY;

        // Camera rotation and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void ResetCamera()
    {
        // Reset camera rotation
        xRotation = 0f;
        yRotation = 0f;
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        // Reset the mouse input axes to zero
        Input.ResetInputAxes();
    }
}
