using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject PathGO;

    public int moneyworth;
    public float speed = 5f;
    public float health = 2f;
    public float damage = 1f;
    public float rotSpeed = 2f;

    MoneyManager mm;
    Transform targetWaypoint;
    int waypointIndex = 0;
    HealthManager hm;

    private void Start()
    {
        mm = GameObject.FindWithTag("GM").GetComponent<MoneyManager>();
        hm = GameObject.FindWithTag("GM").GetComponent<HealthManager>();
    }

    void GetNextWaypoint()
    {
        if(targetWaypoint == null)
        {
            ReachedBase();
        }
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
        DealDamage(damage);
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
        mm.AddMoney(moneyworth);
        Destroy(this.gameObject);
    }

    void DealDamage(float damage)
    {

        hm.health -= damage;

    }
}
