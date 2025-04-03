using UnityEngine;

public class PlayerUnit : Unit
{
    public override string Name => "Player";
    public override int BaseMaxHealth => 330;
    public override float BaseMoveSpeed => 6.8f;

    public int DebugDamageModifier = 1;

    protected override void Die()
    {
        IsDead = true;
        MainEventBus.BCOnRunEnd();
    }
    public override void TakeDamage(bool isCrit, int damage)
    {
        if (IsDead) return;

        if (damage > 0)
        {
            CurrentHealth -= damage * DebugDamageModifier;
            CombatEventBus.BCOnPlayerHit(damage, isCrit);

            SFXManager.i.PlaySFXClip(UISFXList.i.enemyBodyHit, Game.i.PlayerTransform.position, false); 
            if (damage >= CurrentHealth) Die();

        }
    }
}
