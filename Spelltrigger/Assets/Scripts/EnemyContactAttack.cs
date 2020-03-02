using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContactAttack : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    int damage;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // Calculates damage based on baseDamage and difficulty
        damage = (int)(GetComponentInParent<Enemy>().baseDamage * gameManager.difficulty);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // When colliding with a player, run the takeDamage function, passing the enemy's damage as the parameter
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.takeDamage(damage);
        }
    }
}
