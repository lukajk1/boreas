using System;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public abstract string Name { get; }
    public int CurrentHealth { get; protected set; }
    public abstract int BaseMaxHealth { get; }
    public int CurrentMaxHealth { get; protected set; }
    public abstract float BaseMoveSpeed { get; } // in units per s
    public float CurrentMoveSpeed { get; protected set; }

    public event Action<bool, int> OnUnitDamaged;
    public event Action OnUnitReady;
    public event Action OnUnitDeath;
    protected virtual void Awake()
    {
        CurrentMaxHealth = BaseMaxHealth;
        CurrentHealth = CurrentMaxHealth;
        CurrentMoveSpeed = BaseMoveSpeed;
    }
    private void Start()
    {
        OnUnitReady?.Invoke();
    }

    public virtual void TakeDamage(bool isCrit, int damage)
    {
        if (damage > 0)
        {
            if (damage >= CurrentHealth)
            {

                //Debug.Log("damage "+damage);
                //Debug.Log("currenthealth "+CurrentHealth);
                Die();
            }
            else
            {
                CurrentHealth -= damage;
                OnUnitDamaged?.Invoke(isCrit, damage);
            }
        }
    }

    public void KillViaCommand()
    {
        Die();
    }
    protected virtual void Die()
    {
        OnUnitDeath?.Invoke(); // for "local" processes that need to know about the unit dying
        Destroy(gameObject);
    }
}
