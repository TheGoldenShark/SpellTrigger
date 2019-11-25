using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
