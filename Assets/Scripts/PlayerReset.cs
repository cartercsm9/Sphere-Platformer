using UnityEngine;
public class ResetPlayerOnTouch : MonoBehaviour
{
    public Vector3 resetPosition = new Vector3(-1.2f, 10f, 4.5f); // Position to reset the player

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Reset the player's position
            other.transform.position = resetPosition;
            Debug.Log("Player position reset to: " + resetPosition);
        }
    }
}
