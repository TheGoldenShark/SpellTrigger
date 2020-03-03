using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistentData : MonoBehaviour
{
    private float currentScore;
    private List<float> scoreList;
    private SaveData saveData;
    // Start is called before the first frame update
    void Start()
    {
        // Loads the savedata when started
        saveData = SaveManager.LoadSaveData();
        scoreList = saveData.scoreList;
    }

    public void ScoreUpdate(float score)
    {
        // increments the current score
        currentScore += score;
    }

    public void StoreScore()
    {
        // Stores the current score and resets it to zero
        scoreList.Add(currentScore);
        currentScore = 0f;
    }

    public float GetCurrentScore()
    {
        return currentScore;
    }

    public List<float> GetScoreList()
    {
        return scoreList;
    }
}
