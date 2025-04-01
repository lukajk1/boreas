using UnityEngine;

public class Caster : EnemyUnit
{
    public override string Name => "Caster";
    public override int BaseMaxHealth => 250;
    public override float BaseMoveSpeed => 5f;
    public override int ScoreWeight => 115;
    public override int BaseDamage => 35;
    public override float AttackCDLength => 1.5f;
    public override float AttackRange => 150f;

    protected override void Awake()
    {
        base.Awake();
    }
}
