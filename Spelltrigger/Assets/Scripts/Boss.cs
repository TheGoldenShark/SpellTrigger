using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    public override void Die()
    {
        Destroy(gameObject);
        base.gameManager.nextLevel();
        data.ScoreUpdate(baseScore * gameManager.difficulty * gameManager.difficulty);
    }
}
