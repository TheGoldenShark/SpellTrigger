using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Runs instead of the Die() function in the parent class, advancing to the next level and increasing score
    public override void Die()
    {
        Destroy(gameObject);
        gameManager.nextLevel();
        data.ScoreUpdate(baseScore * gameManager.difficulty * gameManager.difficulty);
    }
}
