using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Scores()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("HighScores", LoadSceneMode.Additive);
    }
}
