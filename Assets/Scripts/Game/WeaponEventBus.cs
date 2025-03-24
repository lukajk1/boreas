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
}
