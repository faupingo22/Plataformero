using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float mouseSensitivity = 100f;
    public Vector2 pitchMinMax = new Vector2(-40, 85); 
    public float rotationSmoothTime = 0.12f; 

    public float distanceFromTarget = 5.0f;

    private float yaw; 
    private float pitch; 
    private Vector3 rotationSmoothVelocity;
    private Vector3 currentRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y); 

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        Vector3 cameraPosition = target.position - transform.forward * distanceFromTarget;

        cameraPosition.y = target.position.y + 1.5f;

        transform.position = cameraPosition;
    }
}

