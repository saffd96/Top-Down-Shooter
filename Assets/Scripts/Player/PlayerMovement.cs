using UnityEngine;

public class PlayerMovement : BaseMovement
{
    #region Private Methods

    protected override void Move()
    {
        Direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        base.Move();
    }

    protected override void Rotate()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Direction = Vector3.ClampMagnitude((mousePosition - bodyTransform.position),1f);
      //  Direction = (mousePosition - CachedTransform.position).normalized;
      
        base.Rotate();
    }

    #endregion
}
