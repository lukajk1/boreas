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
            if (value >= 0)
            {
                totalAmmo = value;
                CombatEventBus.BCOnAmmoCountsModified();
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
                CombatEventBus.BCOnAmmoCountsModified();
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
        TotalAmmo = DecideInitialTotalAmmo();
        weaponTimer = new GameObject($"Generic {Name} Timer").AddComponent<WeaponTimer>();
        weaponTimer.Setup(this);
    }
    public abstract void Fire(Vector3 firingOrigin, Vector3 forwardFacingVector);

    protected virtual int DecideInitialTotalAmmo()
    {
        return Random.Range(8, 16) * ClipSize;
    }

    protected virtual bool TryFire()
    {
        if (CurrentAmmo <= 0)
        {
            return false;
        }
        if (weaponTimer.QueryCanFire())
        {
            SFXManager.I.PlaySFXClip(UISFXList.I.weaponFire, Game.I.PlayerTransform.position); // eventually this will be weapon specific and will be moved out of here
            CurrentAmmo--;
            CombatEventBus.BCOnWeaponFired();

            if (_currentAmmo <= 0)
            {
                if (!weaponTimer.IsReloading())
                {
                    Reload();
                    SFXManager.I.PlaySFXClip(UISFXList.I.outOfBullets, Game.I.PlayerTransform.position);
                }
            }

            return true;
        }
        else return false;
    }
    public virtual void Reload()
    {
        if (TotalAmmo >= ClipSize)
        {
            weaponTimer.Reload(ClipSize - CurrentAmmo);
        }
        else if (TotalAmmo > 0 && TotalAmmo < ClipSize)
        {
            weaponTimer.Reload(TotalAmmo);
            TotalAmmo = 0;
        }
        else if (TotalAmmo == 0 && CurrentAmmo == 0) 
        {
            Break(); // weapon is destroyed when it runs out of ammo
            Debug.Log("broken");
        }
    }
    protected virtual void Break() { }

    public void SetCurrentAmmo(int amount)
    {
        CurrentAmmo = amount;
    }

    protected void ProcessHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<EnemyBody>(out var body))
        {
            OnNormalHit(); 
            SFXManager.I.PlaySFXClip(UISFXList.I.enemyBodyHit, Game.I.PlayerTransform.position);

            CombatEventBus.BCOnEnemyHit(BaseDamage, false, hit.point);

            body.MyEnemyUnit.TakeDamage(false, BaseDamage);
        }
        else if (hit.collider.TryGetComponent<EnemyCritical>(out var enemyCritical))
        {
            OnCriticalHit(); 
            SFXManager.I.PlaySFXClip(UISFXList.I.enemyCritHit, Game.I.PlayerTransform.position);

            int critAdjustedDamage = (int)(BaseDamage * 1.75f);

            CombatEventBus.BCOnEnemyHit(critAdjustedDamage, true, hit.point);

            //Debug.Log(enemyCritical.transform.root.GetComponent<EnemyUnit>());
            enemyCritical.MyEnemyUnit.TakeDamage(true, critAdjustedDamage);
        }
    }

    protected virtual void OnCriticalHit() { }
    protected virtual void OnNormalHit() { }
}
