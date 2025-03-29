using System;
using UnityEngine;

public abstract class EnemyUnit : Unit
{
    public abstract int ScoreWeight { get; }
    public abstract int BaseDamage { get; }
    protected override void Die()
    {
        CombatEventBus.BCOnEnemyDeath();
        base.Die();
    }
}
