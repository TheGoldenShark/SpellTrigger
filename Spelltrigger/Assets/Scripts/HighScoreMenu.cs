using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreMenu : MonoBehaviour
{
    public GameObject scorePrefab;
    private PersistentData data;
    private List<float> scoreList;
    private GameObject canvas;
    private int noScores;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("Data").GetComponent<PersistentData>();
        canvas = GameObject.Find("Canvas");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("HighScores"));
        PlaceScores();
    }

    void PlaceScores()
    {
        // Gets and sorts the scorelist in descending order
        scoreList = data.GetScoreList();
        scoreList.Sort();
        scoreList.Reverse();
        // Finds the number of scores that are being placed, which is 10 if the total number is greater than 10
        // And the size of the scorelist if it is less than 10
        if (scoreList.Count <= 10)
        {
            noScores = scoreList.Count;
        }
        else noScores = 10;
        // Loops through, placing the score objects starting at the top and moving down
        // Sets their parent as the canvas, as the canvas controls several properties of the text object
        for (int i = 0; i < noScores; i++)
        {
            GameObject scoreInstance = Instantiate(scorePrefab, transform.position, Quaternion.identity) as GameObject;
            scoreInstance.transform.position += ((200 - 40 * i) * Vector3.up);
            scoreInstance.GetComponent<Text>().text = scoreList[i].ToString();
            scoreInstance.transform.SetParent(canvas.transform);
        }
    }

    public void Back()
    {
        // Unloads this scene and loads the main menu
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
