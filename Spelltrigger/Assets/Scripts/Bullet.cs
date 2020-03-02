using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage;
    public Rigidbody2D rb;
    public float time = 30;
    // Start is called before the first frame update
    void Start()
    {
        // Finds the vector pointing from the player to the mouse position
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        // Normalises the vector, multiplies it by speed and applies it to the bullet's rigidbody
        rb.velocity = direction.normalized * speed;
    }
    void Update()
    {
        // A timer that counts down and destroys the bulet object when the timer reaches zero
        time -= Time.deltaTime;
        if (time < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the bullet collides with an enemy, damage the enemy and destroy the bullet
        if (other.tag == "EnemyTrigger")
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            enemy.takeDamage(damage);
            Destroy(gameObject);
        }
        // If it collides with a levelobject (e.g. a wall), destroy the bullet
        else if (other.tag == "LevelObject")
        {
            Destroy(gameObject);
        }
    }
}
