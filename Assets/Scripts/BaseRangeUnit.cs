using UnityEngine;

public class BaseRangeUnit : BaseUnit
{
    #region Variables

    [Header("RangeUnitSettings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    #endregion


    #region Private Methods

    protected void CreateBullet()
    {
        Instantiate(projectilePrefab, projectileSpawnPoint.position, transform.rotation);
    }

    protected override void Attack()
    {
        if (CurrentShootDelay <= 0)
        {
            CurrentShootDelay = AttackDelay;
            PlayAttackAnimation();
            CreateBullet();
        }

        CurrentShootDelay -= Time.deltaTime;
    }

    #endregion
}
