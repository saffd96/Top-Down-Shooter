using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Zombie : BaseMeleeUnit
{
    private enum State
    {
        Idle,
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
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float dist = 0.1f;
    [SerializeField] private int pickUpCreationRate;
    [SerializeField] private GameObject pickUpPrefab;


    private Player player;
    private ZombieMovement zombieMovement;

    private Transform playerTransform;
    private Transform cachedTransform;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private State currentState;
    private int currentPatrolPointIndex;

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

    protected void Awake()
    {
        cachedTransform = transform;
        startPosition = cachedTransform.position;
        zombieMovement = GetComponent<ZombieMovement>();

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            isPatrol = true;
            currentPatrolPointIndex = 0;
        }
        else
        {
            isPatrol = false;
            currentPatrolPointIndex = -1;
        }

        SetState(isPatrol ? State.Patrol : State.Idle);
    }

    protected override void Start()
    {
        base.Start();

        player = FindObjectOfType<Player>();
        playerTransform = player.transform;
    }

    private void Update()
    {
        if (currentState == State.Dead || player.IsDead) return;

        CheckNewState();
        UpdateCurrentState();
    }

    #endregion


    #region Private Methods

    private void SetState(State newState)
    {
        if (currentState == newState)
        {
            return;
        }

        switch (newState)
        {
            case State.Idle:
                SetActiveMovement(false);

                break;
            case State.Dead:
                SetActiveMovement(false);

                break;
            case State.Attack:
                SetActiveMovement(false);

                break;
            case State.Chase:
                SetActiveMovement(true);
                startPosition = transform.position;

                break;
            case State.Return:
                SetActiveMovement(true);
                zombieMovement.SetTargetPosition(startPosition);

                break;
            case State.Patrol:
                SetActiveMovement(true);
                targetPosition = patrolPoints[currentPatrolPointIndex].position;
                zombieMovement.SetTargetPosition(targetPosition);

                break;
        }

        currentState = newState;
    }

    private void SetNewPatrolPoint()
    {
        currentPatrolPointIndex++;

        if (currentPatrolPointIndex >= patrolPoints.Length)
        {
            currentPatrolPointIndex = 0;
        }

        targetPosition = patrolPoints[currentPatrolPointIndex].position;
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
            SetState(State.Chase);
        }

        else if (distance > chaseRadius)
        {
            if (currentState == State.Chase)
            {
                SetState(State.Return);
            }
        }

        if (currentState == State.Return && Vector3.Distance(transform.position, startPosition) <= dist)
        {
            SetState(isPatrol ? State.Patrol : State.Idle);
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
                targetPosition = patrolPoints[currentPatrolPointIndex].position;
                zombieMovement.SetTargetPosition(targetPosition);

                break;
        }
    }

    private void ReturnToStartPosition()
    {
        if (Vector3.Distance(cachedTransform.position, startPosition) <= dist)
        {
            SetState(State.Idle);
        }
    }

    protected override void Attack()
    {
        base.Attack();
        player.ChangeHealth(Damage);
    }

    protected override void Die()
    {
        base.Die();
        SetState(State.Dead);
        
        if (NeedCreatePickUp())
        {
            Instantiate(pickUpPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Patrol()
    {
        if (Vector3.Distance(cachedTransform.position, targetPosition) <= dist)
        {
            SetNewPatrolPoint();
            zombieMovement.SetTargetPosition(targetPosition);
        }
    }

    private void Chase()
    {
        targetPosition = playerTransform.position;
        zombieMovement.SetTargetPosition(targetPosition);
    }

    private void SetActiveMovement(bool isActive)
    {
        zombieMovement.enabled = isActive;
    }
    
    private bool NeedCreatePickUp()
    {
        var randomNumber = Random.Range(1, 101);
        return pickUpCreationRate >= randomNumber;
    }

    #endregion
}
