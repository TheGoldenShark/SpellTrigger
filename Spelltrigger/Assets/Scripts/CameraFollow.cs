﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Normal camera size: 2
    public void cameraUpdate(Vector3 cameraFollowPosition)
    {
        cameraFollowPosition.z = transform.position.z;
        transform.position = cameraFollowPosition;
    }
}