using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTargetAIBehaviour : MonoBehaviour, AIBehaviourMixin
{

    [SerializeField] Transform target;

    void Update()
    {
        
    }

    public void DoTurn(BaseEnemy e)
    {
        Vector3 vectorToTarget = target.position - transform.position;
        vectorToTarget = vectorToTarget.normalized;

        Debug.DrawRay(transform.position, target.position - transform.position, Color.green, 1f, false);
        
        if (Vector3.Dot(e.transform.up, vectorToTarget) >= 0.51)
        {
            e.MoveCardinal(CardinalDirection.NORTH);
        }
        else if (Vector3.Dot(-e.transform.up, vectorToTarget) >= 0.51)
        {
            e.MoveCardinal(CardinalDirection.SOUTH);
        }
        else if (Vector3.Dot(e.transform.right, vectorToTarget) >= 0.51)
        {
            e.MoveCardinal(CardinalDirection.EAST);
        }
        else if (Vector3.Dot(-e.transform.right, vectorToTarget) >= 0.51)
        {
            e.MoveCardinal(CardinalDirection.WEST);
        }
        //e.Move(target.position);

    }
}
