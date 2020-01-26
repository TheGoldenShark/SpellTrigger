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
        cameraFollowPosition = playerTranform.position;
        cameraFollowPosition.z = transform.position.z;
        transform.position = cameraFollowPosition;
    }
}
