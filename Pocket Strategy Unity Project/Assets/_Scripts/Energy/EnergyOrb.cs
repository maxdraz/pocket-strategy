using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnergyOrb : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
        {
            Debug.Log("moving to target");
            agent.SetDestination(target.position);

            if (agent.remainingDistance <= 0.1f)
            {
                Destroy(gameObject);
            }
        }

        
    }

    public void CalculateSpeed(float timeToReach)
    {
        Vector3 toTarget = target.position - transform.position;
        float distance = Vector3.Magnitude(toTarget);

        float speed = distance / timeToReach;

       agent.speed = speed;
    }

   



    //public IEnumerator TravelTo(Transform target, float timeToTravel)
    //{
    //    while (true)
    //    {
    //        Vector3 toTarget = target.transform.position - transform.position;
    //        float speed = toTarget.magnitude / timeToTravel;
    //        toTarget.Normalize();

    //        transform.Translate(toTarget * speed, Space.World);

    //        if (toTarget.magnitude <= 2f)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
        
    //}
}
