using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public override void Die()
    {
        Destroy(gameObject);
        gameManager.nextLevel();
        data.ScoreUpdate(baseScore * gameManager.difficulty * gameManager.difficulty);
    }
}
