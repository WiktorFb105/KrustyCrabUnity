using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;

    public Transform orientation;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Camera looks up/down
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Player/Orientation looks left/right
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
