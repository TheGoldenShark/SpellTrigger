using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public CameraFollow cameraFollow;
    public Transform playerTranform;
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
}
