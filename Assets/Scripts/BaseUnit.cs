using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    #region Variables

    [Header("Base settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private new Rigidbody2D rigidbody2D; //rider сказал добавить new

    [SerializeField] private float hp;
    [SerializeField] private float shootDelay;

    [Header("Animation settings")]
    [SerializeField] private string shootTriggerName = "Shoot";
    [SerializeField] private string dieTriggerName = "IsDead";

    [Header("DEV")]
    [SerializeField] private float currentHp;
    
    private float maxHp; 
    private int shootId;
    private int dieId;
    private float currentShootDelay;
    protected internal bool IsDead;

    #endregion


    private void Start()
    {
        maxHp = hp;
        currentHp = hp;
        shootId = Animator.StringToHash(shootTriggerName);
        dieId = Animator.StringToHash(dieTriggerName);

        IsDead = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            ApplyDamage(damageDealer.Damage);
            Destroy(other.gameObject);
        }
    }


    #region Private Methods

    protected virtual void Shoot()
    {
        if (Input.GetButton("Fire1") && currentShootDelay <= 0)
        {
            currentShootDelay = shootDelay;
            PlayShootAnimation();
            CreateBullet();
        }

        currentShootDelay -= Time.deltaTime;
    }

    protected void CreateBullet()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }

    protected void PlayShootAnimation()
    {
        animator.SetTrigger(shootId);
    }

    protected virtual void Die()
    {
        IsDead = true;
        animator.SetTrigger(dieId);

        circleCollider2D.enabled = false;
        rigidbody2D.Sleep(); //это если во время бега подстрелят
    }

    internal void ApplyDamage(float damage)
    {
        if (IsDead != false) return;

        currentHp -= damage;

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
