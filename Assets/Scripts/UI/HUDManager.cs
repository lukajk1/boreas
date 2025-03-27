using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI ammoHUD;
    [SerializeField] private Slider healthbar;
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
        activeWeapon = Inventory.Instance.GetActiveWeapon();
        gunName.text = activeWeapon.Name;
        ammoHUD.text = $"{activeWeapon.CurrentAmmo} / {activeWeapon.ClipSize}";
    }

    private void UpdateAmmoHUD()
    {
        ammoHUD.text = $"{activeWeapon.CurrentAmmo} / {activeWeapon.ClipSize}";
    }
}
