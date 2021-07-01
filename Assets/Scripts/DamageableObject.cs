using System;
using NaughtyAttributes;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    #region Variables

    [Header("Base settings")]
    [SerializeField] protected Animator Animator;

    [SerializeField] private float hp;


    [Header("DEV")]
    [SerializeField] [ReadOnly]
    public float CurrentHp;

    protected internal bool IsDead;
    public float maxHp;

    #endregion

    
    #region Events

    public event Action<float, float> OnChanged;
//    public event Action OnDied;

    #endregion


    #region Unity Lifecycle

    protected void Start()
    {
        Reset();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hpChanger = other.GetComponent<HpChanger>();

        if (hpChanger == null) return;

        if (other.CompareTag(Tags.FirstAidKit))
        {
            ChangeHealth(-hpChanger.HpAmount);
        }
        else
        {
            ChangeHealth(hpChanger.HpAmount);
        }

        Destroy(other.gameObject);
    }

    #endregion


    #region Private Methods

    protected virtual void Die()
    {
        IsDead = true;
    }

    #endregion

    #region Public Methods

    public void Reset()
    {
        maxHp = hp;
        CurrentHp = hp;
        IsDead = false;    }

    public void ChangeHealth(float hpAmount)
    {
        if (IsDead) return;

        CurrentHp -= hpAmount;
        OnChanged?.Invoke(CurrentHp,maxHp);

        if (CurrentHp > hp)
        {
            CurrentHp = maxHp;
        }
        
        if (CurrentHp <= 0)
        {
            Die();
        //    OnDied?.Invoke();
        }

    }

    #endregion
}
