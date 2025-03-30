using UnityEngine;

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

    protected int _currentAmmo;
    public int CurrentAmmo
    {
        get => _currentAmmo;
        set
        {
            if (value >= 0 && value <= ClipSize)
            {
                _currentAmmo = value;
                CombatEventBus.BCOnCurrentAmmoModified();
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
        CurrentAmmo = ClipSize;
        weaponTimer = new GameObject($"Generic {Name} Timer").AddComponent<WeaponTimer>();
        weaponTimer.Setup(this);
    }
    public abstract void Fire(Vector3 firingOrigin, Vector3 forwardFacingVector);
    protected virtual bool TryFire()
    {
        if (CurrentAmmo <= 0)
        {
            return false;
        }
        if (weaponTimer.QueryCanFire())
        {
            HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.ShotFired); // eventually this will be weapon specific and will be moved out of here
            CurrentAmmo--;

            if (_currentAmmo <= 0)
            {
                if (!weaponTimer.IsReloading())
                {
                    Reload();
                }
            }

            return true;
        }
        else return false;
    }
    public virtual void Reload()
    {
        weaponTimer.Reload();
    }

    public void SetCurrentAmmo(int amount)
    {
        CurrentAmmo = amount;
    }

    protected void ProcessHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<EnemyBody>(out var body))
        {
            OnNormalHit();
            HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.NormalHit);

            CombatEventBus.BCOnEnemyHit(BaseDamage, false, hit.point);
            body.transform.parent.GetComponent<EnemyUnit>().TakeDamage(false, BaseDamage);
        }
        else if (hit.collider.TryGetComponent<CriticalEnemy>(out var enemyCritical))
        {
            OnCriticalHit();
            HUDSFXManager.I.PlaySound(HUDSFXManager.SFX.CriticalHit);

            int critAdjustedDamage = (int)(BaseDamage * 1.75f);

            CombatEventBus.BCOnEnemyHit(critAdjustedDamage, true, hit.point);

            //Debug.Log(enemyCritical.transform.root.GetComponent<EnemyUnit>());
            enemyCritical.transform.parent.GetComponent<EnemyUnit>().TakeDamage(true, critAdjustedDamage);
        }
    }

    protected virtual void OnCriticalHit() { }
    protected virtual void OnNormalHit() { }
}
