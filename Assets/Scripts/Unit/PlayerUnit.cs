using UnityEngine;

public class PlayerUnit : Unit
{
    public override string Name => "Player";
    public override int BaseMaxHealth => 330;
    public override float BaseMoveSpeed => 6.8f;

    protected override void Die()
    {
        MainEventBus.BCOnRunEnd();
    }
    public override void TakeDamage(bool isCrit, int damage)
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
                CombatEventBus.BCOnPlayerHit(damage, isCrit);
            }
        }
    }
}
