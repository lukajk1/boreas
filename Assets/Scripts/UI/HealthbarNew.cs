using UnityEngine;
using UnityEngine.UI;

public class HealthbarNew : MonoBehaviour
{
    [SerializeField] private Unit unitTarget;
    [SerializeField] private Image healthbar;

    private int currentHealth = 0;
    void OnEnable()
    {
        if (unitTarget != null)
        {
            unitTarget.OnUnitDamaged += UpdateBar;
            unitTarget.OnUnitReady += Setup;
            unitTarget.OnUnitDeath += OnDeath;
        }
    }

    void OnDisable()
    {
        if (unitTarget != null)
        {
            unitTarget.OnUnitDamaged -= UpdateBar;
            unitTarget.OnUnitReady -= Setup;
            unitTarget.OnUnitDeath -= OnDeath;
        }

    }

    private void Setup()
    {
        currentHealth = unitTarget.BaseMaxHealth; 
        healthbar.fillAmount = 1f;
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void UpdateBar(bool isCrit, int damage)
    {
        currentHealth -= damage;
        healthbar.fillAmount = (float)currentHealth / unitTarget.BaseMaxHealth; // cast to float to avoid int division
    }
}
