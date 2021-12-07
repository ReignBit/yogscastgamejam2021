using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
    string name = "Enemy Test";

	// (-0.5, 0.25) 	= W
	// (-0.5, -0.25) 	= A
	// (0.5, -0.25) 	= S
	// (0.5, 0.25) 		= D
	Vector3[] movePositions = {new Vector3(0.5f, 0.25f, 0), new Vector3(0.5f, -0.25f)};


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
