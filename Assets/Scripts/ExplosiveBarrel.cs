using System.Collections;
using UnityEngine;

public class ExplosiveBarrel : DamageableObject
{

    #region Variables

    [SerializeField] private float damage;
    [SerializeField] private float damageArea;
    [SerializeField] protected Animator animator;
    [SerializeField] private string explodeTriggerName;

    #endregion


    #region Unity lifecycle

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageInitiator = collision.GetComponent<HpChanger>();

        if (damageInitiator != null)
        {
            Explode();
            animator.SetTrigger(explodeTriggerName);

            StartCoroutine(OnAnimationEndDestroy());
        }
        
    }

    #endregion


    #region Private methods

    private IEnumerator OnAnimationEndDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageArea);
    }

    private void Explode()
    {
        var unitsInRadius = Physics2D.OverlapCircleAll(transform.position, damageArea);

        foreach(Collider2D baseUnit in unitsInRadius)
        {
            var unit = baseUnit.GetComponent<DamageableObject>();

            if (unit is ExplosiveBarrel explosiveBarrel)
            {
                explosiveBarrel.Explode();
            }
            
            if (unit!=null)
            {
                unit.ChangeHealth(damage);
            }
        }
    }

    #endregion

}
