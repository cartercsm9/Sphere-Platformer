using UnityEngine;

public class PlaneTrigger : MonoBehaviour
{
    public CameraFollow cameraSpinScript;  // Reference to the CameraSpin script on the camera

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the collider is tagged as "Player"
        {
            cameraSpinScript.TriggerRotation();
        }
    }
}
