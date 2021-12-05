using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseEnemy
{
    string name = "Enemy Test";


    public override void DoTurn()
    {
        Debug.Log("DoTurn on enemy");
        Move(new Vector3(Random.Range(0, 1), Random.Range(0, 1), 0));
    }
    
}
