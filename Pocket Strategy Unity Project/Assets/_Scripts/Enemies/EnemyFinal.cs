using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFinal : MonoBehaviour
{
    public float health = 5f;
    public float damage;
    public float worth = 10f;

    public GameObject DeathExplosion, AttackExplosion;

    public Transform baseTransform;
    NavMeshAgent agent;

    GameObject gm;

    public AudioSource enemyAudio;
    public AudioClip enemyClip;

    private void Start()
    {
        gm = GameObject.FindWithTag("GM");
        agent = GetComponent<NavMeshAgent>();
        enemyAudio = GetComponent<AudioSource>();

        enemyAudio.clip = enemyClip;

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
            enemyAudio.Play();
            Die();
            DeathExplode(); //Instantiating method
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
            enemyAudio.Play();
            //deal damage
            //destroy yourself
            DamageBase(damage);
            Destroy(gameObject);
            AttackExplode(); //Instantiating Method
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

    //Particle system that plays when turret kills enemy
    void DeathExplode()
    {
        GameObject firework = Instantiate(DeathExplosion, transform.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();
    }

    //Particle system that plays when enemy reaches base
    void AttackExplode()
    {
        GameObject firework = Instantiate(AttackExplosion, transform.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();
    }
}
