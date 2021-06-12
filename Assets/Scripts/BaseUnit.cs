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

    [SerializeField] private float playerHp;
    [SerializeField] private float shootDelay;

    [Header("Animation settings")]
    [SerializeField] private string shootTriggerName = "Shoot";
    [SerializeField] private string dieTriggerName = "IsDead";

    [Header("DEV")]
    [SerializeField] private float currentPlayerHp;
    
    private float maxPlayerHp; 
    private int shootId;
    private int dieId;
    private float currentShootDelay;
    protected internal bool IsDead;

    #endregion


    private void Start()
    {
        maxPlayerHp = playerHp;
        currentPlayerHp = playerHp;
        shootId = Animator.StringToHash(shootTriggerName);
        dieId = Animator.StringToHash(dieTriggerName);

        IsDead = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var hpChanger = other.GetComponent<HpChanger>();

        if (hpChanger != null)
        {
            ApplyDamage(hpChanger.HpAmount);
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

    internal void ApplyDamage(float hpAmount)
    {
        if (IsDead != false) return;

        currentPlayerHp -= hpAmount;

        if (currentPlayerHp > playerHp)
        {
            currentPlayerHp = maxPlayerHp;
        }

        if (currentPlayerHp <= 0)
        {
            Die();
        }
    }

    #endregion
}
