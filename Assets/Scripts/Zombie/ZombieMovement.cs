using UnityEngine;

public class ZombieMovement : BaseMovement
{
    #region Variables

    private Vector3 targetPosition;

    #endregion


    #region Private Methods

    protected override void Move()
    {
        base.Move();
        SetMoveAnimation(Direction.magnitude);
    }

    private void OnDisable()
    {
        Rb.Sleep();
        SetMoveAnimation(0);
    }

    private void SetMoveAnimation(float magnitude)
    {
        Animator.SetFloat(MoveSpeedName, magnitude);
    }

    protected override void Rotate()
    {
        Direction = (targetPosition - CachedTransform.position).normalized;

        base.Rotate();
    }

    #endregion


    #region Public Methods

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    #endregion
}
