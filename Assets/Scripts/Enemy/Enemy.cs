using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{

	private void Start()
	{
		TilemapManager.instance.Entities.SetTile(TilemapManager.instance.Entities.WorldToCell(transform.position), TilemapManager.instance.EnemyTile);
	}

    public override void DoTurn()
    {
        Debug.Log("DoTurn on enemy");
        Move(Random.Range(0, 4));
    }

}
