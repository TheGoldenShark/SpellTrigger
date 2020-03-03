using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene, and unload the main menu
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
    public void QuitGame()
    {
        // Quits the game
        Application.Quit();
    }

    public void Scores()
    {
        // Loades the scores menu, and unloads the main menu
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("HighScores", LoadSceneMode.Additive);
    }
}
