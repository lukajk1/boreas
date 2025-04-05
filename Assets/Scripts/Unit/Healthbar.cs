using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Unit unitTarget;
    [SerializeField] private Slider healthbar;

    private int currentHealth = 0;

    private Camera playerCamera;

    private Transform playerTransform;
    private float activationDistance = 99f;
    private Canvas canvas;
    private bool isVisible = false;
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

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void UpdateBar(bool isCrit, int damage)
    {
        currentHealth -= damage;
        healthbar.value = (float)currentHealth / unitTarget.BaseMaxHealth; // cast to float to avoid int division
    }

    private void Setup()
    {
        canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.enabled = false;
        }

        playerTransform = Game.i.PlayerTransform;
        playerCamera = Game.i.PlayerCamera;

        currentHealth = unitTarget.BaseMaxHealth;
    }

    private void Update()
    {
        if (playerCamera != null)
        {
            transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                             playerCamera.transform.rotation * Vector3.up);
        }

        if (playerTransform != null)
        {
            // Calculate squared distance to avoid unnecessary sqrt operation
            float sqrDistance = (transform.position - playerTransform.position).sqrMagnitude;
            float sqrActivationDistance = activationDistance * activationDistance;

            if (sqrDistance <= sqrActivationDistance && !isVisible)
            {
                // Enable canvas if within range
                SetVisibility(true);
            }
            else if (sqrDistance > sqrActivationDistance && isVisible)
            {
                // Disable canvas if out of range
                SetVisibility(false);
            }
        }
    }

    private void SetVisibility(bool visible)
    {
        isVisible = visible;
        if (canvas != null)
        {
            canvas.enabled = visible;
        }
    }
}
