using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    private Weapon weapon;

    private void OnEnable()
    {
        CombatEventBus.OnActiveWeaponChanged += UpdateActiveWeapon;
    }
    private void OnDisable()
    {
        CombatEventBus.OnActiveWeaponChanged -= UpdateActiveWeapon;
    }
    public void Setup()
    {
        UpdateActiveWeapon();
    }

    private void UpdateActiveWeapon()
    {
        weapon = Inventory.I.GetActiveWeapon();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            weapon.Fire(fpCamera.transform.position, fpCamera.transform.forward);
        }
    }
}
