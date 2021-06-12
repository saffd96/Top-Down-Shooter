using UnityEngine;

public class HpChanger : MonoBehaviour
{
    #region Variables

    [SerializeField] private float hpAmount;

    #endregion


    #region Properties

    public float HpAmount => hpAmount;

    #endregion
}
