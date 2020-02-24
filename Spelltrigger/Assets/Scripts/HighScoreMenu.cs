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
        scoreList = data.GetScoreList();
        scoreList.Sort();
        scoreList.Reverse();
        if (scoreList.Count <= 10)
        {
            noScores = scoreList.Count;
        }
        else noScores = 10;
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
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }
}
