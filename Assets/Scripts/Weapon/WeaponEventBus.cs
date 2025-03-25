using System;
using UnityEngine;

public static class WeaponEventBus
{
    public static event Action OnActiveWeaponChanged;

    public static void BCOnActiveWeaponChanged()
    {
        OnActiveWeaponChanged?.Invoke();
    }    

    public static event Action OnWeaponFired;

    public static void BCOnWeaponFired()
    {
        OnWeaponFired?.Invoke();
    }

    public static event Action<Vector3> OnEnemyHit;

    public static void BCOnEnemyHit(Vector3 pos)
    {
        OnEnemyHit?.Invoke(pos);
    }

}
