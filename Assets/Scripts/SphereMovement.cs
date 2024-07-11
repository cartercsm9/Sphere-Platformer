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
        // Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;  // Neutralize pitch
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveVertical + right * moveHorizontal).normalized;
        rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    void ProcessJumpRequest()
    {
        if (jumpRequested && isGrounded)
        {
            Jump();
            jumpRequested = false; // Reset jump request after processing
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Debug.Log("Jump Executed; Force Applied: " + jumpForce);
    }
}
