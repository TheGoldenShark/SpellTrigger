using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public GameObject announcementPrefab;
    public float difficulty = 1;
    // Start is called before the first frame update
    private void Start()
    {
        // Loads the level
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }


    // Update is called once per frame
    private void Update()
    {
        
    }

    public void gameOver()
    {
        // Loads the game over scene
        SceneManager.LoadScene(2);
    }

    public void Announcement(string message)
    {
        GameObject announcementInstance = Instantiate(announcementPrefab, transform.position, Quaternion.identity) as GameObject;
        announcementInstance.GetComponentInChildren<Text>().text = message;
    }

    public void nextLevel()
    {
        difficulty += 1;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        Announcement("Level" + difficulty.ToString());
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
