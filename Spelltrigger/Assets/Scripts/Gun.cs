using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int damage = 5;
    public float fireSpeed = 5;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            timer = fireSpeed;
            placeBullet(damage);
        }
        else
        {
            timer -= 1;
        }
    }

    void placeBullet(int damage)
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as Bullet;
        bulletInstance.damage = damage;

    }
}

