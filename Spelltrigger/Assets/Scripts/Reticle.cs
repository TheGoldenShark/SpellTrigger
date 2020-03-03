using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Makes the reticle follow the mouse position, while keeping z as 10f
        Vector3 temp = Input.mousePosition;
        temp.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(temp);
    }
}
