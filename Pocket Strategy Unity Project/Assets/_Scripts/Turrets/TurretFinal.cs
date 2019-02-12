using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFinal : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float fireCD;
    float fireCDRemaining = 0;

    public Queue<GameObject> enemies = new Queue<GameObject>();
    public GameObject nearestEnemy;

    public Transform turretTransform;
  

   

    private void Update()
    {
        Debug.Log(enemies.Count);

        ////if enemy has died, remove it from queue 
        //if (enemies != null)
        //{
        //    foreach (GameObject enemy in enemies)
        //    {
               
                    
        //        if(enemy == null)
        //        {
        //            enemies.Dequeue();
        //        }
               
        //    }
        //    //nearestEnemy = enemies.Dequeue();
        //    //turretTransform.LookAt(nearestEnemy.transform);
        //}

        ////turretTransform.LookAt(nearestEnemy.transform);

        ////get the nearest enemy
        //if (enemies != null)
        //{
        //    nearestEnemy = enemies.Dequeue();
            
        //}
        if(nearestEnemy == null)
        {
            //Reset turret rotation
            RotateTurret(transform.position + transform.forward, 2f);
            //Find New Enemy
            FindNewEnemy();
        }
        else if(nearestEnemy)
        {
            Fire();
        }
        
    }

    void FindNewEnemy()
    {
        if(enemies!= null)
        {
            nearestEnemy = enemies.Dequeue();
        } else
            return;
    }

    void Fire()
    {
        fireCDRemaining -= Time.deltaTime;
        if(fireCDRemaining <= 0)
        {
            fireCDRemaining = fireCD;
            //instantiate bullet
            GameObject bullet = (GameObject)Instantiate(bulletPrefab);
            bullet.transform.position = bulletSpawn.position;
            bullet.GetComponent<Bullet>().target = nearestEnemy.transform;
        }
        //rotate turret
        RotateTurret(nearestEnemy.transform.position, 5f);
        
        
    }

    void RotateTurret(Vector3 target, float lerpSpeed)
    {
        Vector3 dir = target - turretTransform.position;
        Quaternion LookRot = Quaternion.LookRotation(dir);
        turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, Quaternion.Euler(0, LookRot.eulerAngles.y, 0), lerpSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            
            enemies.Enqueue(other.gameObject);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.tag == "Enemy")
        {
            nearestEnemy = null; 
          // enemies.Dequeue();
        }
    }
}
