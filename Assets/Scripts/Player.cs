using System.Collections;
using UnityEngine;

public class Player : BaseUnit
{
    #region Variables

    [Header("Base settings")]

    [SerializeField] private float resetSceneTime;
    
    [SerializeField] private PlayerMovement playerMovement;
   
    #endregion
    
    #region Unity Lifecycle

    private void Update()
    {
        if (IsDead == false)
        {
            Shoot();
        }
    }

    #endregion


    #region Private Methods

    protected override void Die()
    {
        base.Die();
        playerMovement.enabled = false;
        StartCoroutine(nameof(ExecuteAfterTime));
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
