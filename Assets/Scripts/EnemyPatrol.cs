using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == patrolPoints[targetPoint].position){
            increaseTargetInt();
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position,speed * Time.deltaTime);
    } 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SphereMovement playerScript = other.GetComponent<SphereMovement>();
            if (playerScript != null)
            {
                playerScript.ResetPlayerAndCamera();
            }
            // Access the singleton instance directly
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.ResetScore();
            }
        }
    }
    void increaseTargetInt(){
        targetPoint++;

        if(targetPoint>=patrolPoints.Length){
            targetPoint = 0;
        }
    }
}