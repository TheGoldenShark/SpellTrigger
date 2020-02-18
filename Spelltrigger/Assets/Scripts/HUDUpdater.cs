using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour
{
    private PersistentData data;
    private Player player;
    public Text score;
    public Text health;
    // Start is called before the first frame update
    void Start()
    {
        data = GameObject.Find("Data").GetComponent<PersistentData>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = player.health.ToString();
        score.text = data.GetCurrentScore().ToString();
    }
}
