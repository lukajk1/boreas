using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI ammoHUD;
    [SerializeField] private Slider healthbar;
    [SerializeField] private Slider wallClimbStamina;
    private Weapon activeWeapon;

    private void OnEnable()
    {
        CombatEventBus.OnActiveWeaponChanged += SetActiveWeapon;
        CombatEventBus.OnWeaponFired += UpdateAmmoHUD;
    }

    private void OnDisable()
    {
        CombatEventBus.OnActiveWeaponChanged -= SetActiveWeapon;
        CombatEventBus.OnWeaponFired -= UpdateAmmoHUD;
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
