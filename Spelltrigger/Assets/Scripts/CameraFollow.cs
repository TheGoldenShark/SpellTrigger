using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Normal camera size: 2
    public Transform playerTranform;
    private Vector3 cameraFollowPosition;
    public void Update()
    {
        // Copy the player's position to cameraFollowPosition
        cameraFollowPosition = playerTranform.position;
        // Set the z value to the current transform, as the z value of the camera should not change
        cameraFollowPosition.z = transform.position.z;
        // Set the current position as cameraFollowPosition
        transform.position = cameraFollowPosition;
    }
}
