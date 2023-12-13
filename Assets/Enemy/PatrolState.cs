using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool isMoving;
    private Vector3 destination;

    public void EnterState(Enemy enemy)
    {
        if (enemy != null)
        {
            isMoving = false;
            enemy.animator.SetTrigger("PatrolState");
            Debug.Log("Start patrol.");
        }
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.chaseDistance)
        {
            enemy.SwitchState(enemy.chaseState);
        }
        
        if (!isMoving)
        {
            isMoving = true;
            // random index waypoint
            int index = UnityEngine.Random.Range(0, enemy.waypoints.Count);
            destination = enemy.waypoints[index].position;
            enemy.navMeshAgent.destination = destination;
            Debug.Log("Patrolling.");
        }
        else
        {
            if (Vector3.Distance(destination, enemy.transform.position) <= 0.1)
            {
                isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop patrol.");
    }
}
