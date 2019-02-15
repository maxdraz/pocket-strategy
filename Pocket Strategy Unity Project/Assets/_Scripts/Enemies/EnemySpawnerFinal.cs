using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnerFinal : MonoBehaviour
{

    public float waveCD = 2f;
    float waveCDRemaining = 2f;
    public float spawnCD = 0.5f;
    float spawnCDRemaining;
    public Transform[] spawnPoints;
    public List<GameObject> activeEnemies;

    public int currentWave = 0;
  
    public Text waveText;
    public bool spawning = false;

    

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemies;
        public int numToSpawn;
        //[System.NonSerialized]
        public int spawned;
    }

    public Wave[] waves;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new Transform[transform.childCount];
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        waveText.text = "Wave: " + (currentWave + 1);
       
    }

    // Update is called once per frame
    void Update()
    {
        
       

        //start wave cd
        if (!spawning)
        {
            Debug.Log("waveCdD: " + waveCDRemaining);
            waveCDRemaining -= Time.deltaTime;
        }
        //when it reach 0, start new wave
        if (waveCDRemaining <= 0)
        {
            spawning = true;


            //spawning = true;
            //Debug.Log("spawnCD " + spawnCDRemaining);
            //spawnCDRemaining -= Time.deltaTime;
            //if (spawnCDRemaining <= 0)
            //{
            //    Debug.Log("Spawning new Enemy!");
            //    spawnCDRemaining = spawnCD;

            //    //Go into wave component





            //}
            //currentWave++;
            //waveCDRemaining = waveCD;
        }

        if (spawning)
        {
           

            // in case we go above wave length
            if (currentWave >= waves.Length)
            {
                return;
            }
            // StartCoroutine(SpawnEnemies(spawnCD));
            spawnCDRemaining -= Time.deltaTime;
            if (spawnCDRemaining <= 0)
            {
               
                spawnCDRemaining = spawnCD;
               
                
                    if (waves[currentWave].spawned < waves[currentWave].numToSpawn)
                    {
                        waves[currentWave].spawned++;
                    //instantiate enemy prefab
                    GameObject enemy = (GameObject)Instantiate(waves[currentWave].enemies[Random.Range(0, waves[currentWave].enemies.Length)]);
                    //set its position to spawn point
                    enemy.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                    activeEnemies.Add(enemy);
                    }

                if (activeEnemies[activeEnemies.Count - 1] == null)
                {
                    activeEnemies.Clear();
                    StartNewWave();
                }


                //waveCDRemaining = waveCD;
                //spawning = false;
                //waveCDRemaining = waveCD;
            }
        }

        //IEnumerator SpawnEnemies(float t)
        //{
        //    for(int i = 0; i < waves.Length; i++)
        //    {
        //        if(waves[currentWave].spawned < waves[currentWave].numToSpawn)
        //        {
        //            waves[currentWave].spawned++;
        //            Debug.Log("Spawned " + waves[currentWave].enemies[0]);
        //            yield return new WaitForSeconds(t);
        //        }

        //        spawning = false;

        //    }


        //}
    }

    void StartNewWave()
    {
        
        waveCDRemaining = waveCD;
        currentWave++;
        spawning = false;

        waveText.text = "Wave: " + (currentWave + 1);
    }
}
