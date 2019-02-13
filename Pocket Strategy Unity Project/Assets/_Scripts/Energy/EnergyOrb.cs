using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour
{


    private void Update()
    {

    }

    public IEnumerator TravelTo(Transform target, float timeToTravel)
    {
        while (true)
        {
            Vector3 toTarget = target.transform.position - transform.position;
            float speed = toTarget.magnitude / timeToTravel;
            toTarget.Normalize();

            transform.Translate(toTarget * speed, Space.World);

            if (toTarget.magnitude <= 2f)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
