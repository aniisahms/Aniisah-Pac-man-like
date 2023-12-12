using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] public float chaseDistance;
    [SerializeField] public Player player;

    private BaseState currentState;

    [HideInInspector] public PatrolState patrolState = new PatrolState();
    [HideInInspector] public ChaseState chaseState = new ChaseState();
    [HideInInspector] public RetreatState retreatState = new RetreatState();

    [HideInInspector] public NavMeshAgent navMeshAgent;

    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    private void Awake()
    {
        // initial state
        currentState = patrolState;
        currentState.EnterState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() 
    {
        if (player != null)
        {
            player.OnPowerUpStart += StartRetreating;
            player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    private void StartRetreating()
    {
        SwitchState(retreatState);
    }
    private void StopRetreating()
    {
        SwitchState(patrolState);
    }
}
