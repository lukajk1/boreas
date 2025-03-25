using System;
using UnityEngine;

public abstract class EnemyUnit : Unit
{

    protected override void Die()
    {
        WeaponEventBus.BCOnEnemyDeath();
        base.Die();
    }
}
