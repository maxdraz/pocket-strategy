using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFinal : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float damage;
    float originalDamage;
    public float damageUpgrade;
    public float fireCD;
    float originalFireCD;
    public float cooldownUpgrade = 0.5f;
    float fireCDRemaining = 0;
    public int energy;
    public int maxEnergy = 5;
    public float energyCD;
    public TextMesh energyText;

    public Queue<GameObject> enemies = new Queue<GameObject>();
    public GameObject nearestEnemy;

    public Transform turretTransform;
    GameObject gm;

    public TextMesh fireRateUp;
    public TextMesh damageUp;
    public bool receivedEnergy = false;

    public int numClicks;
 



    private void Start()
    {
        gm = GameObject.FindWithTag("GM");
        originalDamage = damage;
        originalFireCD = fireCD;
    }

    private void Update()
    {

        

        if(numClicks >= maxEnergy)
        {
            numClicks = maxEnergy;
        }
        if(numClicks <= 0)
        {
            numClicks = 0;
        }

        if (receivedEnergy)
        {

            CheckForUpgrades();
            receivedEnergy = false;

        }

        

        //energy text
        energyText.text = energy.ToString() + "/" + maxEnergy.ToString();

        if (nearestEnemy == null)
        {
            //Reset turret rotation
            RotateTurret(transform.position + transform.forward, 2f);
            fireCDRemaining = fireCD;
        }

        if (nearestEnemy == null && enemies.Count >= 1)
        {
            //Find New Enemy
            FindNewEnemy();
        }
        else if (nearestEnemy && energy > 0)
        {
            Fire();
        }

    }

    void FindNewEnemy()
    {
        if (enemies == null)
        {
            return;
        }
        else
        {
            nearestEnemy = enemies.Dequeue();
        }

    }

    void Fire()
    {
        Debug.Log(fireCDRemaining);
        fireCDRemaining -= Time.deltaTime;
        if (fireCDRemaining <= 0)
        {
            fireCDRemaining = fireCD;
            //instantiate bullet
            GameObject bullet = (GameObject)Instantiate(bulletPrefab);
            bullet.transform.position = bulletSpawn.position;
            bullet.GetComponent<Bullet>().damage = damage;
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
        if (other.tag == "Enemy")
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

    void CheckForUpgrades()
    {
        if (energy < 2)
        {
            energyText.color = Color.white;
            fireCD = originalFireCD;
            damage = originalDamage;
            
        } else if (energy == 2)
        {
            FireRateUp();


        } else if (energy == maxEnergy) {

            DamageUpgrade();
            damageUp.gameObject.SetActive(true);
            
            
        }
    }


   

     void FireRateUp()
    {
        fireRateUp.gameObject.SetActive(true);
        fireCD = cooldownUpgrade;
        energyText.color = fireRateUp.color;
        
    }

    void DamageUpgrade()
    {
        damage = damageUpgrade;
        energyText.color = damageUp.color;
    }
}
