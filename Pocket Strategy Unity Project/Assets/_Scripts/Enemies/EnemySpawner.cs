using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class EnemySpawner : MonoBehaviour
{
    public float spawnCD = 0.5f;
   public float spawnCDRemaining = 0;
    public GameObject path;

    [System.Serializable]
    public class WaveComponent
    {
        public GameObject enemyPrefab;
        
        public int num;
        [System.NonSerialized]
        public int spawned = 0;
    }

    public WaveComponent[] waveComps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        spawnCDRemaining -= Time.deltaTime;
        if(spawnCDRemaining <= 0)
        {
            spawnCDRemaining = spawnCD;

            bool spawned = false;
            //spawn waves of enemies
            foreach (WaveComponent wc in waveComps)
            {
                if(wc.spawned < wc.num)
                {
                    wc.spawned++;
                    //spawn enemy!
                    Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation);
                    wc.enemyPrefab.GetComponent<Enemy>().PathGO = path;
                    spawned = true;
                    break;
                }
            }

            if(spawned == false)
            {
                Destroy(gameObject);
            }

        }
    }
}
