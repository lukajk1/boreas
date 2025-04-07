using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarNew : MonoBehaviour
{
    [SerializeField] private Unit unitTarget;
    [SerializeField] private Image healthbar;
    [SerializeField] private Image whiteHealth;

    private int currentHealth = 0;
    private Coroutine lerpWhiteHealth;
    private float lerpDuration = 0.40f;
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
        whiteHealth.fillAmount = 1f;
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void UpdateBar(bool isCrit, int damage)
    {
        currentHealth -= damage;
        healthbar.fillAmount = (float)currentHealth / unitTarget.BaseMaxHealth; // cast to float to avoid int division

        if (lerpWhiteHealth == null)
        {
            lerpWhiteHealth = StartCoroutine(LerpWhiteHealth());
        }
        else
        {
            StopCoroutine(lerpWhiteHealth);
            lerpWhiteHealth = StartCoroutine(LerpWhiteHealth());
        }
    }

    private IEnumerator LerpWhiteHealth()
    {
        float elapsed = 0;
        while (elapsed < lerpDuration) 
        {
            elapsed += Time.deltaTime;
            whiteHealth.fillAmount = Mathf.Lerp(whiteHealth.fillAmount, healthbar.fillAmount, elapsed);
            yield return null;
        }
        lerpWhiteHealth = null;
    }
}
