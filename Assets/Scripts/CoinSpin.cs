using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float speed = 100f;  // Rotation speed in degrees per second

    void Update()
    {
        // Rotate around the y-axis at 'speed' degrees per second
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
