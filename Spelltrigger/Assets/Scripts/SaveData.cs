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
        // Constructor that makes a new scorelist when given no argument
        scoreList = new List<float>();
    }

    public SaveData(PersistentData data)
    {
        // Constructor that takes the scorelist from the persistentData class instance in the parameter
        scoreList = data.GetScoreList();
    }
}
