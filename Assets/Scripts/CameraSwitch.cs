using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera currentCamera;  // The camera currently in use
    public Camera newCamera;      // The new camera to switch to when the plane is touched

    void Start()
    {
        currentCamera.gameObject.SetActive(true);
        newCamera.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure it's the player that triggers the camera switch
        {
            // Disable the current camera and enable the new camera
            currentCamera.gameObject.SetActive(false);
            newCamera.gameObject.SetActive(true);
            
            // Call EndGame on the ScoreManager
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.EndGame();
            }
        }
    }
}
