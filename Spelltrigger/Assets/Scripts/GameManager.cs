﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CameraFollow cameraFollow;
    public Transform playerTranform;
    public GameObject announcementPrefab;
    public float difficulty;
    // Start is called before the first frame update
    private void Start()
    {
    }


    // Update is called once per frame
    private void Update()
    {
        cameraFollow.cameraUpdate(playerTranform.position);
    }
    private void FixedUpdate()
    {
        playerMovement.playerMove();
    }

    public void gameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void Announcement(string message)
    {
        GameObject announcementInstance = Instantiate(announcementPrefab, transform.position, Quaternion.identity) as GameObject;
        announcementInstance.GetComponentInChildren<Text>().text = message;
    }

    public void nextLevel()
    {

    }
}
