using UnityEngine;

public class SphereMovement : MonoBehaviour
{   
    public float speed = 5.0f;
    public float jumpForce = 7.0f;
    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpRequested;
    private Transform cameraTransform;
    private Vector3 resetPosition = new Vector3(-1f, 10f, 4.5f);
    public CameraFollow cameraScript;  // Reference to the CameraFollow script

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        cameraScript = Camera.main.GetComponent<CameraFollow>();  // Get the CameraFollow script from the main camera
    }

    void Update()
    {
        CheckGroundStatus();
        CheckJumpInput();

        if (transform.position.y < -30f)
        {
            ResetPlayerAndCamera();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        ApplyVelocityDeadZone();
        ProcessJumpRequest();
    }

    private void ResetPlayerAndCamera()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = resetPosition;
        if (cameraScript != null)
        {
            cameraScript.ResetToInitialPosition();  // Call to reset the camera
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hit;
        float sphereRadius = 0.5f;
        float checkDistance = sphereRadius + 0.1f;
        Vector3 origin = transform.position + Vector3.up * sphereRadius;
        isGrounded = Physics.SphereCast(origin, sphereRadius, Vector3.down, out hit, checkDistance);

        Debug.DrawLine(origin, origin + Vector3.down * checkDistance, isGrounded ? Color.green : Color.red);
    }

    void CheckJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequested = true;
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Apply a dead zone to horizontal and vertical inputs
        if (Mathf.Abs(moveHorizontal) < 0.1f) moveHorizontal = 0;
        if (Mathf.Abs(moveVertical) < 0.1f) moveVertical = 0;



        // Only move the player if there is meaningful input
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;  // Neutralize pitch
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 moveDirection = (forward * moveVertical + right * moveHorizontal).normalized;
            rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }

    void ProcessJumpRequest()
    {
        if (jumpRequested && isGrounded)
        {
            Jump();
            jumpRequested = false;
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Jump Executed; Force Applied: " + jumpForce);
    }

    void ApplyVelocityDeadZone()
    {
        Debug.Log("Current Velocity Magnitude: " + rb.velocity.magnitude);

        // Create a new vector with only the horizontal components of the velocity
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        // Check if the magnitude of the horizontal velocity is below the threshold
        if (horizontalVelocity.magnitude < 0.4f)
        {
            // Zero out only the horizontal components
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

}
