using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int baseHealth;
    public int baseDamage;
    [HideInInspector]
    public GameManager gameManager;
    private int health;
    float spriteColorTimer = 0;
    float transitionSpeed = 5;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sprite = base.GetComponent<SpriteRenderer>();
        health = (int)(baseHealth * gameManager.difficulty);
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
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
