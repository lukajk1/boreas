using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public abstract string Name { get; }
    public int CurrentHealth { get; private set; }
    public abstract int MaxHealth { get; }
    public abstract float BaseMoveSpeed { get; } // in units per s

    public event Action<int> OnUnitDamaged;
    public event Action OnUnitReady;

    private void Start()
    {
        CurrentHealth = MaxHealth;
        OnUnitReady?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            if (damage >= CurrentHealth)
            {
                Die();
            }
            else
            {
                CurrentHealth -= damage;
                OnUnitDamaged?.Invoke(damage);
            }
        }
    }

    private void Die()
    {
        // ..
    }
}
