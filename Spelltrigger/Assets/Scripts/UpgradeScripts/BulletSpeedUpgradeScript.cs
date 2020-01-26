﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeedUpgradeScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentInChildren<Gun>().fireSpeed = other.GetComponentInChildren<Gun>().fireSpeed/2;
            Destroy(gameObject);
            gameManager.Announcement("Fire Rate Increased");
        }
    }
}
