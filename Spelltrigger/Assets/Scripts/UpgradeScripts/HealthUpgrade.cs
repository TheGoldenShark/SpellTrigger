using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Runs if the object collided with is the player
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().health += 10* (int) gameManager.difficulty;
            // Announces to the player that health increases, and destroys the item
            Destroy(gameObject);
            gameManager.Announcement("Health Upgraded");
        }
    }

}
