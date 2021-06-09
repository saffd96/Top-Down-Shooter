using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
