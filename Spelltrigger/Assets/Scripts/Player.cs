using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public GameManager gameManager;
    private float invulerabilityTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invulerabilityTime > 0)
        {
            invulerabilityTime -= Time.deltaTime;
        }

    }

    public void takeDamage(int damage)
    {
        if (invulerabilityTime <= 0)
        {
            health -= damage;
            if (health <= 0)
            {
                gameManager.gameOver();
            }
            invulerabilityTime = 0.5f;
        }
    }
}
