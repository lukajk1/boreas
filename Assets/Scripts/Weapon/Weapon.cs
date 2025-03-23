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

    public abstract float BaseDamage { get; }
    public abstract float FireRate { get; }

    public abstract float LifestealRatio { get; }

    public float[,] DamageFalloffTable;

    public abstract bool Fire();
    public abstract void Reload();
}
