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

    public float[,] DamageFalloffTable;

    public virtual void Fire()
    {
        currentAmmo--;
        CombatEventBus.BCOnWeaponFired(); // currentAmmo has to decrement before broadcast for ammo count to be accurate



        if (currentAmmo <= 0)
        {
            Reload();
        }
    }
    public void Reload()
    {
        currentAmmo = ClipSize;
        CombatEventBus.BCOnWeaponFired(); // hack way of updating ammo count.. change later
    }

    public Weapon()
    {
        currentAmmo = ClipSize;
    }
}
