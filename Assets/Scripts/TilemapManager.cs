using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
    TilemapManager.cs

    Holds the Ground and Collision map.
    Access each with:
        TilemapManager.instance.Ground => Tilemap
        TilemapManager.instance.Collision => Tilemap

*/


public class TilemapManager : MonoBehaviour
{
    public static TilemapManager instance;

    [SerializeField] private Tilemap groundMap;
    [SerializeField] private Tilemap collisionsMap;
    [SerializeField] private Tilemap entitiesMap;
    [SerializeField] private Tile enemyTile;
    [SerializeField] private Tile playerTile;

    public Tilemap Ground
    {
        get { return groundMap; }
    }

    public Tilemap Collision
    {
        get { return collisionsMap; }
    }

    public Tilemap Entities
    {
        get { return entitiesMap; }
    }

	public Tile PlayerTile
	{
		get { return playerTile; }
	}

	public Tile EnemyTile
	{
		get { return enemyTile; }
	}

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one TilemapManager!");
        }
        else
        {
            instance = this;
        }
    }

	public void MoveTile(GameObject entity, Vector3Int newPos, Tilemap map)
	{
		Vector3Int oldPos = map.WorldToCell(entity.transform.position);
		map.SetTile(newPos, map.GetTile(oldPos));
		map.SetTile(oldPos, null);
	}

	public void MoveTile(Vector3 oldPosition, Vector3 newPosition, Tilemap map)
	{
		Vector3Int newPos = map.WorldToCell(newPosition);
		Vector3Int oldPos = map.WorldToCell(oldPosition);
		map.SetTile(newPos, map.GetTile(oldPos));
		map.SetTile(oldPos, null);
	}

	public void MoveTile(Vector3Int oldPos, Vector3Int newPos, Tilemap map)
	{
		map.SetTile(newPos, map.GetTile(oldPos));
		map.SetTile(oldPos, null);
	}
}
