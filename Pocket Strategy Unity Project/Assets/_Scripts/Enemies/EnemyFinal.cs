using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFinal : MonoBehaviour
{
    public float health = 5f;
    public float damage;
    public float worth = 10f;

    public Transform baseTransform;
    NavMeshAgent agent;

    GameObject gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM");
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
            DamageBase(damage);
            Destroy(gameObject);
        }
    }

   public void TakeDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            //add money
            gm.GetComponent<MoneyManager>().AddMoney(worth);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void DamageBase(float dmg)
    {
        gm.GetComponent<HealthManager>().health -= dmg;
    }
}
