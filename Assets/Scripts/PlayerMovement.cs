using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private string moveSpeedName = "MoveSpeed";

    private int moveSpeedId;

    private Rigidbody2D rb;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        moveSpeedId = Animator.StringToHash(moveSpeedName);

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    #endregion


    #region Private Methods

    private void Move()
    {
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        
        rb.velocity = direction * speed;

        animator.SetFloat(moveSpeedId, direction.magnitude);
    }

    private void Rotate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionInvert = -(mousePosition - transform.position);

        transform.up = directionInvert;
    }

    #endregion
}
