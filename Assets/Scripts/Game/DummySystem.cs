using UnityEngine;

public class DummySystem : MonoBehaviour
{
    void Start() // place to create temporary setups to plug in intial starting conditions
    {
        //Inventory.I.SetWeapon(0, new BloodSiphon());
        //Inventory.I.SetWeapon(1, new Barehand());
        FindFirstObjectByType<PlayerWeaponManager>().Setup();
        MainEventBus.BCOnRunStart();
    }
}
