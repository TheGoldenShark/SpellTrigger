using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistentData : MonoBehaviour
{
    private float currentScore;
    private List<float> scoreList;
    // Start is called before the first frame update
    void Start()
    {
        scoreList = SaveManager.LoadSaveData().scoreList;
    }

    public void ScoreUpdate(float score)
    {
        this.currentScore += score;
    }

    public void StoreScore()
    {
        scoreList.Add(currentScore);
        currentScore = 0f;
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public List<float> getScoreList()
    {
        return scoreList;
    }
}
