using UnityEngine;

public class TriggerCameraOffset : MonoBehaviour
{
    public CameraFollow cameraFollowScript;
    public Vector3 newOffset = new(0, 0, -10);  // New offset to apply

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (cameraFollowScript != null)
            {
                Debug.Log("adjusting camera");
                cameraFollowScript.UpdateCameraOffset(newOffset);
            }
        }
    }
}
