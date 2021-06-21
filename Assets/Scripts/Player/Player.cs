using System.Collections;
using UnityEngine;

public class Player : BaseRangeUnit
{
    #region Variables

    [Header("PlayerSettings")]
    [SerializeField] private float resetSceneTime;

    [SerializeField] private PlayerMovement playerMovement;

    #endregion


    #region Unity Lifecycle

    private void Update()
    {
        if (IsDead)
        {
            return;
        }

        Attack();
    }

    #endregion


    #region Private Methods

    protected override void Attack()
    {
        if (Input.GetButton("Fire1"))
        {
            base.Attack();
        }
    }

    protected override void Die()
    {
        base.Die();
        playerMovement.enabled = false;
        StartCoroutine(ExecuteAfterTime());
    }

    #endregion


    #region Coroutines

    private IEnumerator ExecuteAfterTime()
    {
        yield return new WaitForSeconds(resetSceneTime);

        SceneManager.Instance.ResetLevel();
    }

    #endregion
}
