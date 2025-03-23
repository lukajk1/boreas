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

    private void Start()
    {
        Inventory.Instance.SetWeapon(0, new TrackingGun());
        Debug.Log(Inventory.Instance.GetActiveWeapon().FireRate);
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
        shootSound.Play();
        if (Physics.Raycast(fpCamera.transform.position, fpCamera.transform.forward, out hit, Range))
        {

            if (hit.collider.TryGetComponent<EnemyBody>(out var enemy))
            {
                combatUI.ShowHitMarker();
                hitSound.Play();
                return true;
            }
            else if (hit.collider.TryGetComponent<CriticalEnemy>(out var enemyCritical))
            {
                combatUI.ShowHitMarker();
                critSound.Play();
                return true;
            }
            else return false;

        }
        else return false;
    }
}
