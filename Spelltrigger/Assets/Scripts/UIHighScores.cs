using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreUpdater : MonoBehaviour
{
    public PersistentData data;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("Data").GetComponent<PersistentData>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        score.text = data.GetCurrentScore().ToString();
    }
}
