using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // Start is called before the first frame update
    public List<float> scoreList;

    public SaveData()
    {
        scoreList = new List<float>();
    }

    public SaveData(PersistentData data)
    {
        scoreList = data.GetScoreList();
    }
}
