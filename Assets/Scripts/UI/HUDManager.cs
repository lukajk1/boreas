using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slot0Gun;
    [SerializeField] private TextMeshProUGUI slot1Gun;
    [SerializeField] private TextMeshProUGUI ammo;
    [SerializeField] private TextMeshProUGUI totalAmmo;
    [SerializeField] private TextMeshProUGUI txtHealth;
    [SerializeField] private Slider healthbar;
    [SerializeField] private Slider wallClimbStamina;
    private Weapon activeWeapon;

    private void OnEnable()
    {
        CombatEventBus.OnActiveWeaponChanged += UpdateActiveWeapon;
        CombatEventBus.OnAmmoCountsModified += UpdateAmmoHUD;
        CombatEventBus.OnPlayerHit += OnPlayerHit;
        MainEventBus.OnRunStart += Setup;
    }

    private void OnDisable()
    {
        MainEventBus.OnRunStart -= Setup;
        CombatEventBus.OnActiveWeaponChanged -= UpdateActiveWeapon;
        CombatEventBus.OnAmmoCountsModified -= UpdateAmmoHUD;
        CombatEventBus.OnPlayerHit -= OnPlayerHit;
    }
    private void Setup()
    {
        UpdateHealth();
        UpdateActiveWeapon();
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
    private void UpdateActiveWeapon()
    {
        activeWeapon = Inventory.I.GetActiveWeapon();
        
        slot0Gun.text = Inventory.I.GetWeapon(0).Name;
        slot1Gun.text = Inventory.I.GetWeapon(1).Name;

        Color grayedOut = new Color(1, 1, 1, 0.2f);
        Color active = new Color(1, 1, 1, 0.6f);

        if (Inventory.I.GetActiveSlot() == 0)
        {
            slot0Gun.color = active;
            slot1Gun.color = grayedOut;
        }
        else
        {
            slot0Gun.color = grayedOut;
            slot1Gun.color = active;
        }

        UpdateAmmoHUD();
    }

    private void UpdateAmmoHUD()
    {
        ammo.text = $"{activeWeapon.CurrentAmmo} / {activeWeapon.ClipSize}";
        totalAmmo.text = $"remaining: {activeWeapon.TotalAmmo}";
    }

    public void SetWallClimbStamina(float value)
    {
        //wallClimbStamina.value = value;
    }
    public void SetWallClimbMeter(bool value)
    {
        //wallClimbStamina.gameObject.SetActive(value);
    }
}
