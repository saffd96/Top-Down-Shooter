using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    #region Variables

    [Header("Base settings")]
    [SerializeField] protected Animator Animator;
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float hp;

    [Header("Attack")]
    [SerializeField] protected float Damage = 1;
    [SerializeField] protected float AttackDelay;

    [Header("Animation settings")]
    [SerializeField]
    protected string ShootTriggerName = "Shoot";
    [SerializeField] private string dieTriggerName = "IsDead";

    [Header("DEV")]
    [SerializeField] private float currentHp;

    private float maxHp;
    private int shootId;
    private int dieId;
    protected float CurrentShootDelay;
    protected internal bool IsDead;

    #endregion


    #region Unity Lifecycle

    protected virtual void Start()
    {
        maxHp = hp;
        currentHp = hp;
        shootId = Animator.StringToHash(ShootTriggerName);
        dieId = Animator.StringToHash(dieTriggerName);

        IsDead = false;
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

    protected virtual void Die()
    {
        IsDead = true;
        Animator.SetTrigger(dieId);
        circleCollider2D.enabled = false;
        rb.Sleep();
    }

    #endregion


    #region Public Methods

    public void ChangeHealth(float hpAmount)
    {
        if (IsDead) return;

        currentHp -= hpAmount;

        if (currentHp > hp)
        {
            currentHp = maxHp;
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    #endregion
}
