using System;
using System.Collections;
using UnityEngine;

public class Enemy : BaseUnit
{
    #region Variables

    [Header("Enemy settings")]
    [SerializeField] private float shootPeriod = 0.1f;
    [SerializeField] private float startShootingDelay = 1f;

    private Player player;
    private Vector3 playerPosition;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(nameof(WaitForStartShooting));
    }

    private void Update()
    {
        FindPlayerPosition();

        if (IsDead== false && player.IsDead == false)
        {
            Rotate();
        }
        else
        {
            StopCoroutine(nameof(StartShooting));
        }
    }

    #endregion


    #region Coroutines

    private IEnumerator StartShooting()
    {
        while (IsDead == false)
        {
            Shoot();

            yield return new WaitForSeconds(shootPeriod);
        }
    }

    private IEnumerator WaitForStartShooting()
    {
        yield return new WaitForSeconds(startShootingDelay);

        StartCoroutine(nameof(StartShooting));
    }

    #endregion


    #region Private Methods

    protected override void Shoot()
    {
        PlayShootAnimation();
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
