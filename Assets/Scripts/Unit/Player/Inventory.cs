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
    public void SetWeapon(int slot, Weapon weapon)
    {
        equippedWeapons[slot] = weapon;
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
