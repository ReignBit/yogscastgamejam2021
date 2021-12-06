using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
    BaseEnemy.cs

    Base class for all enemies.
    Contains default Move() to move to a cell position.

    Override DoTurn in child class to add AI implementation.
*/

public abstract class BaseEnemy : MonoBehaviour
{

    new string name = "Unnamed Enemy";
	Vector3[] positions = {new Vector3(0.5f, 0.25f, 0), new Vector3(-0.5f, -0.25f, 0), new Vector3(0.5f, -0.25f, 0), new Vector3(-0.5f, -0.25f, 0)};
    public abstract void DoTurn();

    public void Move(int index)
    {

        Vector3Int cellPos = TilemapManager.instance.Ground.WorldToCell(positions[index] + transform.position);

        if (TilemapManager.instance.Ground.HasTile(cellPos))
		{
			TilemapManager.instance.MoveTile(transform.position, cellPos, TilemapManager.instance.Entities);
            transform.position = cellPos;
		}
    }
}
