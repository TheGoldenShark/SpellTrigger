using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject announcementPrefab;
    public float difficulty = 1;
    private PersistentData data;
    // Start is called before the first frame update
    private void Start()
    {
        // Loads the level
        data = GameObject.Find("Data").GetComponent<PersistentData>();
        SceneManager.LoadScene("Level", LoadSceneMode.Additive);
    }

    public void gameOver()
    {
        // Loads the game over scene, stores the current score, and saves
        data.StoreScore();
        SceneManager.UnloadSceneAsync("Main");
        SceneManager.UnloadSceneAsync("Level");
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
        SaveManager.SaveSaveData(new SaveData(data));
    }

    public void Announcement(string message)
    {
        // Places the announcement object and sets its text as the parameter
        GameObject announcementInstance = Instantiate(announcementPrefab, transform.position, Quaternion.identity) as GameObject;
        announcementInstance.GetComponentInChildren<Text>().text = message;
    }

    public void nextLevel()
    {
        // Increases the difficulty, announces the level number, unloads the current level and loads a new one
        difficulty += 1;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main"));
        Announcement("Level" + difficulty.ToString());
        SceneManager.UnloadSceneAsync("Level");
        SceneManager.LoadScene("Level", LoadSceneMode.Additive);
    }
}
