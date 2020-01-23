using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public int damage;
    public int fireSpeed = 5;
    int timer;
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && timer<=0)
        {
            timer = fireSpeed;
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        else
        {
            timer -= 1;
            Debug.Log(timer);
        }
    }
}
