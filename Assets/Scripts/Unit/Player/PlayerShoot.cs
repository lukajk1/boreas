using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource critSound;
    [SerializeField] private CombatUI combatUI;

    private const float Range = 999f;
    private float nextFireTime = 0f;

    private RaycastHit hit;

    private Weapon weapon;

    private void OnEnable()
    {
        WeaponEventBus.OnActiveWeaponChanged += UpdateActiveWeapon;
    }
    private void OnDisable()
    {
        WeaponEventBus.OnActiveWeaponChanged -= UpdateActiveWeapon;
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

    private bool Fire()
    {
        weapon.Fire();
        HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.ShotFired);

        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, Range))
        {
            if (hit.transform.root.TryGetComponent<Unit>(out var unit))
            {
            }


            if (hit.collider.TryGetComponent<EnemyBody>(out var enemy))
            {
                combatUI.ShowHitMarker(false);
                HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.NormalHit);
                WeaponEventBus.BCOnEnemyHit(17, false, hit.point);
                unit.TakeDamage(false, 17);
                return true;
            }
            else if (hit.collider.TryGetComponent<CriticalEnemy>(out var enemyCritical))
            {
                combatUI.ShowHitMarker(true);
                HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.CriticalHit);
                WeaponEventBus.BCOnEnemyHit(17, true, hit.point);
                unit.TakeDamage(true, 17);
                return true;
            }
            else return false;

        }
        else return false;
    }
}
