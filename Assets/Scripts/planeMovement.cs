using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeMovement : MonoBehaviour
{
   public float speed = 5.0f; // Speed at which the object will move forward

    void Update()
    {
        // Move the object forward over time at a constant speed
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
