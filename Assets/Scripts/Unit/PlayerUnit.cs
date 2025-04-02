using UnityEngine;

public class PlayerUnit : Unit
{
    public override string Name => "Player";
    public override int BaseMaxHealth => 330;
    public override float BaseMoveSpeed => 6.8f;

    public int DebugDamageModifier = 1;

    protected override void Die()
    {
        MainEventBus.BCOnRunEnd();
    }
    public override void TakeDamage(bool isCrit, int damage)
    {
        if (damage > 0)
        {
            CurrentHealth -= damage * DebugDamageModifier;
            CombatEventBus.BCOnPlayerHit(damage, isCrit);

            SFXManager.I.PlaySFXClip(UISFXList.I.enemyBodyHit, Game.I.PlayerTransform.position, false); 
            if (damage >= CurrentHealth) Die();

        }
    }
}
