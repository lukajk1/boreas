using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private TextMeshProUGUI ammoHUD;
    private Weapon activeWeapon;

    private void OnEnable()
    {
        EventBus.OnActiveWeaponChanged += SetActiveWeapon;
        EventBus.OnWeaponFired += UpdateAmmoHUD;
    }

    private void OnDisable()
    {
        EventBus.OnActiveWeaponChanged -= SetActiveWeapon;
        EventBus.OnWeaponFired -= UpdateAmmoHUD;
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
