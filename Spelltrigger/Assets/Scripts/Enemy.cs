using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int baseHealth;
    public int baseDamage;
    public int baseScore;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public PersistentData data;
    private int health;
    float spriteColorTimer = 0;
    float transitionTime = 0.5f;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        data = GameObject.Find("Data").GetComponent<PersistentData>();
        sprite = GetComponent<SpriteRenderer>();
        // Calculates the health of the enemy, so it increases with difficulty
        health = (int)(baseHealth * gameManager.difficulty);
    }

    // Update is called once per frame
    void Update()
    {
        // Transitions the sprite from red to it's regular color, by adding to the green and blue component every frame
        if (spriteColorTimer >= 0)
        {
            spriteColorTimer -= Time.deltaTime;
            Color tempColor = sprite.color;
            tempColor.g += Time.deltaTime * 1/transitionTime;
            tempColor.b += Time.deltaTime * 1/transitionTime;
            sprite.color = tempColor;
        }
    }

    public void takeDamage (int damage)
    {
        // Decrements the enemy's health, sets the sprite color to red and starts the color transition timer
        health -= damage;
        sprite.color = Color.red;
        spriteColorTimer = transitionTime;
        // If the health goes below zero, kill the enemy
        if (health <= 0)
        {
            Die();
        }
    }

    // Virtual function so it can be overriden in child classes
    public virtual void Die()
    {
        // Updates the score and destroys the enemy object
        data.ScoreUpdate(baseScore * gameManager.difficulty * gameManager.difficulty);
        Destroy(gameObject);
    }
}
