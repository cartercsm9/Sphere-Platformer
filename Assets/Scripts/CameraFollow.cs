using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 10.0f; // Distance from the target
    public float height = 5.0f;    // Height above the target
    public float smoothSpeed = 0.5f; // Smoothing speed for movement
    public float rotationSpeed = 5.0f; // Speed of camera rotation after delay
    public float directionHoldTime = 3.0f; // Delay before camera starts rotating

    private float rotationTimer = 0.0f;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = target.position;
        // Initialize the camera's initial position
        // Adjust the initial position so the camera looks in the -z direction from the start
        Vector3 initialPosition = target.position + target.forward * distance + Vector3.up * height;
        transform.position = initialPosition;
        transform.LookAt(target); // Ensure the camera initially faces the target
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Check if the target has moved significantly and if it is moving
        if (Vector3.Distance(lastPosition, target.position) > 0.01f)
        {
            rotationTimer += Time.deltaTime;
            lastPosition = target.position;
        }
        else
        {
            rotationTimer = 0.0f; // Reset the timer if the target stops moving
        }

        // Calculate the current height and position of the camera
        float wantedHeight = target.position.y + height;
        float currentHeight = transform.position.y;

        // Always update the height of the camera smoothly
        Vector3 upPosition = transform.position;
        upPosition.y = Mathf.Lerp(currentHeight, wantedHeight, smoothSpeed * Time.deltaTime);

        // Determine if the camera should rotate based on the timer
        if (rotationTimer > directionHoldTime)
        {
            // Calculate the current rotation angles
            float wantedRotationAngle = target.eulerAngles.y;
            float currentRotationAngle = transform.eulerAngles.y;

            // Smoothly rotate towards the desired angle
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationSpeed * Time.deltaTime);

            // Update the camera's rotation
            transform.rotation = Quaternion.Euler(0, currentRotationAngle, 0);
        }

        // Calculate position based on the updated rotation
        Vector3 newPosition = target.position - transform.forward * distance + Vector3.up * height;
        transform.position = Vector3.Lerp(upPosition, newPosition, smoothSpeed * Time.deltaTime);

        // Always look at the target
        transform.LookAt(target);
    }
}
