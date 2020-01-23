﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float time = 30;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        rb.velocity = direction.normalized * speed;
    }
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            enemy.takeDamage(5);
            Destroy(gameObject);
        }
        //else if (other.tag != "Player")
        //{
        //    Destroy(gameObject);
        //}
    }
}
