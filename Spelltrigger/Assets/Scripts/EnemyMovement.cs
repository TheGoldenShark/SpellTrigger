using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform playerTransform;
    public Rigidbody2D rb;
    public float speed;
    private bool seen;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy is onscreen, set seen to true
        if (GetComponent<Renderer>().isVisible)
        {
            seen = true;
        }
        // If the enemy has been seen, calculate the vector from the enemy's position to the player's position
        // and move the enemy in that direction by normalised vector multiplied by speed
        if (seen)
        {
            Vector2 direction = playerTransform.position - transform.position;
            rb.velocity = direction.normalized * speed;
        }
    }
}
