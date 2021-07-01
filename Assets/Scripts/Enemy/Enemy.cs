using System.Collections;
using UnityEngine;

public class Enemy : BaseRangeUnit
{
    #region Variables

    [Header("Enemy settings")]
    [SerializeField] private float shootPeriod = 0.1f;
    [SerializeField] private float startShootingDelay = 1f;

    private Player player;
    private Vector3 playerPosition;

    private Coroutine shootRoutine;

    #endregion


    #region Unity Lifecycle

    protected void Awake()
    {
        shootRoutine = StartCoroutine(StartShooting());
    }

    protected new void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        FindPlayerPosition();

        if (!IsDead && !player.IsDead)
        {
            Rotate();
        }
        else
        {
            if (shootRoutine != null)
            {
                StopCoroutine(shootRoutine);
            }
        }
    }

    #endregion


    #region Coroutines

    private IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(startShootingDelay);

        while (IsDead == false)
        {
            Attack();

            yield return new WaitForSeconds(shootPeriod);
        }
    }

    #endregion


    #region Private Methods

    protected override void Attack()
    {
        PlayAttackAnimation();
        CreateBullet();
    }

    private void FindPlayerPosition()
    {
        playerPosition = player.transform.position;
    }

    private void Rotate()
    {
        Vector3 directionInvert = -(playerPosition - transform.position);

        transform.up = directionInvert;
    }

    #endregion
}
