using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
    public override void DoTurn()
    {
        if (behaviour != null)
        {
            behaviour.DoTurn(this);
        }
        else
        {
            // Default move: Move randomly
            int random = Random.Range(0, movePositions.Length - 1);
            Move(transform.position + movePositions[random]);
        }
    }

}
