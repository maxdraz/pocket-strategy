using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject PathGO;

    public float speed = 5f;
    public float health = 2f;
    public float rotSpeed = 2f;

    Transform targetWaypoint;
    int waypointIndex = 0;

    void GetNextWaypoint()
    {
        targetWaypoint = PathGO.transform.GetChild(waypointIndex);
        waypointIndex++ ;
    }

    private void Update()
    {
        //setting next waypoint to follow
        if (targetWaypoint == null)
        {
            GetNextWaypoint();
       if(targetWaypoint == null)
        {
            ReachedBase();
        }
       }

        //moving the enemy towards that waypoint
        Vector3 toTarget = targetWaypoint.position - this.transform.localPosition;
        

        float distThisFrame = speed * Time.deltaTime;
        if(toTarget.magnitude <= distThisFrame)
        {
            //we reached the node
            GetNextWaypoint();
        }
        else
        {
            //Move towards node
            transform.Translate(toTarget.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(toTarget);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }

    void ReachedBase()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
