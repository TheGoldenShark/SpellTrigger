﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(temp);
    }
}
