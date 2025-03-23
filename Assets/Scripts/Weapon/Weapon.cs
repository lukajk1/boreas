using UnityEngine;

public abstract class Weapon
{
    public int ClipSize;
    public int TotalAmmo;
    public int CurrentAmmo;
    public float BaseDamage;
    public float FireRate;

    public float[,] DamageFalloffTable;

    public abstract bool Fire();
    public abstract void Reload();
}
