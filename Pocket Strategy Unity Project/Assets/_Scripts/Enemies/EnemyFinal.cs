using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFinal : MonoBehaviour
{
    public float health = 5f;
    public float damage;

    public Transform baseTransform;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(baseTransform == null)
        {
            baseTransform = GameObject.FindWithTag("Base").transform;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo(baseTransform);

        if(health <= 0)
        {
            Die();
        }
       
    }

    private void MoveTo(Transform b)
    {
        agent.SetDestination(b.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Base")
        {
            //deal damage
            //destroy yourself
            
            Destroy(gameObject);
        }
    }

   public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
