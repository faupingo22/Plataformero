using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Transform cam; 

    public float playerSpeed = 6.0f;
    public float jumpForce = 8.0f;
    public float gravityMultiplier = 2.0f;
    public float turnSmoothTime = 0.1f; 
    private float turnSmoothVelocity;

    public float dashSpeed = 20.0f;
    public float dashDuration = 0.2f;
    private float dashTimer;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        float targetAngle = cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;

            if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer <= 0)
            {
                dashTimer = dashDuration;
            }

            if (dashTimer > 0)
            {
                controller.Move(moveDir.normalized * dashSpeed * Time.deltaTime);
                dashTimer -= Time.deltaTime;
            }
            else
            {
                controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = jumpForce;
        }

        playerVelocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }
}

