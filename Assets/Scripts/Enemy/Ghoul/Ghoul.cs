using UnityEngine;

public class Ghoul : EnemyUnit
{
    public override string Name => "Ghoul";
    public override int BaseMaxHealth => 300;
    public override float BaseMoveSpeed => 4.5f;
    public override int ScoreWeight => 150;
    public override int BaseDamage => 60;
    public override float AttackCDLength => 5f;
    public override float AttackRange => 15f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Die()
    {
        IsDead = true;
        LocalizedOnDeathEvent();
        CombatEventBus.BCOnEnemyDeath(this, transform.position);
        SFXManager.i.PlaySFXClip(UISFXList.i.enemyDeath, transform.position);
    }
}
