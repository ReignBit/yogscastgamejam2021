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

public enum CardinalDirection
{
    NORTH,
    WEST,
    SOUTH,
    EAST,
}

public abstract class BaseEnemy : MonoBehaviour
{
    // (-0.5, 0.25) 	= W
    // (0.5, 0.25) 		= D
    // (0.5, -0.25) 	= S
    // (-0.5, -0.25) 	= A
    protected AIBehaviourMixin behaviour;
    public Vector3[] movePositions = {
            new Vector3(-0.5f, 0.25f, 0),
            new Vector3(0.5f, 0.25f, 0),
            new Vector3(0.5f, -0.25f, 0),
            new Vector3(-0.5f, -0.25f, 0),
        };
    public abstract void DoTurn();

    public void Start()
    {
        behaviour = GetComponent<AIBehaviourMixin>();
        TilemapManager.instance.Entities.SetTile(TilemapManager.instance.Entities.WorldToCell(transform.position), TilemapManager.instance.EnemyTile);
        RoundManager.instance.AddEnemy(this);
    }

    public Vector3 GetCardinalVector(Vector3 lhs, Vector3 rhs)
    {
        return new Vector3(0, 0, 0);
    }

    public void Move(Vector3 destination)
    {
        Vector3Int cellPos = TilemapManager.instance.Ground.WorldToCell(destination);
        Vector3Int oldPos = TilemapManager.instance.Ground.WorldToCell(transform.position);
        if (TilemapManager.instance.CanMove(cellPos))
        {

            TileBase entity = TilemapManager.instance.GetEntity(cellPos);

            Debug.Log(entity);

            if (entity != TilemapManager.instance.EnemyTile)
            {
                TilemapManager.instance.MoveTile(oldPos, cellPos, TilemapManager.instance.Entities);
                transform.position = destination;
            }
            
            
            if (entity == TilemapManager.instance.PlayerTile)
            {
                // Kill the player
                Debug.Log("PLAYER!");
                RoundManager.instance.PlayerDeath();
            }

        }
    }

    /// <summary>
    /// Move 1 unit in a cardinal direction
    /// </summary>
    /// <param name="direction">CardinalDirection</param>
    public void MoveCardinal(CardinalDirection direction)
    {
        Move(transform.position + movePositions[(int)direction]);
    }
}
