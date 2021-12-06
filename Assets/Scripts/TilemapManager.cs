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
    [SerializeField] private Tilemap highlightMap;

    public Tilemap Ground
    {
        get { return groundMap; }
    }

    public Tilemap Collision
    {
        get { return collisionsMap; }
    }

    public Tilemap Highlights
    {
        get { return highlightMap; }
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
}
