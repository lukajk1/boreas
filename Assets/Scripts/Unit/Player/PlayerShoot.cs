using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private AudioSource shootSound;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource critSound;
    [SerializeField] private CombatUI combatUI;

    private const float Range = 999f;
    private const float FireRate = 0.2f;
    private float nextFireTime = 0f;

    private RaycastHit hit;


    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + FireRate;
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
                combatUI.ShowHitMarker(FireRate * 1.4f);
                hitSound.Play();
                return true;
            }
            else if (hit.collider.TryGetComponent<CriticalEnemy>(out var enemyCritical))
            {
                combatUI.ShowHitMarker(FireRate * 1.4f);
                critSound.Play();
                return true;
            }
            else return false;

        }
        else return false;
    }
}
