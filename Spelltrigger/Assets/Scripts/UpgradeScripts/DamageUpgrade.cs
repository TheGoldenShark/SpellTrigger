using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : MonoBehaviour
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
            // Gets the gun component from the weapon object that is a child of the player, and increases damage by 5
            other.GetComponentInChildren<Gun>().damage += 5;
            // Announces to the player that damage increases, and destroys the item
            Destroy(gameObject);
            gameManager.Announcement("Damage Increased");
        }
    }
}
