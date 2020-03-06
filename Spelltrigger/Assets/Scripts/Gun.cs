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
        // If the mousebutton is being held and a bullet hasn't been fired in a certain amount of time
        // Place a bullet with the gun's damage and reset the timer
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            timer = fireSpeed;
            placeBullet(damage);
        }
        else
        // Decrements the timer
        {
            timer -= Time.deltaTime;
        }
    }

    void placeBullet(int damage)
    {
        // Instatiates a bullet at the position of the player (same as the position of the gun)
        // and sets the damage of the bullet
        Bullet bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as Bullet;
        bulletInstance.damage = damage;

    }
}

