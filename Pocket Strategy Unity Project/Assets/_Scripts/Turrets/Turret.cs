using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField]
    string enemyPathTag;
    [SerializeField]
    float turretRange= 10f;

    public Transform turretTransform;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public float fireCooldown = 0.5f;
    float fireCooldownLeft;
    public GameObject[] enemies;
    public GameObject nearestEnemyGO;
    public GameObject enemy;

    
    public int energy;
    public GameObject textDisplay;
    public EnergyManager em;

    // Start is called before the first frame update
    void Start()
    {
        //turretTransform = transform.Find("Turret");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckMouseInput());  
        textDisplay.GetComponent<TextMesh>().text = energy.ToString();
        //QUICKLY DOUBLING FIRE RATE      CHANGE THIS LATER!!!!
        if(energy >= 2)
        {
            fireCooldown = 0.25f;
        }
        else
        {
            fireCooldown = 0.5f;
        }
        enemies = GameObject.FindGameObjectsWithTag(enemyPathTag);

        nearestEnemyGO = null;
        float distToNearest = Mathf.Infinity;

        foreach(GameObject e in enemies)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            
            if(nearestEnemyGO == null || d < distToNearest)
            {
                nearestEnemyGO = e;
                distToNearest = d;
                
            }
        }
        if(nearestEnemyGO == null )
        {
            return;
        }
        // ROTATING THE TURRET
        Vector3 dir = nearestEnemyGO.transform.position - this.transform.position;
        //get rotation that brings from current to dir (using lookROtation)
        Quaternion lookRot = Quaternion.LookRotation(dir);
        turretTransform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);

        //FIRING
        fireCooldownLeft -= Time.deltaTime;
        if(fireCooldownLeft <= 0 && dir.magnitude <= turretRange && energy >=1)
        {
            fireCooldownLeft = fireCooldown;
            Shoot(nearestEnemyGO);
        }


        
    }

    void Shoot(GameObject enemy)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Bullet b = bulletGO.GetComponent<Bullet>();

        b.target = enemy.transform;
    }

   

    IEnumerator CheckMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    //add energy
                    AddEnergy();
                    Debug.Log(gameObject.name + "Was left clicked");
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    //add energy
                    RemoveEnergy();
                    Debug.Log(gameObject.name + "Was right clicked");
                }
            }
        }

        yield return null;
    }

    void AddEnergy()
    {
        //do these in methods later
        if (em.energy <= 0)
        {
            return;
        }
        em.energy -= 1;
        energy += 1;

    }

    void RemoveEnergy()
    {
        if(energy <= 0)
        {
            return;
        }

        energy -= 1;
        em.energy += 1;
    }
}
