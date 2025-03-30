using System;
using UnityEngine;

public static class CombatEventBus
{
    public static event Action<Weapon> OnWeaponDropped;
    public static void BCOnWeaponDropped(Weapon droppedWeapon) => OnWeaponDropped?.Invoke(droppedWeapon);

    public static event Action OnActiveWeaponChanged;

    public static void BCOnActiveWeaponChanged() => OnActiveWeaponChanged?.Invoke();

    public static event Action OnWeaponFired;

    public static void BCOnWeaponFired() => OnWeaponFired?.Invoke();

    public static event Action<int, bool, Vector3> OnEnemyHit;

    public static void BCOnEnemyHit(int damage, bool isCrit, Vector3 pos) => OnEnemyHit?.Invoke(damage, isCrit, pos);

    public static event Action<int, bool> OnPlayerHit;
    public static void BCOnPlayerHit(int damage, bool isCrit) => OnPlayerHit?.Invoke(damage, isCrit);

    public static event Action OnEnemyDeath;

    public static void BCOnEnemyDeath() => OnEnemyDeath?.Invoke();

}
