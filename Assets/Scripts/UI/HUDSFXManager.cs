using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HUDSFXManager : MonoBehaviour
{
    public static HUDSFXManager I;

    [SerializeField] private AudioSource shotFired;
    [SerializeField] private AudioSource normalHit;
    [SerializeField] private AudioSource criticalHit;
    [SerializeField] private AudioSource enemyKill;

    public enum SFX
    {
        ShotFired, 
        NormalHit, 
        CriticalHit, 
        EnemyKill
    }

    private Dictionary<SFX, AudioSource> sfxDict;

    private void OnEnable()
    {
        WeaponEventBus.OnEnemyDeath += OnEnemyDeath;
    }
    private void OnDisable()
    {
        WeaponEventBus.OnEnemyDeath -= OnEnemyDeath;
    }

    private void Awake()
    {
        if (I != null)
        {
            Debug.LogError("multiple singletons in scene");
        }
        I = this;

        sfxDict = new Dictionary<SFX, AudioSource>
        {
            { SFX.ShotFired, shotFired },
            { SFX.NormalHit, normalHit },
            { SFX.CriticalHit, criticalHit },
            { SFX.EnemyKill, enemyKill }
        };
    }

    public void PlaySound(SFX sfx)
    {
        if (!sfxDict.TryGetValue(sfx, out var source))
            throw new KeyNotFoundException($"SFX key '{sfx}' not found in dictionary.");

        if (source == null)
            throw new System.NullReferenceException($"AudioSource for '{sfx}' is null.");

        source.Play();
    }

    private void OnEnemyDeath()
    {
        PlaySound(SFX.EnemyKill);
    }

}
