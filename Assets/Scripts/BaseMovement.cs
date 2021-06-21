using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    #region Variables

    [SerializeField] protected float Speed;
    [SerializeField] protected Animator Animator;

    [SerializeField] protected string MoveSpeedName = "MoveSpeed";

    protected Vector3 Direction;
    protected Transform CachedTransform;
    protected Rigidbody2D Rb;

    private int moveSpeedId;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        CachedTransform = transform;
        moveSpeedId = Animator.StringToHash(MoveSpeedName);
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Rotate();
        Move();
    }

    #endregion


    #region Private Methods

    protected virtual void Move()
    {
        Rb.velocity = Direction * Speed;

        Animator.SetFloat(moveSpeedId, Direction.magnitude);
    }

    protected virtual void Rotate()
    {
        CachedTransform.up = (Vector2) (-Direction);
    }

    #endregion
}
