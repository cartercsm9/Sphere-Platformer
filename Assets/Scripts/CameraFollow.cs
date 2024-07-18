using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The player to follow
    public float distance = 10.0f;  // Distance behind the player
    public float height = 5.0f;     // Height above the player
    public float followSmoothSpeed = 0.1f;  // Smoothing speed for following
    private bool shouldRotate = false;
    private Vector3 initialPosition;
    private float rotationDuration = 0.5f;  // Total time to complete 180 degrees rotation
    private float rotationTimeElapsed = 0f;
    public Vector3 cameraOffset; 

    void Start()
    {
        initialPosition = new Vector3(-3.3f, 12.0f, 14.4f);
        transform.position = initialPosition;
        transform.LookAt(target);
        cameraOffset = new Vector3(0,0,-3);
    }

    void Update()
    {
        if (target == null) return;

        if (shouldRotate)
        {
            RotateAroundPlayer();
        }
        else
        {
            FollowPlayer();
        }
    }

    Vector3 CalculatePosition(Vector3 basePosition)
    {
        // Calculate the offset position from the target
        return basePosition - target.forward * distance + Vector3.up * height - cameraOffset;
    }

    void FollowPlayer()
    {
        // Lerp to the calculated position for smooth following
        Vector3 targetPosition = CalculatePosition(target.position);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSmoothSpeed * Time.deltaTime);
        transform.LookAt(target);
    }

    void RotateAroundPlayer()
    {
        if (rotationTimeElapsed < rotationDuration)
        {
            float rotationStep = (180 / rotationDuration) * Time.deltaTime;
            transform.RotateAround(target.position, Vector3.up, rotationStep);
            rotationTimeElapsed += Time.deltaTime;
        }
        else
        {
            shouldRotate = false;
            rotationTimeElapsed = 0f; // Reset for potential future rotations
            transform.LookAt(target);  // Ensure the camera focuses on the player
        }
    }

    public void TriggerRotation()
    {
        if (!shouldRotate)
        {
            shouldRotate = true;
            rotationTimeElapsed = 0f;  // Reset rotation timer
        }
    }
    public void ResetToInitialPosition()
    {
        initialPosition = CalculatePosition(target.position);
        transform.position = initialPosition;
        transform.LookAt(target);
        Debug.Log("Camera reset to initial position.");
    }
    public void UpdateCameraOffset(Vector3 offset)
    {
        cameraOffset = offset;
    }
}

