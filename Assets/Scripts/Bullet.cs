using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables

    [SerializeField] private float bulletSpeed;

    #endregion


    #region Unity Lifecycle

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyBullet();
    }

    #endregion


    #region Private Methods

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    #endregion
}
