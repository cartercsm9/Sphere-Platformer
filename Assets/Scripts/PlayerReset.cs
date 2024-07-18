using UnityEngine;

public class ResetPlayerOnTouch : MonoBehaviour
{
    public Vector3 resetPosition = new Vector3(-1.2f, 10f, 4.5f); // Position to reset the player
    public CameraFollow gameCamera; // Reference to the CameraFollow script

    private void Start()
    {
        // Find the camera with CameraFollow script and get the component
        gameCamera = FindObjectOfType<CameraFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Reset the player's position
            other.transform.position = resetPosition;

            // Ensure the gameCamera is not null
            if (gameCamera != null)
            {
                gameCamera.ResetToInitialPosition();
            }
            else
            {
                Debug.LogError("CameraFollow script not found on any camera!");
            }

            Debug.Log("Player position reset to: " + resetPosition);
        }
    }
}
