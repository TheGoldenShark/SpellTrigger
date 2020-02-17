using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreMenu : MonoBehaviour
{
    private PersistentData data;
    private List<float> scoreList;
    private GameObject canvas;
    public GameObject scorePrefab;

    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("Data").GetComponent<PersistentData>();
        canvas = GameObject.Find("Canvas");
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(5));
        PlaceScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaceScores()
    {
        scoreList = data.getScoreList();
        scoreList.Sort();
        scoreList.Reverse();
        int noScores = 10;
        if (scoreList.Count <= 10){
            noScores = scoreList.Count;
        }
        for (int i = 0; i < noScores; i++)
        {
            GameObject scoreInstance = Instantiate(scorePrefab, transform.position, Quaternion.identity) as GameObject;
            scoreInstance.transform.position += ((200 - 40 * i) * Vector3.up);
            scoreInstance.GetComponent<Text>().text = scoreList[i].ToString();
            scoreInstance.transform.SetParent(canvas.transform);
        }
    }
}
