using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.animator.SetTrigger("RetreatState");
            Debug.Log("Start retreat.");
        }
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.player != null)
        {
            enemy.navMeshAgent.destination = enemy.transform.position - enemy.player.transform.position;
        }
        Debug.Log("Retreating.");
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop retreat.");
    }
}
