using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public float speed = 5.0f; // Ensure 'f' is used for float literals
    public float jumpForce = 7.0f;
    private Rigidbody rb;
    private bool isGrounded;
    private Transform cameraTransform;
    private Vector3 resetPosition = new Vector3(-1f, 10f, 4.5f); // Use 'f' for all float components

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Correct usage of 'f' in a float context
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (transform.position.y < -30f) // Use 'f' here
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = resetPosition;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        rb.MovePosition(transform.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
