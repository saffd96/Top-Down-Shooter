using UnityEngine;
using UnityEngine.UI;

public class HpPanel : MonoBehaviour
{
    #region Variables

    [SerializeField] private BaseUnit baseUnit;
    [SerializeField] private Image image;

    #endregion


    #region Unity lifecycle

    private void OnEnable()
    {
        baseUnit.OnChanged += BaseUnit_OnChanged;
    }
    private void OnDisable()
    {
        baseUnit.OnChanged -= BaseUnit_OnChanged;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.AngleAxis(0, Vector2.up);
    }

    #endregion


    #region Private methods

    private void UpdateHpPanel(float currentHp, float maxHp)
    {
        if (currentHp >= 0)
        {
            var fillAmount = currentHp / maxHp;
            image.fillAmount = fillAmount;
        }
        if (currentHp <= 0)
        {
            gameObject.GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    #endregion


    #region Event handlers

    private void BaseUnit_OnChanged(float currentHp, float maxHp)
    {
        UpdateHpPanel(currentHp, maxHp);
    }
}

#endregion
