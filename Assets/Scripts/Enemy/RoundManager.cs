using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    RoundManager.cs

    Responsible for managing the turns, and keeping references to all enemy objects in play.
    PlayerManager -> RoundManager -> EnemyObjects

*/



public class RoundManager : MonoBehaviour
{

    public static RoundManager instance;

    [SerializeField] List<BaseEnemy> enemies = new List<BaseEnemy>();

    void Awake()
    {
        // Singleton
        if (instance != null)
            Debug.LogError("More than one RoundManager in the scene! Undefined behaviour will occur!");
        else
            instance = this;
    }


    /// <summary>
    /// Ends the player's turn and performs turns for all enemies on board.
    /// </summary>
    public void EndPlayerTurn()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].DoTurn();
        }
    }


    /// <summary>
    /// Add a new enemy to the enemies list
    /// </summary>
    /// <param name="enemy">Enemy to add.</param>
    public void AddEnemy(BaseEnemy enemy)
    {
        enemies.Add(enemy);
    }


    /// <summary>
    /// Remove an enemy from the enemies list. Does nothing if enemny not in list.
    /// </summary>
    /// <param name="enemy">Enemy instance to remove.</param>
    public void RemoveEnemy(BaseEnemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }
}
