using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public abstract string Name { get; }
    public int CurrentHealth { get; private set; }
    public abstract int BaseMaxHealth { get; }
    public int CurrentMaxHealth { get; private set; }
    public abstract float BaseMoveSpeed { get; } // in units per s
    public float CurrentMoveSpeed { get; private set; }

    public event Action<int> OnUnitDamaged;
    public event Action OnUnitReady;
    public event Action OnUnitDeath;

    private void Start()
    {
        CurrentHealth = BaseMaxHealth;
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

    protected virtual void Die()
    {
        OnUnitDeath?.Invoke(); // for "local" processes that need to know about the unit dying
        Destroy(gameObject);
    }
}
