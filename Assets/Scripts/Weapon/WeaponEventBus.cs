using System;
using UnityEngine;

public static class WeaponEventBus
{
    public static event Action OnActiveWeaponChanged;

    public static void BCOnActiveWeaponChanged() => OnActiveWeaponChanged?.Invoke();

    public static event Action OnWeaponFired;

    public static void BCOnWeaponFired() => OnWeaponFired?.Invoke();

    public static event Action<int, bool, Vector3> OnEnemyHit;

    public static void BCOnEnemyHit(int damage, bool isCrit, Vector3 pos) => OnEnemyHit?.Invoke(damage, isCrit, pos);

    public static event Action OnEnemyDeath;

    public static void BCOnEnemyDeath() => OnEnemyDeath?.Invoke();

}
