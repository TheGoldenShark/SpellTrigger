using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
