using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private Weapon[] equippedWeapons = new Weapon[2];
    private int activeSlot = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("multiple singletons in scene");
        }
        Instance = this;
    }
    public void SetActiveSlot(int slot)
    {
        if (slot != activeSlot && (slot == 0 || slot == 1))
        {
            activeSlot = slot;
            EventBus.BCOnActiveWeaponChanged();
        }
    }
    public void SetWeapon(int slot, Weapon weapon)
    {
        equippedWeapons[slot] = weapon;
        if (slot == activeSlot)
        {
            EventBus.BCOnActiveWeaponChanged();
        }
    }

    public Weapon GetWeapon(int slot) 
    { 
        return equippedWeapons[slot];
    }

    public Weapon GetActiveWeapon()
    {
        return equippedWeapons[activeSlot];
    }
}
