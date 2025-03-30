using UnityEngine;

public class WeaponPickup : Interactable
{
    [SerializeField] private int weaponId;

    public override void OnInteract()
    {
        Weapon weapon;
        switch (weaponId)
        {
            case 0:
                weapon = new Barehand();
                break;
            case 1:
                weapon = new BloodSiphon();
                break;
            case 2:
                weapon = new ChainDaggers();
                break;
            case 3:
                weapon = new Spear();
                break;
            default:
                weapon = new Barehand();
                break;
        }

        Inventory.I.SetWeapon(Inventory.I.ActiveSlot, weapon);
        Destroy(transform.root.gameObject);
    }
}
