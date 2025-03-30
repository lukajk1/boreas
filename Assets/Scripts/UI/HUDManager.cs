using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI ammoHUD;
    [SerializeField] private TextMeshProUGUI txtHealth;
    [SerializeField] private Slider healthbar;
    [SerializeField] private Slider wallClimbStamina;
    private Weapon activeWeapon;

    private void OnEnable()
    {
        CombatEventBus.OnActiveWeaponChanged += SetActiveWeapon;
        CombatEventBus.OnWeaponFired += UpdateAmmoHUD;
        CombatEventBus.OnPlayerHit += OnPlayerHit;
        MainEventBus.OnRunStart += Setup;
    }

    private void OnDisable()
    {
        MainEventBus.OnRunStart -= Setup;
        CombatEventBus.OnActiveWeaponChanged -= SetActiveWeapon;
        CombatEventBus.OnWeaponFired -= UpdateAmmoHUD;
        CombatEventBus.OnPlayerHit -= OnPlayerHit;
    }
    private void Setup()
    {
        UpdateHealth();
    }
    private void OnPlayerHit(int damage, bool isCrit)
    {
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        txtHealth.text = $"{Game.I.PlayerUnitInstance.CurrentHealth} / {Game.I.PlayerUnitInstance.CurrentMaxHealth}";
        healthbar.value = (float)Game.I.PlayerUnitInstance.CurrentHealth / Game.I.PlayerUnitInstance.CurrentMaxHealth;
    }
    private void SetActiveWeapon()
    {
        activeWeapon = Inventory.I.GetActiveWeapon();
        gunName.text = activeWeapon.Name;
        ammoHUD.text = $"{activeWeapon.CurrentAmmo} / {activeWeapon.ClipSize}";
    }

    private void UpdateAmmoHUD()
    {
        ammoHUD.text = $"{activeWeapon.CurrentAmmo} / {activeWeapon.ClipSize}";
    }

    public void SetWallClimbStamina(float value)
    {
        wallClimbStamina.value = value;
    }
    public void SetWallClimbMeter(bool value)
    {
        wallClimbStamina.gameObject.SetActive(value);
    }
}
