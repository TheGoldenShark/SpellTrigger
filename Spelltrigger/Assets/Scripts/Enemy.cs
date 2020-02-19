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
        health = (int)(baseHealth * gameManager.difficulty);
    }

    // Update is called once per frame
    void Update()
    {
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
        health -= damage;
        sprite.color = Color.red;
        spriteColorTimer = transitionTime;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        data.ScoreUpdate(baseScore * gameManager.difficulty * gameManager.difficulty);
        Destroy(gameObject);
    }
}
