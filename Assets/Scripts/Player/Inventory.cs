using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory I;
    private Weapon[] equippedWeapons = new Weapon[2];
    public int ActiveSlot {  get; private set; }
    private void Awake()
    {
        if (I != null)
        {
            Debug.LogError("multiple singletons in scene");
        }
        I = this;
        ActiveSlot = 0;
    }
    public void SetActiveSlot(int slot)
    {
        if (slot != ActiveSlot && (slot == 0 || slot == 1))
        {
            ActiveSlot = slot;
            CombatEventBus.BCOnActiveWeaponChanged();
        }
    }
    public void SetWeapon(int slot, Weapon weapon)
    {
        CombatEventBus.BCOnWeaponDropped(equippedWeapons[slot]); // drop current weapon

        equippedWeapons[slot] = weapon; // equip new weapon
        if (slot == ActiveSlot)
        {
            CombatEventBus.BCOnActiveWeaponChanged();
        }
    }

    public void SwapActiveWeapon()
    {
        SetActiveSlot(1 - ActiveSlot);
    }

    public Weapon GetWeapon(int slot) 
    { 
        return equippedWeapons[slot];
    }

    public Weapon GetActiveWeapon()
    {
        return equippedWeapons[ActiveSlot];
    }
}
