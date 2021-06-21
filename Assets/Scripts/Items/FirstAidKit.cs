using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    #region Unity Lifecycle

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyFirstAidKit();
    }

    #endregion


    #region Private Methods

    private void DestroyFirstAidKit()
    {
        Destroy(gameObject);
    }

    #endregion
}
