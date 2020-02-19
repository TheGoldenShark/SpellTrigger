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
        damage = (int)(GetComponentInParent<Enemy>().baseDamage * gameManager.difficulty);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.takeDamage(damage);
        }
    }
}
