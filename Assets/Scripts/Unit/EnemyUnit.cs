using System;
using UnityEngine;

public abstract class EnemyUnit : Unit
{
    public abstract int ScoreWeight { get; }
    public abstract int BaseDamage { get; }
    public abstract float AttackCDLength { get; }
    public bool AttackReady { get; set; }
    public abstract float AttackRange { get; }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Die()
    {
        CombatEventBus.BCOnEnemyDeath();
        base.Die();
    }
}
