using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
    string name = "Enemy Test";


	private void Start()
	{
		TilemapManager.instance.Entities.SetTile(TilemapManager.instance.Entities.WorldToCell(transform.position), TilemapManager.instance.EnemyTile);
	}

    public override void DoTurn()
    {
        Debug.Log("Do Turn on enemy");
        // Move(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.25f, 0.25f), 0));
    }

}
