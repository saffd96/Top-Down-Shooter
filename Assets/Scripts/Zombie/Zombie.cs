using System;
using UnityEngine;

public class Zombie : BaseMeleeUnit
{
    private enum State
    {
        Idle,
        Moving,
        Attack,
        Chase,
        Dead,
        Return,
        Patrol
    }


    #region Variables

    [Header("Movement")]
    [SerializeField] private float chaseRadius = 15;
    [SerializeField] private float moveRadius = 10;
    [SerializeField] private bool isPatrol;
    [SerializeField] private Transform patrolPoint;
    
    private Player player;
    private ZombieMovement zombieMovement;

    private Transform playerTransform;
    private Transform cachedTransform;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 patrolPointPosition;

    private State currentState;
    
    #endregion


    #region Unity Lifecycle

    private void OnDrawGizmos()
    {
        var position = transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, chaseRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(position, moveRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, AttackRadius);
    }

    private void Awake()
    {
        cachedTransform = transform;
        targetPosition = patrolPointPosition;
        startPosition = cachedTransform.position;
        zombieMovement = GetComponent<ZombieMovement>();
        SetState(State.Idle);
    }

    protected override void Start()
    {
        base.Start();

        SetState(isPatrol ? State.Patrol : State.Idle);
        
        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
    }

    private void Update()
    {
        if (currentState == State.Dead && player.IsDead) return;

        CheckNewState();
        UpdateCurrentState();
    }

    #endregion


    #region Private Methods

    private void SetState(State newState)
    {
        switch (newState)
        {
            case State.Idle:
                SetActiveMovement(false);
                break;
            case State.Attack:
                SetActiveMovement(false);
                break;
            case State.Chase:
                SetActiveMovement(true);
                break;
            case State.Return:
                SetActiveMovement(true);
                zombieMovement.SetTargetPosition(startPosition);
                break;
            case State.Patrol:
                SetActiveMovement(true);
                zombieMovement.SetTargetPosition(patrolPoint.position);
                break;
        }

        currentState = newState;
    }

    private void CheckNewState()
    {
        var distance = Vector3.Distance(playerTransform.position, cachedTransform.position);

        if (distance < AttackRadius)
        {
            SetState(State.Attack);
        }
        
        else if (distance < moveRadius)
        {
            SetState(State.Moving);
        }

        else if (distance > chaseRadius)
        {
            if (currentState != State.Patrol) return;

            SetState(isPatrol ? State.Patrol : State.Return);
        }
    }

    private void UpdateCurrentState()
    {
        switch (currentState)
        {
            case State.Return:
                ReturnToStartPosition();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Patrol:
                Patrol();
                break;
        }
    }

    private void ReturnToStartPosition()
    {
        if (Vector3.Distance(cachedTransform.position, startPosition) <= 0)
        {
            SetState(State.Idle);
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(cachedTransform.position, targetPosition) <= 0)
        {
            targetPosition = startPosition; //тут запутался :(
            startPosition = patrolPointPosition;
            zombieMovement.SetTargetPosition(targetPosition);
        }    
    }

    private void Chase()
    {
        targetPosition = (playerTransform.position - targetPosition);
        zombieMovement.SetTargetPosition(targetPosition);
    }

    private void SetActiveMovement(bool isActive)
    {
        zombieMovement.enabled = isActive;
    }
    
    #endregion
}
