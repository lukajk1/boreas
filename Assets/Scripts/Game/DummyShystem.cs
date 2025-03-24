using UnityEngine;

public class DummyShystem : MonoBehaviour
{
    void Start()
    {
        Inventory.Instance.SetWeapon(0, new TrackingGun());
        Inventory.Instance.SetWeapon(1, new Railgun());
        FindFirstObjectByType<PlayerShoot>().Setup();
    }
}
