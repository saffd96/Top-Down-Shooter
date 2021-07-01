using System;
using UnityEngine;

public class BaseUnit : DamageableObject
{
    #region Variables

    [Header("Attack")]
    [SerializeField] protected float Damage = 1;
    [SerializeField] protected float AttackDelay;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] protected CircleCollider2D CircleCollider2D;

    [Header("Animation settings")]
    [SerializeField] protected string ShootTriggerName = "Shoot";
    [SerializeField] private string dieTriggerName = "IsDead";

    private int dieId;
    private int shootId;
    protected float CurrentShootDelay;

    #endregion



    #region Unity Lifecycle

    protected new virtual void Start()
    {
        base.Start();
        dieId = Animator.StringToHash(dieTriggerName);
        shootId = Animator.StringToHash(ShootTriggerName);
    }

    #endregion


    #region Private Methods

    protected override void Die()
    {
        base.Die();
        CircleCollider2D.enabled = false;

        Animator.SetTrigger(dieId);
        rb.Sleep();
    }

    protected virtual void Attack()
    {
        if (CurrentShootDelay <= 0)
        {
            CurrentShootDelay = AttackDelay;
            PlayAttackAnimation();
        }

        CurrentShootDelay -= Time.deltaTime;
    }

    protected void PlayAttackAnimation()
    {
        Animator.SetTrigger(shootId);
    }

    #endregion
}
