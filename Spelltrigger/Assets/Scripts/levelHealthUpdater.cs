﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelHealthUpdater : MonoBehaviour
{
    public Player player;
    public Text health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
        health = ToString(player.health).text;
    }
}
