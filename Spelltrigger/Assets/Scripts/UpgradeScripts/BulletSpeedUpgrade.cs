using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpeedUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
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
            // Gets the gun component from the weapon object that is a child of the player, and halves the fire speed
            other.GetComponentInChildren<Gun>().fireSpeed = other.GetComponentInChildren<Gun>().fireSpeed/2;
            // Announces to the player that fire rate increases, and destroys the item
            gameManager.Announcement("Fire Rate Increased");
            Destroy(gameObject);
        }
    }
}
