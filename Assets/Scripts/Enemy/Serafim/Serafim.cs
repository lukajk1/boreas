using UnityEngine;

public class Serafim : EnemyUnit
{
    public override string Name => "Serafim";
    public override int BaseMaxHealth => 50;
    public override float BaseMoveSpeed => 3f;
    public override int ScoreWeight => 100;
    public override int BaseDamage => 45;
    public override float AttackCDLength => 4f;
    public override float AttackRange => 17f;

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
