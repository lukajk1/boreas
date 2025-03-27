using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;

    private const float Range = 999f;
    private float nextFireTime = 0f;


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
        weapon = Inventory.Instance.GetActiveWeapon();
    }

    private void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + weapon.FireRate;
            Fire();
        }
    }

    private void Fire()
    {
        weapon.Fire(fpCamera.transform.position, fpCamera.transform.forward);
    }
}
