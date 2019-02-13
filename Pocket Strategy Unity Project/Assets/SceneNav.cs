using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneNav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadHowToPlayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainLevelScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
