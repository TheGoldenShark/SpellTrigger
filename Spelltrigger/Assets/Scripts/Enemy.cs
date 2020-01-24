using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    float spriteColorTimer = 0;
    float transitionSpeed = 5;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteColorTimer >= 0)
        {
            spriteColorTimer -= Time.deltaTime;
            Color tempColor = sprite.color;
            tempColor.g += Time.deltaTime * transitionSpeed;
            tempColor.b += Time.deltaTime * transitionSpeed;
            sprite.color = tempColor;
        }
        else
        {
            sprite.color = Color.white;
        }
    }

    public void takeDamage (int damage)
    {
        health -= damage;
        sprite.color = Color.red;
        spriteColorTimer = 5f;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
