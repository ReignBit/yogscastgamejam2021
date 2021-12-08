using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using TMPro;

/*
    RoundManager.cs

    Responsible for managing the turns, and keeping references to all enemy objects in play.
    PlayerManager -> RoundManager -> EnemyObjects

*/



public class RoundManager : MonoBehaviour
{

    public static RoundManager instance;

    [SerializeField] List<BaseEnemy> enemies = new List<BaseEnemy>();
    [SerializeField] List<Present> presents = new List<Present>();
	[SerializeField] TextMeshProUGUI presentsCollected;

    [SerializeField] GameObject playerGameObject;

    public GameObject deathParticleSystemPrefab;

    // Events
    public UnityAction onPlayerDeath;


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
        foreach (BaseEnemy enemy in enemies)
		{
            enemy.DoTurn();
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

	public bool HitEnemy(Vector3 position)
	{

		return HitEnemy(FindEnemy(position));
	}

	public bool HitEnemy(BaseEnemy enemy)
	{
		RemoveEnemy(enemy);
		return true;
	}

	public void CollectPresent(Vector3 position)
	{
		presentsCollected.text = " x 1 / 1";
		CollectPresent(FindPresent(position));
	}

	public void CollectPresent(Present present)
	{

		RemovePresent(present);
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
			GameObject.Destroy(enemy.gameObject);
        }

		Debug.LogWarning("Can't remove enemy. Enemy not in list.");
    }

	public void RemoveEnemy(Vector3 position)
	{
		BaseEnemy enemy = FindEnemy(position);
		RemoveEnemy(enemy);
	}

	public void RemovePresent(Present present)
	{
		if (presents.Contains(present))
		{
			presents.Remove(present);
			GameObject.Destroy(present);
		}

		Debug.LogWarning("Can't remove present. Present not in list.");
	}

	public void RemovePresent(Vector3 position)
	{

		Present present = FindPresent(position);
		RemovePresent(present);
	}

	public BaseEnemy FindEnemy(Vector3 position)
	{
		foreach (BaseEnemy enemy in enemies)
		{
			if (enemy.transform.position == position)
			{
				return enemy;
			}
		}

		return null;
	}

	public Present FindPresent(Vector3 position)
	{
		foreach (Present present in presents)
		{
			if (present.transform.position == position)
			{
				return present;
			}
		}

		return null;
	}

    /// <summary>
    /// Call to kill the player
    /// </summary>
    public void PlayerDeath()
    {
        Debug.Log("Player has been killed!");
        if (onPlayerDeath != null)
        {
            onPlayerDeath.Invoke();
        }
    }

}
