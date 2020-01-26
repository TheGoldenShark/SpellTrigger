using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public GameManager gameManager;
    private float invulerabilityTime;

    // Used for movement
    // Animation 0=Idle 1=N 2=NE 3=E 4=SE 5=S
    Vector2 playerInput;
    Rigidbody2D rb;
    public Animator animator;
    bool flipped;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        flipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        // Decrements the remaining invulnerability time
        if (invulerabilityTime > 0)
        {
            invulerabilityTime -= Time.deltaTime;
        }
    }

    public void takeDamage(int damage)
    {
        // Damage only taken if not invulnerable
        if (invulerabilityTime <= 0)
        {
            // health decremented
            health -= damage;
            if (health <= 0)
            {
                // game over if the player runs out of health
                gameManager.gameOver();
            }
            // Once damage has been taken, invulnerability is assigned as half a second
            invulerabilityTime = 0.5f;
        }
    }

    private void playerMove()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = playerInput.normalized * speed;

        //Animation

        //North East
        if (playerInput.x > 0 && playerInput.y > 0)
        {
            flip(false);
            animator.SetInteger("Direction", 2);
        }
        //South East
        else if (playerInput.x > 0 && playerInput.y < 0)
        {
            flip(false);
            animator.SetInteger("Direction", 4);
        }
        //North West
        else if (playerInput.x < 0 && playerInput.y > 0)
        {
            flip(true);
            animator.SetInteger("Direction", 2);
        }
        //South West
        else if (playerInput.x < 0 && playerInput.y < 0)
        {
            flip(true);
            animator.SetInteger("Direction", 4);
        }
        //East
        else if (playerInput.x > 0)
        {
            flip(false);
            animator.SetInteger("Direction", 3);
        }
        //West
        else if (playerInput.x < 0)
        {
            flip(true);
            animator.SetInteger("Direction", 3);

        }
        //North
        else if (playerInput.y > 0)
        {
            flip(false);
            animator.SetInteger("Direction", 1);
        }
        //South
        else if (playerInput.y < 0)
        {
            flip(false);
            animator.SetInteger("Direction", 5);
        }
        else
        {
            flip(false);
            animator.SetInteger("Direction", 0);
        }

    }
    private void flip(bool flipState)
        // This function mirrors the player depending on which direction they are facing. This is so sprites only had to be used for one 
    {
        if (flipped != flipState)
        {
            flipped = !flipped;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
