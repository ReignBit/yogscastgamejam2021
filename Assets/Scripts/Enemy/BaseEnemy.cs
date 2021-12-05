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

public class BaseEnemy : MonoBehaviour
{
    new string name = "Unnamed Enemy";
    public virtual void DoTurn()
    {

    }

    public void Move(Vector3 destination)
    {
        Vector3Int cellPos = TilemapManager.instance.Ground.WorldToCell(destination);

        if (TilemapManager.instance.Ground.HasTile(cellPos))
            transform.position = cellPos;
    }
}
