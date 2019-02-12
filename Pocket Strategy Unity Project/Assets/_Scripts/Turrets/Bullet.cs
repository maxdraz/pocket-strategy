using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public Transform target;
    public float damage = 2f;
    float rotSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        // MOVING BULLET TOWARDS TARGET
        Vector3 toTarget = target.position - this.transform.localPosition;


        float distThisFrame = speed * Time.deltaTime;
        if (toTarget.magnitude <= distThisFrame)
        {
            //we reached the enemy
            HitTarget();
        }
        else
        {
            //Move towards node
            transform.Translate(toTarget.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(toTarget);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
        }
    }

    void HitTarget()
    {
        //do damage
        target.GetComponent<EnemyFinal>().TakeDamage(damage);
        //destroy bullet
        Destroy(this.gameObject);

    }
}
