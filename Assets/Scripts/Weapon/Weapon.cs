using UnityEngine;
using UnityEngine.InputSystem.HID;

public abstract class Weapon
{
    public abstract string Name { get; }
    public abstract int ClipSize { get; }
    protected int totalAmmo;
    public int TotalAmmo 
    { 
        get => totalAmmo;
        set
        {
            if (value > 0)
            {
                totalAmmo = value;
            }
        }
    }

    protected int currentAmmo;
    public int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            if (value >= 0 && value <= ClipSize)
            {
                currentAmmo = value;
            }
        }
    }

    public abstract int BaseDamage { get; }
    public abstract float FireRate { get; }
    public abstract float ReloadSpeed { get; }
    public abstract float ReadySpeed { get; } // how fast a gun can be fired after being switched to

    public abstract float LifestealRatio { get; }
    public abstract float Range { get; }

    public float[,] DamageFalloffTable;
    protected WeaponTimer weaponTimer;
    protected RaycastHit hit;

    public Weapon()
    {
        currentAmmo = ClipSize;
        weaponTimer = new GameObject($"{Name} Timer").AddComponent<WeaponTimer>();
        weaponTimer.Setup(this);
    }
    public abstract void Fire(Vector3 firingOrigin, Vector3 forwardFacingVector);
    protected virtual bool TryFire()
    {
        if (currentAmmo <= 0)
        {
            return false;
        }
        if (weaponTimer.QueryCanFire())
        {
            HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.ShotFired); // eventually this will be weapon specific and will be moved out of here
            currentAmmo--;
            CombatEventBus.BCOnWeaponFired(); // currentAmmo has to decrement before broadcast for ammo count to be accurate

            if (currentAmmo <= 0)
            {
                if (!weaponTimer.IsReloading())
                {
                    StartReload();
                }
            }

            return true;
        }
        else return false;
    }
    protected void StartReload()
    {
        weaponTimer.Reload();
    }
    public void Reload()
    {
        currentAmmo = ClipSize;
        CombatEventBus.BCOnWeaponFired(); // hack way of updating ammo count.. change later
    }

    protected void ProcessHit(RaycastHit hit)
    {
        if (hit.transform.root.TryGetComponent<Unit>(out var unit))
        {
            // keep this for now
        }

        if (hit.collider.TryGetComponent<EnemyBody>(out var enemy))
        {
            HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.NormalHit);

            int damageDealt = BaseDamage;

            CombatEventBus.BCOnEnemyHit(damageDealt, false, hit.point);
            unit.TakeDamage(false, damageDealt);
        }
        else if (hit.collider.TryGetComponent<CriticalEnemy>(out var enemyCritical))
        {
            HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.CriticalHit);

            int damageDealt = (int)(BaseDamage * 1.75f);

            CombatEventBus.BCOnEnemyHit(damageDealt, true, hit.point);
            unit.TakeDamage(true, damageDealt);
        }
    }
}
