using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
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
    public void playerMove()
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
