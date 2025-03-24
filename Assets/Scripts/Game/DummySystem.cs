using UnityEngine;

public class DummySystem : MonoBehaviour
{
    void Start() // place to create temporary setups to plug in intial starting conditions
    {
        Inventory.Instance.SetWeapon(0, new TrackingGun());
        Inventory.Instance.SetWeapon(1, new Deagle());
        FindFirstObjectByType<PlayerShoot>().Setup();
    }
}
