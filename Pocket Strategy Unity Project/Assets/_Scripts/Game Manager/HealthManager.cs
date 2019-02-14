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
    public Text scoreText;
    public EnemySpawnerFinal espawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health.ToString();

        if (health <= 0)
        {
            gameMusic.Stop();
            //gameOverMusic.Play();
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver.SetActive(true);
        scoreText.text = "You survived " + espawner.currentWave.ToString() + " waves!";

        Time.timeScale = 0f;

        if (Input.GetKey(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}
