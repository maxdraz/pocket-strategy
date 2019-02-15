using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public float health = 100f;
    public Text healthText;

    public GameObject gameOver;
    public AudioSource gameMusic;
    public AudioSource gameOverMusic;
    public AudioSource baseDestroyed;
    public Text scoreText;
    public EnemySpawnerFinal espawner;
    bool gameOverBool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health.ToString();

      //  if (gameOverBool)
       // {
            if (health <= 0 )
            {
                gameMusic.Stop();
                //gameOverMusic.Play();
                GameOver();
                gameOverBool = false;
            }
       // }
    }

    void GameOver()
    {
        //audio for game over
        gameMusic.Stop();
        gameOverMusic.gameObject.SetActive(true);
        baseDestroyed.gameObject.SetActive(true);

        gameOver.SetActive(true);
        scoreText.text = "You survived " + (espawner.currentWave +1) + " waves!";

        Time.timeScale = 0f;

        if (Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
      
    }
}
