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
    [SerializeField] private AudioSource playerHurt;

    public enum SFX
    {
        ShotFired, 
        NormalHit, 
        CriticalHit, 
        EnemyKill,
        PlayerHurt
    }

    private Dictionary<SFX, AudioSource> sfxDict;
    private void Awake()
    {
        if (I != null) Debug.LogError("multiple singletons in scene");
        I = this;

        sfxDict = new Dictionary<SFX, AudioSource>
        {
            { SFX.ShotFired, shotFired },
            { SFX.NormalHit, normalHit },
            { SFX.CriticalHit, criticalHit },
            { SFX.EnemyKill, enemyKill },
            { SFX.PlayerHurt, playerHurt}
        };
    }
    private void OnEnable()
    {
        CombatEventBus.OnEnemyDeath += OnEnemyDeath;
        CombatEventBus.OnPlayerHit += OnPlayerHit;
    }
    private void OnDisable()
    {
        CombatEventBus.OnPlayerHit -= OnPlayerHit;
    }

    public void PlaySound(SFX sfx)
    {
        if (!sfxDict.TryGetValue(sfx, out var source))
            throw new KeyNotFoundException($"SFX key '{sfx}' not found in dictionary.");

        if (source == null)
            throw new System.NullReferenceException($"AudioSource for '{sfx}' is null.");

        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
    }

    private void OnEnemyDeath()
    {
        PlaySound(SFX.EnemyKill);
    }
    private void OnPlayerHit(int a, bool b)
    {
        PlaySound(SFX.PlayerHurt);
    }

}
